using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelRobotics.Data.Migrations
{
    public partial class intupdat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Robots",
                newName: "Robotid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Robotid",
                table: "Robots",
                newName: "Id");
        }
    }
}
