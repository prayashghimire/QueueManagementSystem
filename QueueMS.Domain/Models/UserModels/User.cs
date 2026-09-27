
using Microsoft.AspNetCore.Identity;
using QueueMS.Domain.Enum;
using QueueMS.Domain.Models.CounterModels;
using QueueMS.Domain.Models.QueueModels;
using QueueMS.Domain.Models.TokenModels;

namespace QueueMS.Domain.Models.UserModels;

public class User : IdentityUser<int>
{
    public UserRole Role { get; set; }
    public bool IsActive { get; set; }


    public ICollection<Token> Tokens { get; set; } = new List<Token>();
    public ICollection<CounterStaff> CounterStaffs { get; set; } = new List<CounterStaff>();
    public ICollection<QueueHistory> QueueHistories { get; set; } = new List<QueueHistory>();

}
