using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillsCub.DataLibrary.Migrations
{
    public partial class AddFieldsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TemporaryJob",
                table: "AspNetUsers",
                newName: "IsTemporaryJob");

            migrationBuilder.RenameColumn(
                name: "RemoteWork",
                table: "AspNetUsers",
                newName: "IsRemoteWork");

            migrationBuilder.RenameColumn(
                name: "OneOffWork",
                table: "AspNetUsers",
                newName: "IsPayable");

            migrationBuilder.RenameColumn(
                name: "FullDay",
                table: "AspNetUsers",
                newName: "IsOneOffWork");

            migrationBuilder.RenameColumn(
                name: "FreelanceWork",
                table: "AspNetUsers",
                newName: "IsNeedRecommendation");

            migrationBuilder.AddColumn<bool>(
                name: "IsCuratorNeeded",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFreelanceWork",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFullDay",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInCollective",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIndividual",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNeedCV",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCuratorNeeded",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsFreelanceWork",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsFullDay",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsInCollective",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsIndividual",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsNeedCV",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IsTemporaryJob",
                table: "AspNetUsers",
                newName: "TemporaryJob");

            migrationBuilder.RenameColumn(
                name: "IsRemoteWork",
                table: "AspNetUsers",
                newName: "RemoteWork");

            migrationBuilder.RenameColumn(
                name: "IsPayable",
                table: "AspNetUsers",
                newName: "OneOffWork");

            migrationBuilder.RenameColumn(
                name: "IsOneOffWork",
                table: "AspNetUsers",
                newName: "FullDay");

            migrationBuilder.RenameColumn(
                name: "IsNeedRecommendation",
                table: "AspNetUsers",
                newName: "FreelanceWork");
        }
    }
}
