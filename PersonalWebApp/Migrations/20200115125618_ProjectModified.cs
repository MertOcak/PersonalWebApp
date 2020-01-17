using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalWebApp.Migrations
{
    public partial class ProjectModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Projects",
                newName: "SefUrl");

            migrationBuilder.AddColumn<string>(
                name: "ProjectUrl",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectUrl",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "SefUrl",
                table: "Projects",
                newName: "Url");
        }
    }
}
