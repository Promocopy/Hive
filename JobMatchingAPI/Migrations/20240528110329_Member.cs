using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobMatchingAPI.Migrations
{
    /// <inheritdoc />
    public partial class Member : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EHistories",
                columns: table => new
                {
                    DegreeClassification = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseOfStudy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeObtained = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CGPAScore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EHistories", x => x.DegreeClassification);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    EMAIL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    FIRSTNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LASTNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<double>(type: "float", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobPreference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.EMAIL);
                });

            migrationBuilder.CreateTable(
                name: "WorkHistories",
                columns: table => new
                {
                    Industry = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SummaryOfJobDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHistories", x => x.Industry);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EHistories");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "WorkHistories");
        }
    }
}
