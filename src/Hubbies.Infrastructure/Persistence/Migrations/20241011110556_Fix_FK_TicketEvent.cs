using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hubbies.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fix_FK_TicketEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketEvents_Account_ApplicationUserId",
                table: "TicketEvents");

            migrationBuilder.DropIndex(
                name: "IX_TicketEvents_ApplicationUserId",
                table: "TicketEvents");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "TicketEvents");

            migrationBuilder.AddColumn<Guid>(
                name: "EventHostId",
                table: "TicketEvents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TicketEvents_EventHostId",
                table: "TicketEvents",
                column: "EventHostId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEvents_Account_EventHostId",
                table: "TicketEvents",
                column: "EventHostId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketEvents_Account_EventHostId",
                table: "TicketEvents");

            migrationBuilder.DropIndex(
                name: "IX_TicketEvents_EventHostId",
                table: "TicketEvents");

            migrationBuilder.DropColumn(
                name: "EventHostId",
                table: "TicketEvents");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "TicketEvents",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketEvents_ApplicationUserId",
                table: "TicketEvents",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEvents_Account_ApplicationUserId",
                table: "TicketEvents",
                column: "ApplicationUserId",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}
