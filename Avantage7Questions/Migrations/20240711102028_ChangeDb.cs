using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avantage7Questions.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "data",
                table: "Quetions",
                newName: "Data");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Quetions",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "Job",
                table: "Quetions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Job",
                table: "Quetions");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Quetions",
                newName: "data");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Quetions",
                newName: "Surname");
        }
    }
}
