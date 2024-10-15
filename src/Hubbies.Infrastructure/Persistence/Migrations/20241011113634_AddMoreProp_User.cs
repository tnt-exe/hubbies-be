using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hubbies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreProp_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Account",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset?>(
                name: "Dob",
                table: "Account",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Dob",
                table: "Account");
        }
    }
}
