using Microsoft.EntityFrameworkCore.Migrations;

namespace Bugtracker.Migrations
{
    public partial class domain_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeId",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubmitterId",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tickets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssigneeId",
                table: "Tickets",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SubmitterId",
                table: "Tickets",
                column: "SubmitterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_AssigneeId",
                table: "Tickets",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_SubmitterId",
                table: "Tickets",
                column: "SubmitterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_AssigneeId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_SubmitterId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AssigneeId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SubmitterId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SubmitterId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
