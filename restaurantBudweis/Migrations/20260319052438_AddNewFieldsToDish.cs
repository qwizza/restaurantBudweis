using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurantBudweis.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldsToDish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DishID",
                table: "Dishs",
                newName: "DishName");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionDish",
                table: "Dishs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredients",
                table: "Dishs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionDish",
                table: "Dishs");

            migrationBuilder.DropColumn(
                name: "Ingredients",
                table: "Dishs");

            migrationBuilder.RenameColumn(
                name: "DishName",
                table: "Dishs",
                newName: "DishID");
        }
    }
}
