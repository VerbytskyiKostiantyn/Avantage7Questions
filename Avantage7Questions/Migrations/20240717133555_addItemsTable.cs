using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avantage7Questions.Migrations
{
    /// <inheritdoc />
    public partial class addItemsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Quetions_QuestionId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quetions",
                table: "Quetions");

            migrationBuilder.RenameTable(
                name: "Quetions",
                newName: "Questions");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "ItemsId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Computer = table.Column<bool>(type: "bit", nullable: true),
                    Laptop = table.Column<bool>(type: "bit", nullable: true),
                    Mouse = table.Column<bool>(type: "bit", nullable: true),
                    Keyboard = table.Column<bool>(type: "bit", nullable: true),
                    Monitor = table.Column<bool>(type: "bit", nullable: true),
                    Phone = table.Column<bool>(type: "bit", nullable: true),
                    Another = table.Column<bool>(type: "bit", nullable: true),
                    AnotherText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ItemsId",
                table: "Questions",
                column: "ItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Questions_QuestionId",
                table: "Images",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Items_ItemsId",
                table: "Questions",
                column: "ItemsId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Questions_QuestionId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Items_ItemsId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ItemsId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ItemsId",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Quetions");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Quetions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quetions",
                table: "Quetions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Quetions_QuestionId",
                table: "Images",
                column: "QuestionId",
                principalTable: "Quetions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
