using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SkillsCub.DataLibrary.Migrations
{
    public partial class Messages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "Message",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Message_CourseId",
                table: "Message",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Courses_CourseId",
                table: "Message",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Courses_CourseId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_CourseId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Message");
        }
    }
}
