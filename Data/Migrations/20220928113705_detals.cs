using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelRobotics.Data.Migrations
{
    public partial class detals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Footprint",
                table: "Robots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LigtingCapacity",
                table: "Robots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Parts",
                table: "Robots",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Radius",
                table: "Robots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "Robots",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Footprint",
                table: "Robots");

            migrationBuilder.DropColumn(
                name: "LigtingCapacity",
                table: "Robots");

            migrationBuilder.DropColumn(
                name: "Parts",
                table: "Robots");

            migrationBuilder.DropColumn(
                name: "Radius",
                table: "Robots");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Robots");
        }
    }
}
