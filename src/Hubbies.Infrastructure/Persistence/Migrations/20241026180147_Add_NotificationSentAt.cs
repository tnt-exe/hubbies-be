using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hubbies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_NotificationSentAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SentAt",
                table: "Notifications",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentAt",
                table: "Notifications");
        }
    }
}
