using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QueueMS.Domain.Models.CounterModels;
using QueueMS.Domain.Models.ServiceModel;
using QueueMS.Domain.Models.TokenModels;
using QueueMS.Domain.Models.UserModels;
using System.Data.Common;

namespace QueueMS.Infrastructure.DatabaseContext;

public class QueueMSDatabaseContext : IdentityDbContext<User>
{
    private DbTransaction? _transaction;

    public QueueMSDatabaseContext(DbContextOptions<QueueMSDatabaseContext> options) : base(options) { }

    public DbSet<Counter> Counters { get; set; }
    public DbSet<CounterStaff> CounterStaffs { get; set; }
    public DbSet<Services> Services { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<User> Users {  get; set; }
    public DbSet<CounterService> CounterServices {  get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

       
        ConfigureRelationship(builder);
    }

    private static void ConfigureRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CounterService>().HasKey(
            x => new
            {
                x.ServiceId,
                x.CounterId
            });


        modelBuilder.Entity<CounterStaff>().HasKey(x => new
        {
            x.CounterId,
            x.StaffId
        });


        // Counter -> Counter Service
        modelBuilder.Entity<CounterService>()
            .HasOne(x => x.Counters)
            .WithMany(x => x.CounterServices)
            .HasForeignKey(x => x.CounterId);

        // Service -> CounterService
        modelBuilder.Entity<CounterService>()
            .HasOne(x => x.Services)
            .WithMany(x => x.CounterServices)
            .HasForeignKey(x => x.ServiceId);

        // Counter -> CounterStaff
        modelBuilder.Entity<CounterStaff>()
            .HasOne(x => x.Counter)
            .WithMany(x => x.CounterStaffs)
            .HasForeignKey(x => x.CounterId);

        // User -> CounterStaff
        modelBuilder.Entity<CounterStaff>()
            .HasOne(x => x.Staff)
            .WithMany(x => x.CounterStaffs)
            .HasForeignKey(x => x.StaffId)
            .OnDelete(DeleteBehavior.Restrict);

        //Service -> Token
        modelBuilder.Entity<Token>()
            .HasOne(x => x.Service)
            .WithMany(x => x.Tokens)
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);


        // Customer -> Token
        modelBuilder.Entity<Token>()
            .HasOne(x => x.Customer)
            .WithMany(x => x.Tokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Counter -> Token
        modelBuilder.Entity<Token>()
            .HasOne(x => x.Counter)
            .WithMany(x => x.Tokens)
            .HasForeignKey(x =>x.CounterId)
            .OnDelete(DeleteBehavior.Restrict);


        // Unique Fields 

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();


        modelBuilder.Entity<Services>()
            .HasIndex(x => x.Prefix)
            .IsUnique();

        modelBuilder.Entity<Token>()
            .HasIndex(x => x.TokenNumber)
            .IsUnique();
            



    }
}
