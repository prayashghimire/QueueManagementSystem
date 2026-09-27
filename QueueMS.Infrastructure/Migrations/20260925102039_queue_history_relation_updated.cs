using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueueMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class queue_history_relation_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_queue_history_counter_id",
                table: "queue_history",
                column: "counter_id");

            migrationBuilder.CreateIndex(
                name: "ix_queue_history_performed_by",
                table: "queue_history",
                column: "performed_by");

            migrationBuilder.CreateIndex(
                name: "ix_queue_history_token_id",
                table: "queue_history",
                column: "token_id");

            migrationBuilder.AddForeignKey(
                name: "fk_queue_history_asp_net_users_performed_by",
                table: "queue_history",
                column: "performed_by",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_queue_history_counters_counter_id",
                table: "queue_history",
                column: "counter_id",
                principalTable: "counters",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_queue_history_tokens_token_id",
                table: "queue_history",
                column: "token_id",
                principalTable: "tokens",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_queue_history_asp_net_users_performed_by",
                table: "queue_history");

            migrationBuilder.DropForeignKey(
                name: "fk_queue_history_counters_counter_id",
                table: "queue_history");

            migrationBuilder.DropForeignKey(
                name: "fk_queue_history_tokens_token_id",
                table: "queue_history");

            migrationBuilder.DropIndex(
                name: "ix_queue_history_counter_id",
                table: "queue_history");

            migrationBuilder.DropIndex(
                name: "ix_queue_history_performed_by",
                table: "queue_history");

            migrationBuilder.DropIndex(
                name: "ix_queue_history_token_id",
                table: "queue_history");
        }
    }
}
