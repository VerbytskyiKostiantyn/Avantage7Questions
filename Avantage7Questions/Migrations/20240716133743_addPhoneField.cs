using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avantage7Questions.Migrations
{
    /// <inheritdoc />
    public partial class addPhoneField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Job",
                table: "Quetions");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Quetions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Quetions");

            migrationBuilder.AddColumn<string>(
                name: "Job",
                table: "Quetions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
