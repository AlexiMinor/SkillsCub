using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillsCub.DataLibrary.Migrations
{
    public partial class editingProfessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Professions",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsArchitect",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCameraman",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsClothesDesigner",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEditor",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEventManager",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGraphicalDesigner",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIllustrator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInterpreter",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLayer",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMarketer",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPhotographer",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrManager",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProjectManager",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsScreenwriter",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSmm",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWebDesigner",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWriter",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchitect",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsCameraman",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsClothesDesigner",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsEditor",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsEventManager",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsGraphicalDesigner",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsIllustrator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsInterpreter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsLayer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsMarketer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsPhotographer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsPrManager",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsProjectManager",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsScreenwriter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsSmm",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsWebDesigner",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsWriter",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Professions",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
