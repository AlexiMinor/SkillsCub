using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillsCub.DataLibrary.Migrations
{
    public partial class UserChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionPlan",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddAssessment",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalEducation",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BudgetPlanning",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CombineIncongruous",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommandProfessionalExperience",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommandWork",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommunicationExperience",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contacts",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreativityLimitation",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisclosureOPfSecrecy",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndJustifiesTheMeans",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExplainingComfort",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FlexibleSchedule",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Flextime",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FreelanceWork",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FullDay",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "KindOfActivity",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KindOfThinking",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MailingLists",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MaintainingStatistics",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NeedForCommunication",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OneOffWork",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PlanningExperience",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousActivities",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousProjects",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Professions",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Psychotic",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchasingAlgorithm",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelationToPerformances",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RemoteWork",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResponseForCritic",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Responsibility",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SMMExperience",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelfEducation",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShiftChart",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TemporaryJob",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WorkTime",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionPlan",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AddAssessment",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AdditionalEducation",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BudgetPlanning",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CombineIncongruous",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CommandProfessionalExperience",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CommandWork",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CommunicationExperience",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Contacts",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreativityLimitation",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DisclosureOPfSecrecy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EndJustifiesTheMeans",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExplainingComfort",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FlexibleSchedule",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Flextime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FreelanceWork",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullDay",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KindOfActivity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KindOfThinking",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MailingLists",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MaintainingStatistics",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NeedForCommunication",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OneOffWork",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlanningExperience",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PreviousActivities",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PreviousProjects",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Professions",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Psychotic",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PurchasingAlgorithm",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RelationToPerformances",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RemoteWork",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResponseForCritic",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Responsibility",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SMMExperience",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SelfEducation",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShiftChart",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TemporaryJob",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkTime",
                table: "AspNetUsers");
        }
    }
}
