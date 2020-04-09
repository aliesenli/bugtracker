using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bugtracker.Migrations
{
    public partial class guid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId1",
                table: "Tickets",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
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
    }
}
