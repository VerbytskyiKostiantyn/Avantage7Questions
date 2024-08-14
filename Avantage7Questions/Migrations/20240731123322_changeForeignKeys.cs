using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avantage7Questions.Migrations
{
    /// <inheritdoc />
    public partial class changeForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Items_ItemsId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Itemses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itemses",
                table: "Itemses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Itemses_ItemsId",
                table: "Questions",
                column: "ItemsId",
                principalTable: "Itemses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Itemses_ItemsId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itemses",
                table: "Itemses");

            migrationBuilder.RenameTable(
                name: "Itemses",
                newName: "Items");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Items_ItemsId",
                table: "Questions",
                column: "ItemsId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
