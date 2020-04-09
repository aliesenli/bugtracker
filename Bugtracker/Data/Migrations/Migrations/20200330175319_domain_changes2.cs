using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bugtracker.Migrations
{
    public partial class domain_changes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Projects_ProjectId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ProjectId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "Tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ProjectId",
                table: "Tickets",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Projects_ProjectId",
                table: "Tickets",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Projects_ProjectId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ProjectId",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId1",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ProjectId1",
                table: "Tickets",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Projects_ProjectId1",
                table: "Tickets",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
