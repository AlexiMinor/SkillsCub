﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkillsCub.DataLibrary.Repositories.Context;

namespace SkillsCub.DataLibrary.Migrations
{
    /// <summary>
    /// The aplication database context 
    /// </summary>
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SkillsCub.DataLibrary.Entities.Implementation.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ActionPlan");

                    b.Property<string>("ActivityTime");

                    b.Property<string>("AddAssessment");

                    b.Property<string>("AdditionalEducation");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("BudgetPlanning");

                    b.Property<string>("City");

                    b.Property<string>("CombineIncongruous");

                    b.Property<string>("CommandProfessionalExperience");

                    b.Property<string>("CommandWork");

                    b.Property<string>("CommunicationExperience");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Contacts");

                    b.Property<string>("CreativityLimitation");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("DisclosureOPfSecrecy");

                    b.Property<string>("Education");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("EndJustifiesTheMeans");

                    b.Property<string>("ExplainingComfort");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsAddToTheMailingList");

                    b.Property<bool>("IsArchitect");

                    b.Property<bool>("IsCameraman");

                    b.Property<bool>("IsClothesDesigner");

                    b.Property<bool>("IsCuratorNeeded");

                    b.Property<bool>("IsEditor");

                    b.Property<bool>("IsEventManager");

                    b.Property<bool>("IsFlexibleSchedule");

                    b.Property<bool>("IsFlextime");

                    b.Property<bool>("IsFree");

                    b.Property<bool>("IsFreelanceWork");

                    b.Property<bool>("IsFullDay");

                    b.Property<bool>("IsGraphicalDesigner");

                    b.Property<bool>("IsIllustrator");

                    b.Property<bool>("IsInCollective");

                    b.Property<bool>("IsIndividual");

                    b.Property<bool>("IsInterpreter");

                    b.Property<bool>("IsLayer");

                    b.Property<bool>("IsMarketer");

                    b.Property<bool>("IsNeedCV");

                    b.Property<bool>("IsNeedRecommendation");

                    b.Property<bool>("IsOneOffWork");

                    b.Property<bool>("IsPayable");

                    b.Property<bool>("IsPhotographer");

                    b.Property<bool>("IsPrManager");

                    b.Property<bool>("IsProjectManager");

                    b.Property<bool>("IsRemoteWork");

                    b.Property<bool>("IsScreenwriter");

                    b.Property<bool>("IsShiftChart");

                    b.Property<bool>("IsSmm");

                    b.Property<bool>("IsTemporaryJob");

                    b.Property<bool>("IsWebDesigner");

                    b.Property<bool>("IsWriter");

                    b.Property<string>("KindOfThinking");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MaintainingStatistics");

                    b.Property<string>("NeedForCommunication");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Patronymic");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PlanningExperience");

                    b.Property<string>("PreviousActivities");

                    b.Property<string>("PreviousProjects");

                    b.Property<string>("Psychotic");

                    b.Property<string>("PurchasingAlgorithm");

                    b.Property<string>("RelationToPerformances");

                    b.Property<string>("ResponseForCritic");

                    b.Property<bool>("Responsibility");

                    b.Property<string>("SMMExperience");

                    b.Property<string>("ScheduleOfWorkingDays");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("SelfEducation");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SkillsCub.DataLibrary.Entities.Implementation.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AssignationDate");

                    b.Property<DateTime>("ConsultationDate");

                    b.Property<string>("ConsultationPlace");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("StudentId");

                    b.Property<string>("TeacherId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("SkillsCub.DataLibrary.Entities.Implementation.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AnswerDateTime");

                    b.Property<string>("AnswerValue");

                    b.Property<DateTime>("CloseDateTime");

                    b.Property<string>("ConditionOfProblem");

                    b.Property<Guid>("CourseId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime?>("LastEditDate");

                    b.Property<int>("Mark");

                    b.Property<string>("MarkComment");

                    b.Property<DateTime>("MarkDateTime");

                    b.Property<string>("Name");

                    b.Property<DateTime>("OpenDateTime");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("SkillsCub.DataLibrary.Entities.Implementation.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CourseId");

                    b.Property<string>("MessageText");

                    b.Property<string>("RecieverId");

                    b.Property<DateTime>("SendedDateTime");

                    b.Property<string>("SenderId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("RecieverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("SkillsCub.DataLibrary.Entities.Implementation.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AppliedDate");

                    b.Property<int>("Course");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool>("FirstTime");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Patronymic")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.Property<int>("Source");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SkillsCub.DataLibrary.Entities.Implementation.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SkillsCub.DataLibrary.Entities.Implementation.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SkillsCub.DataLibrary.Entities.Implementation.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SkillsCub.DataLibrary.Entities.Implementation.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SkillsCub.DataLibrary.Entities.Implementation.Course", b =>
                {
                    b.HasOne("SkillsCub.DataLibrary.Entities.Implementation.ApplicationUser", "Student")
                        .WithMany("Courses")
                        .HasForeignKey("StudentId");

                    b.HasOne("SkillsCub.DataLibrary.Entities.Implementation.ApplicationUser", "Teacher")
                        .WithMany("CoursesAsTeacher")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("SkillsCub.DataLibrary.Entities.Implementation.Exercise", b =>
                {
                    b.HasOne("SkillsCub.DataLibrary.Entities.Implementation.Course", "Course")
                        .WithMany("Exercises")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SkillsCub.DataLibrary.Entities.Implementation.ApplicationUser", "User")
                        .WithMany("Exercises")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SkillsCub.DataLibrary.Entities.Implementation.Message", b =>
                {
                    b.HasOne("SkillsCub.DataLibrary.Entities.Implementation.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SkillsCub.DataLibrary.Entities.Implementation.ApplicationUser", "Reciever")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("RecieverId");

                    b.HasOne("SkillsCub.DataLibrary.Entities.Implementation.ApplicationUser", "Sender")
                        .WithMany("SendedMessages")
                        .HasForeignKey("SenderId");
                });
#pragma warning restore 612, 618
        }
    }
}
