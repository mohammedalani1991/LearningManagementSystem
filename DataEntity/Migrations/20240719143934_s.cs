using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataEntity.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutDic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutDic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AcademicSupervisionStandard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Standard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSupervisionStandard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calendar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CmsCatery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ShowInHomePage = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsCatery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsCatery_CmsCatery",
                        column: x => x.ParentId,
                        principalTable: "CmsCatery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsProjectCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsProjectCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CmsSlider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReadMoreLink = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    Image2Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsSlider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CmsWhatPeopleSay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowInHomePage = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsWhatPeopleSay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationChannel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ShowInHomePage = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCategory_CourseCategory",
                        column: x => x.ParentId,
                        principalTable: "CourseCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoursePackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    PackageName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: true),
                    IsExchange = table.Column<bool>(type: "bit", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataBaseScripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataBaseScripts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expulsion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpelledFrom = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpelledTo = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpulsionStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpulsionEnd = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expulsion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ManagementStandard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Standard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagementStandard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    BaseUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PracticalExam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Mark = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    MarkAfterConversion = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticalExam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PracticalQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Mark = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    IsDiscountFromTotal = table.Column<bool>(type: "bit", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Main = table.Column<bool>(type: "bit", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticalQuestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    PublicationDate = table.Column<DateTime>(type: "date", nullable: false),
                    PublicationEndDate = table.Column<DateTime>(type: "date", nullable: true),
                    WorkStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    WorkEndDate = table.Column<DateTime>(type: "date", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    Default = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "source",
                columns: table => new
                {
                    Gender = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DateYear = table.Column<double>(type: "float", nullable: true),
                    DateMonth = table.Column<double>(type: "float", nullable: true),
                    Birthday = table.Column<double>(type: "float", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    State = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExamTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperAdmin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Show = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperAdmin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperAdminSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEnglish = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ImageUrlAr = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    SiteColor = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SecondarySiteColor = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperAdminSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AltText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Component = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Temp_Table1",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rownum = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TrainerRateMeasure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    FromRange = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    ToRange = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Measure = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerRateMeasure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AboutDicTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AboutDicId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutDicTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboutDicTranslation_AboutDicTranslation",
                        column: x => x.AboutDicId,
                        principalTable: "AboutDic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicSupervisionStandardTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicSupervisionStandardId = table.Column<int>(type: "int", nullable: false),
                    Standard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSupervisionStandardTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicSupervisionStandardTranslation_AcademicSupervisionStandard",
                        column: x => x.AcademicSupervisionStandardId,
                        principalTable: "AcademicSupervisionStandard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchTranslation_Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Generalization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    GeneralizationTypeId = table.Column<int>(type: "int", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generalization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Generalization_Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToId = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsExtraMobile = table.Column<bool>(type: "bit", nullable: true),
                    ExtraMobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalendarTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalendarId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarTranslation_Calendar",
                        column: x => x.CalendarId,
                        principalTable: "Calendar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsCateryTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CateryId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsCateryTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsCateryTranslation_CmsCatery",
                        column: x => x.CateryId,
                        principalTable: "CmsCatery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsPage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CateryId = table.Column<int>(type: "int", nullable: true),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowComment = table.Column<bool>(type: "bit", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShowInHomePage = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsPage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsPage_CmsCatery",
                        column: x => x.CateryId,
                        principalTable: "CmsCatery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    ShowInHomePage = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentType = table.Column<int>(type: "int", nullable: true),
                    ProjectCost = table.Column<double>(type: "float", nullable: true),
                    OneObjectFees = table.Column<double>(type: "float", nullable: true),
                    ProjectStatus = table.Column<int>(type: "int", nullable: true),
                    ProjectCategoryId = table.Column<int>(type: "int", nullable: true),
                    TargetQty = table.Column<int>(type: "int", nullable: true),
                    SecondDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsProject_CmsProjectCategory",
                        column: x => x.ProjectCategoryId,
                        principalTable: "CmsProjectCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsProjectCategoryTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectCategoryId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsProjectCategoryTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsProjectCategoryTranslation_CmsProjectCategory",
                        column: x => x.ProjectCategoryId,
                        principalTable: "CmsProjectCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsSliderTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsSliderTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsSliderTranslation_CmsSlider",
                        column: x => x.SliderId,
                        principalTable: "CmsSlider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsWhatPeopleSayTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeopleId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsWhatPeopleSayTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsWhatPeopleSayTranslation_CmsWhatPeopleSay",
                        column: x => x.PeopleId,
                        principalTable: "CmsWhatPeopleSay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationChannelTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommunicationChannelId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationChannelTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationChannelTranslation_CommunicationChannel",
                        column: x => x.CommunicationChannelId,
                        principalTable: "CommunicationChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubCommunicationChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommunicationChannelId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCommunicationChannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCommunicationChannel_CommunicationChannel",
                        column: x => x.CommunicationChannelId,
                        principalTable: "CommunicationChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubCommunicationChannel_SubCommunicationChannel",
                        column: x => x.ParentId,
                        principalTable: "SubCommunicationChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountryTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryTranslation_Country",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CourseName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CourseDuration = table.Column<int>(type: "int", nullable: true),
                    CoursePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AcquiredSkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    LearningMethodId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowInHomePage = table.Column<bool>(type: "bit", nullable: true),
                    NeedQuestion = table.Column<bool>(type: "bit", nullable: true),
                    QuestionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuccessMark = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    AssignmentMark = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    ListeningExamMark = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Course_CourseCategory",
                        column: x => x.CategoryId,
                        principalTable: "CourseCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseCategoryTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCategoryTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCategoryTranslation_CourseCategory",
                        column: x => x.CategoryId,
                        principalTable: "CourseCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoursePakagesTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoursePackagesId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    PackageName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePakagesTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursePakagesTranslation_CoursePackages",
                        column: x => x.CoursePackagesId,
                        principalTable: "CoursePackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyTranslation_Currency",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ManagementStandardTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagementStandardId = table.Column<int>(type: "int", nullable: false),
                    Standard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagementStandardTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagementStandardTranslation_ManagementStandard",
                        column: x => x.ManagementStandardId,
                        principalTable: "ManagementStandard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailsLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsLookup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailsLookup_MasterLookup",
                        column: x => x.MasterId,
                        principalTable: "MasterLookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterLookupTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterLookupId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterLookupTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterLookupTranslation_MasterLookup",
                        column: x => x.MasterLookupId,
                        principalTable: "MasterLookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModuleTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleTranslation_Modules",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PracticalExamTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracticalExamId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticalExamTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticalExamTranslation_PracticalExam",
                        column: x => x.PracticalExamId,
                        principalTable: "PracticalExam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PracticalExamQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    PracticalExamId = table.Column<int>(type: "int", nullable: false),
                    PracticalQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticalExamQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticalExamQuestion_PracticalExam",
                        column: x => x.PracticalExamId,
                        principalTable: "PracticalExam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PracticalExamQuestion_PracticalQuestion",
                        column: x => x.PracticalQuestionId,
                        principalTable: "PracticalQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PracticalQuestionTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracticalQuestionId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticalQuestionTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticalQuestionTranslation_PracticalQuestion",
                        column: x => x.PracticalQuestionId,
                        principalTable: "PracticalQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SemesterTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SemesterTranslation_Semester",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectTranslation_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    PermissionKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModuleId = table.Column<int>(type: "int", nullable: true),
                    SuperAdminId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_Modules",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permission_SuperAdmin",
                        column: x => x.SuperAdminId,
                        principalTable: "SuperAdmin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    SuperAdminId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemSetting_Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemSetting_SuperAdmin",
                        column: x => x.SuperAdminId,
                        principalTable: "SuperAdmin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemFile_SystemFile",
                        column: x => x.FileId,
                        principalTable: "SystemFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemFileTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemFileId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AltText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemFileTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemFileTranslation_SystemFile",
                        column: x => x.SystemFileId,
                        principalTable: "SystemFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemGroupTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemGroupId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemGroupTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemGroupTranslation_SystemGroup",
                        column: x => x.SystemGroupId,
                        principalTable: "SystemGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainerRateMeasureTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerRateMeasureId = table.Column<int>(type: "int", nullable: false),
                    Measure = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerRateMeasureTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerRateMeasureTranslation_TrainerRateMeasure",
                        column: x => x.TrainerRateMeasureId,
                        principalTable: "TrainerRateMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GeneralizationTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneralizationId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralizationTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralizationTranslation_Generalization",
                        column: x => x.GeneralizationId,
                        principalTable: "Generalization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsPageTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsPageTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsPageTranslation_CmsPage",
                        column: x => x.PageId,
                        principalTable: "CmsPage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsProjectCost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    IsOther = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsProjectCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsProjectCost_CmsProject",
                        column: x => x.ProjectId,
                        principalTable: "CmsProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsProjectResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsProjectResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsProjectResource_CmsProject",
                        column: x => x.ProjectId,
                        principalTable: "CmsProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsProjectTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsProjectTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsProjectTranslation_CmsProject",
                        column: x => x.ProjectId,
                        principalTable: "CmsProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubCommunicationChannelTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCommunicationChannelId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCommunicationChannelTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCommunicationChannelTranslation_SubCommunicationChannel",
                        column: x => x.SubCommunicationChannelId,
                        principalTable: "SubCommunicationChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CityTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityTranslation_City",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mobile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ThirdName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IdNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Language = table.Column<int>(type: "int", nullable: true),
                    NationalityId = table.Column<int>(type: "int", nullable: true),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: true),
                    PhoneNumberCode = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_City",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_Country",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignment_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertificateAdoption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    SemesterId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateAdoption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificateAdoption_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificateAdoption_Semester",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseMarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ValueTo = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseMarks_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoursePrerequisite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    PrerequisiteCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePrerequisite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrerequisiteCourse_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrerequisiteCourse_Course1",
                        column: x => x.PrerequisiteCourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    AcquiredSkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTranslation_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    FinalMark = table.Column<double>(type: "float", nullable: true),
                    MarkAfterConversion = table.Column<double>(type: "float", nullable: true),
                    Shuffle = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamTemplate_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamTemplate_CourseCategory",
                        column: x => x.CategoryId,
                        principalTable: "CourseCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PracticalExamCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    PracticalExamId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    SubjectMark = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticalExamCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticalExamCourse_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PracticalExamCourse_PracticalExam",
                        column: x => x.PracticalExamId,
                        principalTable: "PracticalExam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SectionOfCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    SectionName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionOfCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionOfCourse_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailsLookupTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetailsLookupId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsLookupTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailsLookupTranslation_DetailsLookup",
                        column: x => x.DetailsLookupId,
                        principalTable: "DetailsLookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemplateHtml",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RanderHtml = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateHtml", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateHtml_DetailsLookup",
                        column: x => x.TypeId,
                        principalTable: "DetailsLookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionTranslation_Permission",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permission",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_RolePermissions",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemSettingTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettingTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemSettingTranslation_SystemSetting",
                        column: x => x.SettingId,
                        principalTable: "SystemSetting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsProjectCostTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectCostId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsProjectCostTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsProjectCostTranslation_CmsProjectCost",
                        column: x => x.ProjectCostId,
                        principalTable: "CmsProjectCost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsProjectDonor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    ProjectCostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsProjectDonor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsProjectDonor_CmsProject",
                        column: x => x.ProjectId,
                        principalTable: "CmsProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CmsProjectDonor_CmsProjectCost",
                        column: x => x.ProjectCostId,
                        principalTable: "CmsProjectCost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AbsenceNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsPresent = table.Column<bool>(type: "bit", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    FromHour = table.Column<TimeSpan>(type: "time", nullable: true),
                    ToHour = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    ContactType = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    TypeText = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LogText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsForExtraType = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationLogs_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ThirdName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactTranslation_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactType_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    ContactType = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ImageLink = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    cycleLink = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Email_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    StartWorkDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    JobTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GeneralizationEmployee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneralizationId = table.Column<int>(type: "int", nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralizationEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralizationEmployee_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneralizationEmployee_Generalization",
                        column: x => x.GeneralizationId,
                        principalTable: "Generalization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneralizationId = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notification_Generalization",
                        column: x => x.GeneralizationId,
                        principalTable: "Generalization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Work = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EducationalLevelId = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WorkPlace = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ExtraMobile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CollegeId = table.Column<int>(type: "int", nullable: true),
                    SpecialtyId = table.Column<int>(type: "int", nullable: true),
                    IsExternalStudy = table.Column<bool>(type: "bit", nullable: true),
                    IsMedicalPast = table.Column<bool>(type: "bit", nullable: true),
                    IsFastSubscription = table.Column<bool>(type: "bit", nullable: true),
                    MedicalDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrainingConsultantId = table.Column<int>(type: "int", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trainer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    StartWorkDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    GeneralSpecialtyId = table.Column<int>(type: "int", nullable: true),
                    WorkHouers = table.Column<int>(type: "int", nullable: true),
                    UserProfileId = table.Column<int>(type: "int", nullable: true),
                    IsUser = table.Column<bool>(type: "bit", nullable: true),
                    IsFullTimeWorker = table.Column<bool>(type: "bit", nullable: true),
                    ShowInPages = table.Column<bool>(type: "bit", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Signature = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainer_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProfilePhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferedLanguageId = table.Column<int>(type: "int", nullable: true),
                    StartWorkDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    EmployeeColorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfile_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentTranslation_Assignment",
                        column: x => x.AssignmentId,
                        principalTable: "Assignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseMarkTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseMarkId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMarkTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseMarkTranslation_CourseMarks",
                        column: x => x.CourseMarkId,
                        principalTable: "CourseMarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamTemplateTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTemplateTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamTemplateTranslation_ExamTemplate",
                        column: x => x.ExamId,
                        principalTable: "ExamTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PracticalExamCourseSubject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    PracticalExamCourseId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticalExamCourseSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticalExamCourseSubject_PracticalExamCourse",
                        column: x => x.PracticalExamCourseId,
                        principalTable: "PracticalExamCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PracticalExamCourseSubject_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lecture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LectureName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecture_SectionOfCourse",
                        column: x => x.SectionId,
                        principalTable: "SectionOfCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SectionOfCourseTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionOfCourseTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionOfCourseTranslation_SectionOfCourse",
                        column: x => x.SectionId,
                        principalTable: "SectionOfCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseCertification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    TemplateHtmlId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCertification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCertification_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseCertification_TemplateHtml",
                        column: x => x.TemplateHtmlId,
                        principalTable: "TemplateHtml",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeTranslation_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentNotes_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    BirthPlace = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Work = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    WorkPlace = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentTranslation_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollTeacherCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CourseName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LearningMethodId = table.Column<int>(type: "int", nullable: true),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    SemesterId = table.Column<int>(type: "int", nullable: true),
                    SectionName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PublicationDate = table.Column<DateTime>(type: "date", nullable: false),
                    PublicationEndDate = table.Column<DateTime>(type: "date", nullable: true),
                    WorkStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    WorkEndDate = table.Column<DateTime>(type: "date", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    CountOfStudent = table.Column<int>(type: "int", nullable: true),
                    AgeAllowedForRegistration = table.Column<int>(type: "int", nullable: false),
                    AgeGroup = table.Column<int>(type: "int", nullable: false),
                    NotesForEnrolled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculationTypeId = table.Column<int>(type: "int", nullable: true),
                    IsCourseDone = table.Column<bool>(type: "bit", nullable: true),
                    CertificateAdoption = table.Column<bool>(type: "bit", nullable: true),
                    AgeGroupTo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollTeacherCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollTeacherCourse_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollTeacherCourse_Semester",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollTeacherCourse_Trainer",
                        column: x => x.TeacherId,
                        principalTable: "Trainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Mark = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CertifiedBankQuestion = table.Column<bool>(type: "bit", nullable: true),
                    TeacherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Trainer",
                        column: x => x.TeacherId,
                        principalTable: "Trainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainerTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerTranslation_Trainer",
                        column: x => x.TrainerId,
                        principalTable: "Trainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemGroupUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemGroupId = table.Column<int>(type: "int", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemGroupUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemGroupUsers_SystemGroup",
                        column: x => x.SystemGroupId,
                        principalTable: "SystemGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemGroupUsers_UserProfile",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    UserProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileTranslation_UserProfile",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LectureId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseResource_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseResource_Lecture",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LectureTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LectureName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LectureId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureTranslation_Lecture",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SectionOfCourseQuiz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    SectionOfCourseId = table.Column<int>(type: "int", nullable: true),
                    LectureId = table.Column<int>(type: "int", nullable: true),
                    QuestionOne = table.Column<bool>(type: "bit", nullable: true),
                    QuestionTwo = table.Column<bool>(type: "bit", nullable: true),
                    QuestionThree = table.Column<bool>(type: "bit", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionOfCourseQuiz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionOfCourseQuiz_Lecture",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectionOfCourseQuiz_SectionOfCourse",
                        column: x => x.SectionOfCourseId,
                        principalTable: "SectionOfCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentNotesTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentNoteId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentNotesTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentNotesTranslation_StudentNotes",
                        column: x => x.StudentNoteId,
                        principalTable: "StudentNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicSupervisionRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: true),
                    StandardId = table.Column<int>(type: "int", nullable: true),
                    Rate = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSupervisionRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicSupervisionRate_AcademicSupervisionStandard",
                        column: x => x.StandardId,
                        principalTable: "AcademicSupervisionStandard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AcademicSupervisionRate_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoursePackagesRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoursePackagesId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePackagesRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursePackagesRelations_Course",
                        column: x => x.CourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoursePackagesRelations_CoursePackages",
                        column: x => x.CoursePackagesId,
                        principalTable: "CoursePackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EnrollCourseId = table.Column<int>(type: "int", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollAssignment_EnrollCourseExam",
                        column: x => x.EnrollCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseAllowUserRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    RateTypeId = table.Column<int>(type: "int", nullable: true),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseAllowUserRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseAllowUserRates_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollCourseAllowUserRates_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseAssigment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseAssigment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseAssigment_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseQuiz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: true),
                    LectureId = table.Column<int>(type: "int", nullable: true),
                    QuestionOne = table.Column<bool>(type: "bit", nullable: true),
                    QuestionTwo = table.Column<bool>(type: "bit", nullable: true),
                    QuestionThree = table.Column<bool>(type: "bit", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseQuiz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseQuiz_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollCourseQuiz_Lecture",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollCourseId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    FromTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    ToTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LearningMethodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseTime_EnrollTeacherCourse",
                        column: x => x.EnrollCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseTimeCustomization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollCourseId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    FromTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    ToTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LearningMethodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseTimeCustomization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseTimeCustomization_EnrollTeacherCourse",
                        column: x => x.EnrollCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollSectionOfCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    SectionName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    EnrollCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollSectionOfCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollSectionOfCourse_EnrollTeacherCourse",
                        column: x => x.EnrollCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollStudentCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsPass = table.Column<bool>(type: "bit", nullable: true),
                    Mark = table.Column<double>(type: "float", nullable: true),
                    NeedApproval = table.Column<bool>(type: "bit", nullable: true),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,8)", nullable: true),
                    CustomerCurrencyCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExpelledDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollStudentCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollStudentCourse_EnrollTeacherCourse",
                        column: x => x.CourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollStudentCourse_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollTeacherCourseTranlation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SectionName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    EnrollCourseId = table.Column<int>(type: "int", nullable: false),
                    NotesForEnrolled = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollTeacherCourseTranlation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollTeacherCourseTranlation_EnrollTeacherCourse",
                        column: x => x.EnrollCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoicesPay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactID = table.Column<int>(type: "int", nullable: false),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: false),
                    ProcessStatus = table.Column<int>(type: "int", nullable: false),
                    ProcessDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReceiptNo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,8)", nullable: true),
                    CustomerCurrencyCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicesPay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicesPay_Contact",
                        column: x => x.ContactID,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoicesPay_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ManagementRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: true),
                    Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagementRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagementRate_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarkAdoptionForExam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamTemplateId = table.Column<int>(type: "int", nullable: true),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkAdoptionForExam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarkAdoptionForExam_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarkAdoptionForExam_ExamTemplate",
                        column: x => x.ExamTemplateId,
                        principalTable: "ExamTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarkAdoptionForPracticalExam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracticalExamId = table.Column<int>(type: "int", nullable: true),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkAdoptionForPracticalExam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarkAdoptionForPracticalExam_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarkAdoptionForPracticalExam_PracticalExam",
                        column: x => x.PracticalExamId,
                        principalTable: "PracticalExam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PracticalEnrollmentExam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PracticalExamId = table.Column<int>(type: "int", nullable: true),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    MarkAdoption = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticalEnrollmentExam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticalEnrollmentExam_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PracticalEnrollmentExam_PracticalExam",
                        column: x => x.PracticalExamId,
                        principalTable: "PracticalExam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SenangPay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ProcessDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Msg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenangPayPaymentType = table.Column<int>(type: "int", nullable: false),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    ProjectCostId = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,8)", nullable: true),
                    CustomerCurrencyCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenangPay", x => x.Id);
                    table.ForeignKey(
                        name: "FK__SenangPay__Enrol__30E33A54",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SenangPay_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SenangPay_CmsProject",
                        column: x => x.ProjectId,
                        principalTable: "CmsProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SenangPay_CmsProjectCost",
                        column: x => x.ProjectCostId,
                        principalTable: "CmsProjectCost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentAttendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsPresent = table.Column<bool>(type: "bit", nullable: true),
                    AbsenceNote = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAttendance_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAttendance_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentWayId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    CurrencyRate = table.Column<double>(type: "float", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "decimal(9,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentSubscription_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherAttendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: false),
                    Attended = table.Column<bool>(type: "bit", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherAttendances_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    TemplateId = table.Column<int>(type: "int", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamQuestion_ExamTemplate",
                        column: x => x.TemplateId,
                        principalTable: "ExamTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamQuestion_Question",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOption_Question",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionTranslation_Question",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseResourceTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    CourseResourceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseResourceTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseResourceTranslation_CourseResource",
                        column: x => x.CourseResourceId,
                        principalTable: "CourseResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollAssignmentTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollAssignmentId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollAssignmentTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollAssignmentTranslation_EnrollAssignment",
                        column: x => x.EnrollAssignmentId,
                        principalTable: "EnrollAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseAssigmentQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EnrollCourseAssigmentId = table.Column<int>(type: "int", nullable: false),
                    QuestionName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    QuestionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseAssigmentQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseAssigmentQuestion_EnrollCourseAssigment",
                        column: x => x.EnrollCourseAssigmentId,
                        principalTable: "EnrollCourseAssigment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseAssigmentTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollCourseAssigmentId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseAssigmentTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseAssigmentTranslation_EnrollCourseAssigment",
                        column: x => x.EnrollCourseAssigmentId,
                        principalTable: "EnrollCourseAssigment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollLecture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LectureName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    EnrollSectionId = table.Column<int>(type: "int", nullable: false),
                    EnrollCourseId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollLecture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollLecture_EnrollSectionOfCourse",
                        column: x => x.EnrollSectionId,
                        principalTable: "EnrollSectionOfCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollLecture_EnrollTeacherCourse",
                        column: x => x.EnrollCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollSectionOfCourseTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    EnrollSectionId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollSectionOfCourseTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollSectionOfCourseTranslation_EnrollSectionOfCourse",
                        column: x => x.EnrollSectionId,
                        principalTable: "EnrollSectionOfCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseAttendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollStudentCourseId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsPresent = table.Column<bool>(type: "bit", nullable: true),
                    AbsenceNote = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAttendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseAttendance_EnrollStudentCourse",
                        column: x => x.EnrollStudentCourseId,
                        principalTable: "EnrollStudentCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseAttendance_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseQuizPointes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EnrollStudentCourseId = table.Column<int>(type: "int", nullable: true),
                    EnrollCourseQuizId = table.Column<int>(type: "int", nullable: true),
                    QuestionOne = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    QuestionTwo = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    QuestionThree = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseQuizPointes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseQuizPointes_EnrollCourseQuiz",
                        column: x => x.EnrollCourseQuizId,
                        principalTable: "EnrollCourseQuiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollCourseQuizPointes_EnrollStudentCourse",
                        column: x => x.EnrollStudentCourseId,
                        principalTable: "EnrollStudentCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollStudentAlert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlertTypeId = table.Column<int>(type: "int", nullable: true),
                    EnrollStudentCourseId = table.Column<int>(type: "int", nullable: true),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollStudentAlert", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollStudentAlert_EnrollStudentCourse",
                        column: x => x.EnrollStudentCourseId,
                        principalTable: "EnrollStudentCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollStudentAlert_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollStudentAssigment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EnrollCourseAssigmentId = table.Column<int>(type: "int", nullable: false),
                    EnrollStudentCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollStudentAssigment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollStudentAssigment_EnrollCourseAssigment",
                        column: x => x.EnrollCourseAssigmentId,
                        principalTable: "EnrollCourseAssigment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollStudentAssigment_EnrollStudentCourse",
                        column: x => x.EnrollStudentCourseId,
                        principalTable: "EnrollStudentCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollStudentCourseAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollStudentCourseId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FileAttached = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollStudentCourseAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollStudentCourseAttachment_EnrollStudentCourse",
                        column: x => x.EnrollStudentCourseId,
                        principalTable: "EnrollStudentCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentBalanceHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    EnrollStudentCourseId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentBalanceHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentBalanceHistory_EnrollStudentCourse",
                        column: x => x.EnrollStudentCourseId,
                        principalTable: "EnrollStudentCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentBalanceHistory_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentExpulsionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    EnrollStudentCourseId = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExpulsionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentExpulsionHistory_EnrollStudentCourse",
                        column: x => x.EnrollStudentCourseId,
                        principalTable: "EnrollStudentCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentExpulsionHistory_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ManagementRateLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagementRateId = table.Column<int>(type: "int", nullable: true),
                    StandardId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagementRateLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagementRateLine_ManagementRate",
                        column: x => x.ManagementRateId,
                        principalTable: "ManagementRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManagementRateLine_Standard",
                        column: x => x.StandardId,
                        principalTable: "ManagementStandard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PracticalEnrollmentExamStudent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PracticalEnrollmentExamId = table.Column<int>(type: "int", nullable: true),
                    EnrollStudentCourseId = table.Column<int>(type: "int", nullable: true),
                    Mark = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    MarkAfterConversion = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticalEnrollmentExamStudent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticalEnrollmentExamStudent_EnrollStudentCourse",
                        column: x => x.EnrollStudentCourseId,
                        principalTable: "EnrollStudentCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PracticalEnrollmentExamStudent_PracticalEnrollmentExam",
                        column: x => x.PracticalEnrollmentExamId,
                        principalTable: "PracticalEnrollmentExam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PracticalEnrollmentExamTrainer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracticalEnrollmentExamId = table.Column<int>(type: "int", nullable: true),
                    TrainerId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticalEnrollmentExamTrainer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticalEnrollmentExamTrainer_PracticalEnrollmentExam",
                        column: x => x.PracticalEnrollmentExamId,
                        principalTable: "PracticalEnrollmentExam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PracticalEnrollmentExamTrainer_Trainer",
                        column: x => x.TrainerId,
                        principalTable: "Trainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptionTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptionTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptionTranslation_QuestionOption",
                        column: x => x.OptionId,
                        principalTable: "QuestionOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseAssigmentQuestionOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseAssigmentQuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseAssigmentQuestionOption_EnrollCourseAssigmentQuestion",
                        column: x => x.QuestionId,
                        principalTable: "EnrollCourseAssigmentQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseAssigmentQuestionTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseAssigmentQuestionTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseAssigmentQuestionTranslation_EnrollCourseAssigmentQuestion",
                        column: x => x.QuestionId,
                        principalTable: "EnrollCourseAssigmentQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseExam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    EnrollTeacherCourseId = table.Column<int>(type: "int", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PublishEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPrerequisite = table.Column<bool>(type: "bit", nullable: true),
                    ExamTemplateId = table.Column<int>(type: "int", nullable: true),
                    TestTypeID = table.Column<int>(type: "int", nullable: true),
                    EnrollLectureID = table.Column<int>(type: "int", nullable: true),
                    Shuffle = table.Column<bool>(type: "bit", nullable: true),
                    MarkAdoption = table.Column<bool>(type: "bit", nullable: true),
                    ExamFinalMark = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseExam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseExam_EnrollLecture",
                        column: x => x.EnrollLectureID,
                        principalTable: "EnrollLecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollCourseExam_EnrollTeacherCourse",
                        column: x => x.EnrollTeacherCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollCourseExam_ExamTemplate",
                        column: x => x.ExamTemplateId,
                        principalTable: "ExamTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EnrollLectureId = table.Column<int>(type: "int", nullable: false),
                    EnrollCourseId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseResource_EnrollLecture",
                        column: x => x.EnrollLectureId,
                        principalTable: "EnrollLecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollCourseResource_EnrollTeacherCourse",
                        column: x => x.EnrollCourseId,
                        principalTable: "EnrollTeacherCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollLectureTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LectureName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    EnrollLectureId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollLectureTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollLectureTranslation_EnrollLecture",
                        column: x => x.EnrollLectureId,
                        principalTable: "EnrollLecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollStudentAssigmentAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EnrollCourseAssigmentQuestionId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EnrollStudentAssigmentId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollStudentAssigmentAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollStudentAssigmentAnswer_EnrollCourseAssigmentQuestion",
                        column: x => x.EnrollCourseAssigmentQuestionId,
                        principalTable: "EnrollCourseAssigmentQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollStudentAssigmentAnswer_EnrollStudentAssigment",
                        column: x => x.EnrollStudentAssigmentId,
                        principalTable: "EnrollStudentAssigment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PracticalEnrollmentExamStudentSubject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PracticalEnrollmentExamStudentId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    Mark = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    DisountMarkTotal = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticalEnrollmentExamStudentSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticalEnrollmentExamStudentSubject_PracticalEnrollmentExamStudent",
                        column: x => x.PracticalEnrollmentExamStudentId,
                        principalTable: "PracticalEnrollmentExamStudent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PracticalEnrollmentExamStudentSubject_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseAssigmentQuestionOptionTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseAssigmentQuestionOptionTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseAssigmentQuestionOptionTranslation_EnrollCourseAssigmentQuestionOption",
                        column: x => x.OptionId,
                        principalTable: "EnrollCourseAssigmentQuestionOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseExamQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EnrollCourseExamId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Mark = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseExamQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseExamQuestion_EnrollCourseExam",
                        column: x => x.EnrollCourseExamId,
                        principalTable: "EnrollCourseExam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollCourseExamQuestion_ExamQuestion",
                        column: x => x.QuestionId,
                        principalTable: "ExamQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseExamTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollCourseExamId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseExamTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseExamTranslation_EnrollCourseExam",
                        column: x => x.EnrollCourseExamId,
                        principalTable: "EnrollCourseExam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollStudentExam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    FinalMark = table.Column<double>(type: "float", nullable: true),
                    MarkHeGot = table.Column<double>(type: "float", nullable: true),
                    MarkAfterConversion = table.Column<double>(type: "float", nullable: true),
                    EnrollCourseExamId = table.Column<int>(type: "int", nullable: false),
                    EnrollStudentCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollStudentExam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollStudentExam_EnrollCourseExam",
                        column: x => x.EnrollCourseExamId,
                        principalTable: "EnrollCourseExam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollStudentExam_Student",
                        column: x => x.EnrollStudentCourseId,
                        principalTable: "EnrollStudentCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourseResourceTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    EnrollCourseResourceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourseResourceTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollCourseResourceTranslation_EnrollCourseResource",
                        column: x => x.EnrollCourseResourceId,
                        principalTable: "EnrollCourseResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollStudentAssigmentAnswerOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EnrollStudentAssigmentAnswerId = table.Column<int>(type: "int", nullable: false),
                    QusetionOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollStudentAssigmentAnswerOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollStudentAssigmentAnswerOption_EnrollStudentAssigmentAnswer",
                        column: x => x.EnrollStudentAssigmentAnswerId,
                        principalTable: "EnrollStudentAssigmentAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PracticalEnrollmentExamStudentSubjectRating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PracticalEnrollmentExamStudentSubjectId = table.Column<int>(type: "int", nullable: false),
                    PracticalQuestionId = table.Column<int>(type: "int", nullable: false),
                    Mark = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NoOfErrors = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticalEnrollmentExamStudentSubjectRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticalEnrollmentExamStudentSubjectRating_PracticalEnrollmentExamStudentSubject",
                        column: x => x.PracticalEnrollmentExamStudentSubjectId,
                        principalTable: "PracticalEnrollmentExamStudentSubject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PracticalEnrollmentExamStudentSubjectRating_PracticalQuestion",
                        column: x => x.PracticalQuestionId,
                        principalTable: "PracticalQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollStudentExamAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EnrollCourseExamQuestionId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Mark = table.Column<double>(type: "float", nullable: false),
                    EnrollStudentExamId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollStudentExamAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollStudentExamQuestion_EnrollStudentExam",
                        column: x => x.EnrollStudentExamId,
                        principalTable: "EnrollStudentExam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollStudentExamQuestion_ExamQuestion",
                        column: x => x.EnrollCourseExamQuestionId,
                        principalTable: "EnrollCourseExamQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrollStudentExamAnswerOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    EnrollStudentExamAnswerId = table.Column<int>(type: "int", nullable: false),
                    QusetionOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollStudentExamAnswerOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollStudentExamAnswerOption_EnrollStudentExamQuestion",
                        column: x => x.EnrollStudentExamAnswerId,
                        principalTable: "EnrollStudentExamAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollStudentExamAnswerOption_QuestionOption",
                        column: x => x.QusetionOptionId,
                        principalTable: "QuestionOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AboutDicTranslation_AboutDicId",
                table: "AboutDicTranslation",
                column: "AboutDicId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSupervisionRate_EnrollTeacherCourseId",
                table: "AcademicSupervisionRate",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSupervisionRate_StandardId",
                table: "AcademicSupervisionRate",
                column: "StandardId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSupervisionStandardTranslation_AcademicSupervisionStandardId",
                table: "AcademicSupervisionStandardTranslation",
                column: "AcademicSupervisionStandardId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "([NormalizedName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_CourseId",
                table: "Assignment",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentTranslation_AssignmentId",
                table: "AssignmentTranslation",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_ContactId",
                table: "Attendance",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchTranslation_BranchId",
                table: "BranchTranslation",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarTranslation_CalendarId",
                table: "CalendarTranslation",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateAdoption_CourseId",
                table: "CertificateAdoption",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateAdoption_SemesterId",
                table: "CertificateAdoption",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CityTranslation_CityId",
                table: "CityTranslation",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsCatery_ParentId",
                table: "CmsCatery",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsCateryTranslation_CateryId",
                table: "CmsCateryTranslation",
                column: "CateryId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsPage_CateryId",
                table: "CmsPage",
                column: "CateryId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsPageTranslation_PageId",
                table: "CmsPageTranslation",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsProject_ProjectCategoryId",
                table: "CmsProject",
                column: "ProjectCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsProjectCategoryTranslation_ProjectCategoryId",
                table: "CmsProjectCategoryTranslation",
                column: "ProjectCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsProjectCost_ProjectId",
                table: "CmsProjectCost",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsProjectCostTranslation_ProjectCostId",
                table: "CmsProjectCostTranslation",
                column: "ProjectCostId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsProjectDonor_ProjectCostId",
                table: "CmsProjectDonor",
                column: "ProjectCostId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsProjectDonor_ProjectId",
                table: "CmsProjectDonor",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsProjectResource_ProjectId",
                table: "CmsProjectResource",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsProjectTranslation_ProjectId",
                table: "CmsProjectTranslation",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsSliderTranslation_SliderId",
                table: "CmsSliderTranslation",
                column: "SliderId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsWhatPeopleSayTranslation_PeopleId",
                table: "CmsWhatPeopleSayTranslation",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationChannelTranslation_CommunicationChannelId",
                table: "CommunicationChannelTranslation",
                column: "CommunicationChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationLogs_ContactId",
                table: "CommunicationLogs",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_BranchId",
                table: "Contact",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CityId",
                table: "Contact",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CountryId",
                table: "Contact",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactTranslation_ContactId",
                table: "ContactTranslation",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactType_ContactId",
                table: "ContactType",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryTranslation_CountryId",
                table: "CountryTranslation",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_CategoryId",
                table: "Course",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseAttendance_EnrollStudentCourseId",
                table: "CourseAttendance",
                column: "EnrollStudentCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseAttendance_EnrollTeacherCourseId",
                table: "CourseAttendance",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCategory_ParentId",
                table: "CourseCategory",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCategoryTranslation_CategoryId",
                table: "CourseCategoryTranslation",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCertification_CourseId",
                table: "CourseCertification",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCertification_TemplateHtmlId",
                table: "CourseCertification",
                column: "TemplateHtmlId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMarks_CourseId",
                table: "CourseMarks",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMarkTranslation_CourseMarkId",
                table: "CourseMarkTranslation",
                column: "CourseMarkId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePackagesRelations_CourseId",
                table: "CoursePackagesRelations",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePackagesRelations_CoursePackagesId",
                table: "CoursePackagesRelations",
                column: "CoursePackagesId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePakagesTranslation_CoursePackagesId",
                table: "CoursePakagesTranslation",
                column: "CoursePackagesId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePrerequisite_CourseId",
                table: "CoursePrerequisite",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePrerequisite_PrerequisiteCourseId",
                table: "CoursePrerequisite",
                column: "PrerequisiteCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseResource_CourseId",
                table: "CourseResource",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseResource_LectureId",
                table: "CourseResource",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseResourceTranslation_CourseResourceId",
                table: "CourseResourceTranslation",
                column: "CourseResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTranslation_CourseId",
                table: "CourseTranslation",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyTranslation_CurrencyId",
                table: "CurrencyTranslation",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsLookup_MasterId",
                table: "DetailsLookup",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsLookupTranslation_DetailsLookupId",
                table: "DetailsLookupTranslation",
                column: "DetailsLookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Email_ContactId",
                table: "Email",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ContactId",
                table: "Employee",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTranslation_EmployeeId",
                table: "EmployeeTranslation",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollAssignment_EnrollCourseId",
                table: "EnrollAssignment",
                column: "EnrollCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollAssignmentTranslation_EnrollAssignmentId",
                table: "EnrollAssignmentTranslation",
                column: "EnrollAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseAllowUserRates_ContactId",
                table: "EnrollCourseAllowUserRates",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseAllowUserRates_EnrollTeacherCourseId",
                table: "EnrollCourseAllowUserRates",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseAssigment_EnrollTeacherCourseId",
                table: "EnrollCourseAssigment",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseAssigmentQuestion_EnrollCourseAssigmentId",
                table: "EnrollCourseAssigmentQuestion",
                column: "EnrollCourseAssigmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseAssigmentQuestionOption_QuestionId",
                table: "EnrollCourseAssigmentQuestionOption",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseAssigmentQuestionOptionTranslation_OptionId",
                table: "EnrollCourseAssigmentQuestionOptionTranslation",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseAssigmentQuestionTranslation_QuestionId",
                table: "EnrollCourseAssigmentQuestionTranslation",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseAssigmentTranslation_EnrollCourseAssigmentId",
                table: "EnrollCourseAssigmentTranslation",
                column: "EnrollCourseAssigmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseExam_EnrollLectureID",
                table: "EnrollCourseExam",
                column: "EnrollLectureID");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseExam_EnrollTeacherCourseId",
                table: "EnrollCourseExam",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseExam_ExamTemplateId",
                table: "EnrollCourseExam",
                column: "ExamTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseExamQuestion_EnrollCourseExamId",
                table: "EnrollCourseExamQuestion",
                column: "EnrollCourseExamId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseExamQuestion_QuestionId",
                table: "EnrollCourseExamQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseExamTranslation_EnrollCourseExamId",
                table: "EnrollCourseExamTranslation",
                column: "EnrollCourseExamId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseQuiz_EnrollTeacherCourseId",
                table: "EnrollCourseQuiz",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseQuiz_LectureId",
                table: "EnrollCourseQuiz",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseQuizPointes_EnrollCourseQuizId",
                table: "EnrollCourseQuizPointes",
                column: "EnrollCourseQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseQuizPointes_EnrollStudentCourseId",
                table: "EnrollCourseQuizPointes",
                column: "EnrollStudentCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseResource_EnrollCourseId",
                table: "EnrollCourseResource",
                column: "EnrollCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseResource_EnrollLectureId",
                table: "EnrollCourseResource",
                column: "EnrollLectureId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseResourceTranslation_EnrollCourseResourceId",
                table: "EnrollCourseResourceTranslation",
                column: "EnrollCourseResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseTime_EnrollCourseId",
                table: "EnrollCourseTime",
                column: "EnrollCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourseTimeCustomization_EnrollCourseId",
                table: "EnrollCourseTimeCustomization",
                column: "EnrollCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollLecture_EnrollCourseId",
                table: "EnrollLecture",
                column: "EnrollCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollLecture_EnrollSectionId",
                table: "EnrollLecture",
                column: "EnrollSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollLectureTranslation_EnrollLectureId",
                table: "EnrollLectureTranslation",
                column: "EnrollLectureId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollSectionOfCourse_EnrollCourseId",
                table: "EnrollSectionOfCourse",
                column: "EnrollCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollSectionOfCourseTranslation_EnrollSectionId",
                table: "EnrollSectionOfCourseTranslation",
                column: "EnrollSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentAlert_EnrollStudentCourseId",
                table: "EnrollStudentAlert",
                column: "EnrollStudentCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentAlert_EnrollTeacherCourseId",
                table: "EnrollStudentAlert",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentAssigment_EnrollCourseAssigmentId",
                table: "EnrollStudentAssigment",
                column: "EnrollCourseAssigmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentAssigment_EnrollStudentCourseId",
                table: "EnrollStudentAssigment",
                column: "EnrollStudentCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentAssigmentAnswer_EnrollCourseAssigmentQuestionId",
                table: "EnrollStudentAssigmentAnswer",
                column: "EnrollCourseAssigmentQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentAssigmentAnswer_EnrollStudentAssigmentId",
                table: "EnrollStudentAssigmentAnswer",
                column: "EnrollStudentAssigmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentAssigmentAnswerOption_EnrollStudentAssigmentAnswerId",
                table: "EnrollStudentAssigmentAnswerOption",
                column: "EnrollStudentAssigmentAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentCourse_CourseId",
                table: "EnrollStudentCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentCourse_StudentId",
                table: "EnrollStudentCourse",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentCourseAttachment_EnrollStudentCourseId",
                table: "EnrollStudentCourseAttachment",
                column: "EnrollStudentCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentExam_EnrollCourseExamId",
                table: "EnrollStudentExam",
                column: "EnrollCourseExamId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentExam_EnrollStudentCourseId",
                table: "EnrollStudentExam",
                column: "EnrollStudentCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentExamAnswer_EnrollCourseExamQuestionId",
                table: "EnrollStudentExamAnswer",
                column: "EnrollCourseExamQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentExamAnswer_EnrollStudentExamId",
                table: "EnrollStudentExamAnswer",
                column: "EnrollStudentExamId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentExamAnswerOption_EnrollStudentExamAnswerId",
                table: "EnrollStudentExamAnswerOption",
                column: "EnrollStudentExamAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollStudentExamAnswerOption_QusetionOptionId",
                table: "EnrollStudentExamAnswerOption",
                column: "QusetionOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollTeacherCourse_CourseId",
                table: "EnrollTeacherCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollTeacherCourse_SemesterId",
                table: "EnrollTeacherCourse",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollTeacherCourse_TeacherId",
                table: "EnrollTeacherCourse",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollTeacherCourseTranlation_EnrollCourseId",
                table: "EnrollTeacherCourseTranlation",
                column: "EnrollCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestion_QuestionId",
                table: "ExamQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestion_TemplateId",
                table: "ExamQuestion",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTemplate_CategoryId",
                table: "ExamTemplate",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTemplate_CourseId",
                table: "ExamTemplate",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTemplateTranslation_ExamId",
                table: "ExamTemplateTranslation",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Generalization_BranchId",
                table: "Generalization",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralizationEmployee_ContactId",
                table: "GeneralizationEmployee",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralizationEmployee_GeneralizationId",
                table: "GeneralizationEmployee",
                column: "GeneralizationId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralizationTranslation_GeneralizationId",
                table: "GeneralizationTranslation",
                column: "GeneralizationId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicesPay_ContactID",
                table: "InvoicesPay",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicesPay_EnrollTeacherCourseId",
                table: "InvoicesPay",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemFile_FileId",
                table: "ItemFile",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecture_SectionId",
                table: "Lecture",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureTranslation_LectureId",
                table: "LectureTranslation",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagementRate_EnrollTeacherCourseId",
                table: "ManagementRate",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagementRateLine_ManagementRateId",
                table: "ManagementRateLine",
                column: "ManagementRateId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagementRateLine_StandardId",
                table: "ManagementRateLine",
                column: "StandardId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagementStandardTranslation_ManagementStandardId",
                table: "ManagementStandardTranslation",
                column: "ManagementStandardId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkAdoptionForExam_EnrollTeacherCourseId",
                table: "MarkAdoptionForExam",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkAdoptionForExam_ExamTemplateId",
                table: "MarkAdoptionForExam",
                column: "ExamTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkAdoptionForPracticalExam_EnrollTeacherCourseId",
                table: "MarkAdoptionForPracticalExam",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkAdoptionForPracticalExam_PracticalExamId",
                table: "MarkAdoptionForPracticalExam",
                column: "PracticalExamId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterLookupTranslation_MasterLookupId",
                table: "MasterLookupTranslation",
                column: "MasterLookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_BranchId",
                table: "Messages",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleTranslation_ModuleId",
                table: "ModuleTranslation",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ContactId",
                table: "Notification",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_GeneralizationId",
                table: "Notification",
                column: "GeneralizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_ModuleId",
                table: "Permission",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_SuperAdminId",
                table: "Permission",
                column: "SuperAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionTranslation_PermissionId",
                table: "PermissionTranslation",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalEnrollmentExam_EnrollTeacherCourseId",
                table: "PracticalEnrollmentExam",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalEnrollmentExam_PracticalExamId",
                table: "PracticalEnrollmentExam",
                column: "PracticalExamId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalEnrollmentExamStudent_EnrollStudentCourseId",
                table: "PracticalEnrollmentExamStudent",
                column: "EnrollStudentCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalEnrollmentExamStudent_PracticalEnrollmentExamId",
                table: "PracticalEnrollmentExamStudent",
                column: "PracticalEnrollmentExamId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalEnrollmentExamStudentSubject_PracticalEnrollmentExamStudentId",
                table: "PracticalEnrollmentExamStudentSubject",
                column: "PracticalEnrollmentExamStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalEnrollmentExamStudentSubject_SubjectId",
                table: "PracticalEnrollmentExamStudentSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalEnrollmentExamStudentSubjectRating_PracticalEnrollmentExamStudentSubjectId",
                table: "PracticalEnrollmentExamStudentSubjectRating",
                column: "PracticalEnrollmentExamStudentSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalEnrollmentExamStudentSubjectRating_PracticalQuestionId",
                table: "PracticalEnrollmentExamStudentSubjectRating",
                column: "PracticalQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalEnrollmentExamTrainer_PracticalEnrollmentExamId",
                table: "PracticalEnrollmentExamTrainer",
                column: "PracticalEnrollmentExamId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalEnrollmentExamTrainer_TrainerId",
                table: "PracticalEnrollmentExamTrainer",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalExamCourse_CourseId",
                table: "PracticalExamCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalExamCourse_PracticalExamId",
                table: "PracticalExamCourse",
                column: "PracticalExamId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalExamCourseSubject_PracticalExamCourseId",
                table: "PracticalExamCourseSubject",
                column: "PracticalExamCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalExamCourseSubject_SubjectId",
                table: "PracticalExamCourseSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalExamQuestion_PracticalExamId",
                table: "PracticalExamQuestion",
                column: "PracticalExamId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalExamQuestion_PracticalQuestionId",
                table: "PracticalExamQuestion",
                column: "PracticalQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalExamTranslation_PracticalExamId",
                table: "PracticalExamTranslation",
                column: "PracticalExamId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticalQuestionTranslation_PracticalQuestionId",
                table: "PracticalQuestionTranslation",
                column: "PracticalQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_TeacherId",
                table: "Question",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOption_QuestionId",
                table: "QuestionOption",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptionTranslation_OptionId",
                table: "QuestionOptionTranslation",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTranslation_QuestionId",
                table: "QuestionTranslation",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionOfCourse_CourseId",
                table: "SectionOfCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionOfCourseQuiz_LectureId",
                table: "SectionOfCourseQuiz",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionOfCourseQuiz_SectionOfCourseId",
                table: "SectionOfCourseQuiz",
                column: "SectionOfCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionOfCourseTranslation_SectionId",
                table: "SectionOfCourseTranslation",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterTranslation_SemesterId",
                table: "SemesterTranslation",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_SenangPay_ApplicationUserId",
                table: "SenangPay",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SenangPay_EnrollTeacherCourseId",
                table: "SenangPay",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SenangPay_ProjectCostId",
                table: "SenangPay",
                column: "ProjectCostId");

            migrationBuilder.CreateIndex(
                name: "IX_SenangPay_ProjectId",
                table: "SenangPay",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_ContactId",
                table: "Student",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendance_EnrollTeacherCourseId",
                table: "StudentAttendance",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendance_StudentId",
                table: "StudentAttendance",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBalanceHistory_EnrollStudentCourseId",
                table: "StudentBalanceHistory",
                column: "EnrollStudentCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBalanceHistory_StudentId",
                table: "StudentBalanceHistory",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExpulsionHistory_EnrollStudentCourseId",
                table: "StudentExpulsionHistory",
                column: "EnrollStudentCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExpulsionHistory_StudentId",
                table: "StudentExpulsionHistory",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentNotes_StudentId",
                table: "StudentNotes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentNotesTranslation_StudentNoteId",
                table: "StudentNotesTranslation",
                column: "StudentNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubscription_EnrollTeacherCourseId",
                table: "StudentSubscription",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTranslation_StudentId",
                table: "StudentTranslation",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCommunicationChannel_CommunicationChannelId",
                table: "SubCommunicationChannel",
                column: "CommunicationChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCommunicationChannel_ParentId",
                table: "SubCommunicationChannel",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCommunicationChannelTranslation_SubCommunicationChannelId",
                table: "SubCommunicationChannelTranslation",
                column: "SubCommunicationChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTranslation_SubjectId",
                table: "SubjectTranslation",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemFileTranslation_SystemFileId",
                table: "SystemFileTranslation",
                column: "SystemFileId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemGroupTranslation_SystemGroupId",
                table: "SystemGroupTranslation",
                column: "SystemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemGroupUsers_SystemGroupId",
                table: "SystemGroupUsers",
                column: "SystemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemGroupUsers_UserProfileId",
                table: "SystemGroupUsers",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemSetting_BranchId",
                table: "SystemSetting",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemSetting_SuperAdminId",
                table: "SystemSetting",
                column: "SuperAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemSettingTranslation_SettingId",
                table: "SystemSettingTranslation",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAttendances_EnrollTeacherCourseId",
                table: "TeacherAttendances",
                column: "EnrollTeacherCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateHtml_TypeId",
                table: "TemplateHtml",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainer_ContactId",
                table: "Trainer",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerRateMeasureTranslation_TrainerRateMeasureId",
                table: "TrainerRateMeasureTranslation",
                column: "TrainerRateMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerTranslation_TrainerId",
                table: "TrainerTranslation",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_ContactId",
                table: "UserProfile",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileTranslation_UserProfileId",
                table: "UserProfileTranslation",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutDicTranslation");

            migrationBuilder.DropTable(
                name: "AcademicSupervisionRate");

            migrationBuilder.DropTable(
                name: "AcademicSupervisionStandardTranslation");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssignmentTranslation");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BranchTranslation");

            migrationBuilder.DropTable(
                name: "CalendarTranslation");

            migrationBuilder.DropTable(
                name: "CertificateAdoption");

            migrationBuilder.DropTable(
                name: "CityTranslation");

            migrationBuilder.DropTable(
                name: "CmsCateryTranslation");

            migrationBuilder.DropTable(
                name: "CmsPageTranslation");

            migrationBuilder.DropTable(
                name: "CmsProjectCategoryTranslation");

            migrationBuilder.DropTable(
                name: "CmsProjectCostTranslation");

            migrationBuilder.DropTable(
                name: "CmsProjectDonor");

            migrationBuilder.DropTable(
                name: "CmsProjectResource");

            migrationBuilder.DropTable(
                name: "CmsProjectTranslation");

            migrationBuilder.DropTable(
                name: "CmsSliderTranslation");

            migrationBuilder.DropTable(
                name: "CmsWhatPeopleSayTranslation");

            migrationBuilder.DropTable(
                name: "CommunicationChannelTranslation");

            migrationBuilder.DropTable(
                name: "CommunicationLogs");

            migrationBuilder.DropTable(
                name: "ContactTranslation");

            migrationBuilder.DropTable(
                name: "ContactType");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "CountryTranslation");

            migrationBuilder.DropTable(
                name: "CourseAttendance");

            migrationBuilder.DropTable(
                name: "CourseCategoryTranslation");

            migrationBuilder.DropTable(
                name: "CourseCertification");

            migrationBuilder.DropTable(
                name: "CourseMarkTranslation");

            migrationBuilder.DropTable(
                name: "CoursePackagesRelations");

            migrationBuilder.DropTable(
                name: "CoursePakagesTranslation");

            migrationBuilder.DropTable(
                name: "CoursePrerequisite");

            migrationBuilder.DropTable(
                name: "CourseResourceTranslation");

            migrationBuilder.DropTable(
                name: "CourseTranslation");

            migrationBuilder.DropTable(
                name: "CurrencyTranslation");

            migrationBuilder.DropTable(
                name: "DataBaseScripts");

            migrationBuilder.DropTable(
                name: "DetailsLookupTranslation");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "EmployeeTranslation");

            migrationBuilder.DropTable(
                name: "EnrollAssignmentTranslation");

            migrationBuilder.DropTable(
                name: "EnrollCourseAllowUserRates");

            migrationBuilder.DropTable(
                name: "EnrollCourseAssigmentQuestionOptionTranslation");

            migrationBuilder.DropTable(
                name: "EnrollCourseAssigmentQuestionTranslation");

            migrationBuilder.DropTable(
                name: "EnrollCourseAssigmentTranslation");

            migrationBuilder.DropTable(
                name: "EnrollCourseExamTranslation");

            migrationBuilder.DropTable(
                name: "EnrollCourseQuizPointes");

            migrationBuilder.DropTable(
                name: "EnrollCourseResourceTranslation");

            migrationBuilder.DropTable(
                name: "EnrollCourseTime");

            migrationBuilder.DropTable(
                name: "EnrollCourseTimeCustomization");

            migrationBuilder.DropTable(
                name: "EnrollLectureTranslation");

            migrationBuilder.DropTable(
                name: "EnrollSectionOfCourseTranslation");

            migrationBuilder.DropTable(
                name: "EnrollStudentAlert");

            migrationBuilder.DropTable(
                name: "EnrollStudentAssigmentAnswerOption");

            migrationBuilder.DropTable(
                name: "EnrollStudentCourseAttachment");

            migrationBuilder.DropTable(
                name: "EnrollStudentExamAnswerOption");

            migrationBuilder.DropTable(
                name: "EnrollTeacherCourseTranlation");

            migrationBuilder.DropTable(
                name: "ExamTemplateTranslation");

            migrationBuilder.DropTable(
                name: "Expulsion");

            migrationBuilder.DropTable(
                name: "GeneralizationEmployee");

            migrationBuilder.DropTable(
                name: "GeneralizationTranslation");

            migrationBuilder.DropTable(
                name: "InvoicesPay");

            migrationBuilder.DropTable(
                name: "ItemFile");

            migrationBuilder.DropTable(
                name: "LectureTranslation");

            migrationBuilder.DropTable(
                name: "ManagementRateLine");

            migrationBuilder.DropTable(
                name: "ManagementStandardTranslation");

            migrationBuilder.DropTable(
                name: "MarkAdoptionForExam");

            migrationBuilder.DropTable(
                name: "MarkAdoptionForPracticalExam");

            migrationBuilder.DropTable(
                name: "MasterLookupTranslation");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ModuleTranslation");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PermissionTranslation");

            migrationBuilder.DropTable(
                name: "PracticalEnrollmentExamStudentSubjectRating");

            migrationBuilder.DropTable(
                name: "PracticalEnrollmentExamTrainer");

            migrationBuilder.DropTable(
                name: "PracticalExamCourseSubject");

            migrationBuilder.DropTable(
                name: "PracticalExamQuestion");

            migrationBuilder.DropTable(
                name: "PracticalExamTranslation");

            migrationBuilder.DropTable(
                name: "PracticalQuestionTranslation");

            migrationBuilder.DropTable(
                name: "QuestionOptionTranslation");

            migrationBuilder.DropTable(
                name: "QuestionTranslation");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SectionOfCourseQuiz");

            migrationBuilder.DropTable(
                name: "SectionOfCourseTranslation");

            migrationBuilder.DropTable(
                name: "SemesterTranslation");

            migrationBuilder.DropTable(
                name: "SenangPay");

            migrationBuilder.DropTable(
                name: "source");

            migrationBuilder.DropTable(
                name: "StudentAttendance");

            migrationBuilder.DropTable(
                name: "StudentBalanceHistory");

            migrationBuilder.DropTable(
                name: "StudentExpulsionHistory");

            migrationBuilder.DropTable(
                name: "StudentNotesTranslation");

            migrationBuilder.DropTable(
                name: "StudentSubscription");

            migrationBuilder.DropTable(
                name: "StudentTranslation");

            migrationBuilder.DropTable(
                name: "SubCommunicationChannelTranslation");

            migrationBuilder.DropTable(
                name: "SubjectTranslation");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "SuperAdminSettings");

            migrationBuilder.DropTable(
                name: "SystemFileTranslation");

            migrationBuilder.DropTable(
                name: "SystemGroupTranslation");

            migrationBuilder.DropTable(
                name: "SystemGroupUsers");

            migrationBuilder.DropTable(
                name: "SystemLog");

            migrationBuilder.DropTable(
                name: "SystemSettingTranslation");

            migrationBuilder.DropTable(
                name: "TeacherAttendances");

            migrationBuilder.DropTable(
                name: "Temp_Table1");

            migrationBuilder.DropTable(
                name: "TrainerRateMeasureTranslation");

            migrationBuilder.DropTable(
                name: "TrainerTranslation");

            migrationBuilder.DropTable(
                name: "UserProfileTranslation");

            migrationBuilder.DropTable(
                name: "AboutDic");

            migrationBuilder.DropTable(
                name: "AcademicSupervisionStandard");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "Calendar");

            migrationBuilder.DropTable(
                name: "CmsPage");

            migrationBuilder.DropTable(
                name: "CmsSlider");

            migrationBuilder.DropTable(
                name: "CmsWhatPeopleSay");

            migrationBuilder.DropTable(
                name: "TemplateHtml");

            migrationBuilder.DropTable(
                name: "CourseMarks");

            migrationBuilder.DropTable(
                name: "CoursePackages");

            migrationBuilder.DropTable(
                name: "CourseResource");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "EnrollAssignment");

            migrationBuilder.DropTable(
                name: "EnrollCourseAssigmentQuestionOption");

            migrationBuilder.DropTable(
                name: "EnrollCourseQuiz");

            migrationBuilder.DropTable(
                name: "EnrollCourseResource");

            migrationBuilder.DropTable(
                name: "EnrollStudentAssigmentAnswer");

            migrationBuilder.DropTable(
                name: "EnrollStudentExamAnswer");

            migrationBuilder.DropTable(
                name: "ManagementRate");

            migrationBuilder.DropTable(
                name: "ManagementStandard");

            migrationBuilder.DropTable(
                name: "Generalization");

            migrationBuilder.DropTable(
                name: "PracticalEnrollmentExamStudentSubject");

            migrationBuilder.DropTable(
                name: "PracticalExamCourse");

            migrationBuilder.DropTable(
                name: "PracticalQuestion");

            migrationBuilder.DropTable(
                name: "QuestionOption");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CmsProjectCost");

            migrationBuilder.DropTable(
                name: "StudentNotes");

            migrationBuilder.DropTable(
                name: "SubCommunicationChannel");

            migrationBuilder.DropTable(
                name: "SystemFile");

            migrationBuilder.DropTable(
                name: "SystemGroup");

            migrationBuilder.DropTable(
                name: "SystemSetting");

            migrationBuilder.DropTable(
                name: "TrainerRateMeasure");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "CmsCatery");

            migrationBuilder.DropTable(
                name: "DetailsLookup");

            migrationBuilder.DropTable(
                name: "Lecture");

            migrationBuilder.DropTable(
                name: "EnrollCourseAssigmentQuestion");

            migrationBuilder.DropTable(
                name: "EnrollStudentAssigment");

            migrationBuilder.DropTable(
                name: "EnrollStudentExam");

            migrationBuilder.DropTable(
                name: "EnrollCourseExamQuestion");

            migrationBuilder.DropTable(
                name: "PracticalEnrollmentExamStudent");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "CmsProject");

            migrationBuilder.DropTable(
                name: "CommunicationChannel");

            migrationBuilder.DropTable(
                name: "SuperAdmin");

            migrationBuilder.DropTable(
                name: "MasterLookup");

            migrationBuilder.DropTable(
                name: "SectionOfCourse");

            migrationBuilder.DropTable(
                name: "EnrollCourseAssigment");

            migrationBuilder.DropTable(
                name: "EnrollCourseExam");

            migrationBuilder.DropTable(
                name: "ExamQuestion");

            migrationBuilder.DropTable(
                name: "EnrollStudentCourse");

            migrationBuilder.DropTable(
                name: "PracticalEnrollmentExam");

            migrationBuilder.DropTable(
                name: "CmsProjectCategory");

            migrationBuilder.DropTable(
                name: "EnrollLecture");

            migrationBuilder.DropTable(
                name: "ExamTemplate");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "PracticalExam");

            migrationBuilder.DropTable(
                name: "EnrollSectionOfCourse");

            migrationBuilder.DropTable(
                name: "EnrollTeacherCourse");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "Trainer");

            migrationBuilder.DropTable(
                name: "CourseCategory");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
