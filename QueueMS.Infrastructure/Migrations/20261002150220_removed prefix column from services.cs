using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QueueMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removedprefixcolumnfromservices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_counter_staffs_user_staff_id",
                table: "counter_staffs");

            migrationBuilder.DropForeignKey(
                name: "fk_tokens_user_user_id",
                table: "tokens");

            migrationBuilder.DropIndex(
                name: "ix_services_prefix",
                table: "services");

            migrationBuilder.DropColumn(
                name: "prefix",
                table: "services");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "services",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "token_counter",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    service_id = table.Column<int>(type: "integer", nullable: false),
                    token_date = table.Column<DateOnly>(type: "date", nullable: false),
                    last_number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_token_counter", x => x.id);
                    table.ForeignKey(
                        name: "fk_token_counter_services_service_id",
                        column: x => x.service_id,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_token_counter_service_id_token_date",
                table: "token_counter",
                columns: new[] { "service_id", "token_date" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_counter_staffs_users_staff_id",
                table: "counter_staffs",
                column: "staff_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_tokens_users_user_id",
                table: "tokens",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_counter_staffs_users_staff_id",
                table: "counter_staffs");

            migrationBuilder.DropForeignKey(
                name: "fk_tokens_users_user_id",
                table: "tokens");

            migrationBuilder.DropTable(
                name: "token_counter");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "services",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "prefix",
                table: "services",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_services_prefix",
                table: "services",
                column: "prefix",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_counter_staffs_user_staff_id",
                table: "counter_staffs",
                column: "staff_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_tokens_user_user_id",
                table: "tokens",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
