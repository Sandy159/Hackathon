using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hackathon.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeePk = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeePk);
                });

            migrationBuilder.CreateTable(
                name: "Hackathons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Score = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hackathons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamPk = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JuniorEmployeePk = table.Column<int>(type: "integer", nullable: false),
                    TeamLeadEmployeePk = table.Column<int>(type: "integer", nullable: false),
                    HackathonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamPk);
                    table.ForeignKey(
                        name: "FK_Teams_Employees_JuniorEmployeePk",
                        column: x => x.JuniorEmployeePk,
                        principalTable: "Employees",
                        principalColumn: "EmployeePk",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Employees_TeamLeadEmployeePk",
                        column: x => x.TeamLeadEmployeePk,
                        principalTable: "Employees",
                        principalColumn: "EmployeePk",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Hackathons_HackathonId",
                        column: x => x.HackathonId,
                        principalTable: "Hackathons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    WishlistPk = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeePk = table.Column<int>(type: "integer", nullable: false),
                    DesiredEmployees = table.Column<int[]>(type: "integer[]", nullable: false),
                    HackathonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.WishlistPk);
                    table.ForeignKey(
                        name: "FK_Wishlists_Employees_EmployeePk",
                        column: x => x.EmployeePk,
                        principalTable: "Employees",
                        principalColumn: "EmployeePk",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wishlists_Hackathons_HackathonId",
                        column: x => x.HackathonId,
                        principalTable: "Hackathons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_HackathonId",
                table: "Teams",
                column: "HackathonId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_JuniorEmployeePk",
                table: "Teams",
                column: "JuniorEmployeePk");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamLeadEmployeePk",
                table: "Teams",
                column: "TeamLeadEmployeePk");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_EmployeePk",
                table: "Wishlists",
                column: "EmployeePk");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_HackathonId",
                table: "Wishlists",
                column: "HackathonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Hackathons");
        }
    }
}
