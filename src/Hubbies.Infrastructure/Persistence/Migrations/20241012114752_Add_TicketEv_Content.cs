using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hubbies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_TicketEv_Content : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "TicketEvents",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "TicketEvents");
        }
    }
}
