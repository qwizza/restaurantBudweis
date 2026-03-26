using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurantBudweis.Migrations
{
    /// <inheritdoc />
    public partial class DishGroupDish2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishs_GroupDish_GroupDishId",
                table: "Dishs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupDish",
                table: "GroupDish");

            migrationBuilder.RenameTable(
                name: "GroupDish",
                newName: "DishsGroupDishs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DishsGroupDishs",
                table: "DishsGroupDishs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishs_DishsGroupDishs_GroupDishId",
                table: "Dishs",
                column: "GroupDishId",
                principalTable: "DishsGroupDishs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishs_DishsGroupDishs_GroupDishId",
                table: "Dishs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DishsGroupDishs",
                table: "DishsGroupDishs");

            migrationBuilder.RenameTable(
                name: "DishsGroupDishs",
                newName: "GroupDish");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupDish",
                table: "GroupDish",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishs_GroupDish_GroupDishId",
                table: "Dishs",
                column: "GroupDishId",
                principalTable: "GroupDish",
                principalColumn: "Id");
        }
    }
}
