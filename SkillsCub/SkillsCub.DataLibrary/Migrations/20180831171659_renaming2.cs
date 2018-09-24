using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillsCub.DataLibrary.Migrations
{
    public partial class renaming2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MailingLists",
                table: "AspNetUsers",
                newName: "IsAddToTheMailingList");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TheMailingList",
                table: "AspNetUsers",
                newName: "IsAddToTheMailingLists");
        }
    }
}
