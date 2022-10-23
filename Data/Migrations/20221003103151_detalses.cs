using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelRobotics.Data.Migrations
{
    public partial class detalses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kontaktForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Regarding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kontaktForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kontaktFormToRobots",
                columns: table => new
                {
                    KontaktFormId = table.Column<int>(type: "int", nullable: false),
                    RobotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kontaktFormToRobots", x => new { x.KontaktFormId, x.RobotId });
                    table.ForeignKey(
                        name: "FK_kontaktFormToRobots_kontaktForms_KontaktFormId",
                        column: x => x.KontaktFormId,
                        principalTable: "kontaktForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kontaktFormToRobots_Robots_RobotId",
                        column: x => x.RobotId,
                        principalTable: "Robots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_kontaktFormToRobots_RobotId",
                table: "kontaktFormToRobots",
                column: "RobotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kontaktFormToRobots");

            migrationBuilder.DropTable(
                name: "kontaktForms");
        }
    }
}
