using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurantBudweis.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishs_DishsGroupDishs_GroupDishId",
                table: "Dishs");

            migrationBuilder.RenameColumn(
                name: "GroupDishId",
                table: "Dishs",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Dishs_GroupDishId",
                table: "Dishs",
                newName: "IX_Dishs_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishs_DishsGroupDishs_GroupId",
                table: "Dishs",
                column: "GroupId",
                principalTable: "DishsGroupDishs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishs_DishsGroupDishs_GroupId",
                table: "Dishs");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Dishs",
                newName: "GroupDishId");

            migrationBuilder.RenameIndex(
                name: "IX_Dishs_GroupId",
                table: "Dishs",
                newName: "IX_Dishs_GroupDishId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishs_DishsGroupDishs_GroupDishId",
                table: "Dishs",
                column: "GroupDishId",
                principalTable: "DishsGroupDishs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
