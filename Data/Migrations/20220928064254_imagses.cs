using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelRobotics.Data.Migrations
{
    public partial class imagses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "visible",
                table: "Robots");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Robots",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Robots");

            migrationBuilder.AddColumn<bool>(
                name: "visible",
                table: "Robots",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
