using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurantBudweis.Migrations
{
    /// <inheritdoc />
    public partial class DishGroupDish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupDish",
                table: "Dishs");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Dishs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DishID",
                table: "Dishs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupDishId",
                table: "Dishs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupDish",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDish", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dishs_ClientId",
                table: "Dishs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishs_GroupDishId",
                table: "Dishs",
                column: "GroupDishId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishs_Clients_ClientId",
                table: "Dishs",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishs_GroupDish_GroupDishId",
                table: "Dishs",
                column: "GroupDishId",
                principalTable: "GroupDish",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishs_Clients_ClientId",
                table: "Dishs");

            migrationBuilder.DropForeignKey(
                name: "FK_Dishs_GroupDish_GroupDishId",
                table: "Dishs");

            migrationBuilder.DropTable(
                name: "GroupDish");

            migrationBuilder.DropIndex(
                name: "IX_Dishs_ClientId",
                table: "Dishs");

            migrationBuilder.DropIndex(
                name: "IX_Dishs_GroupDishId",
                table: "Dishs");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Dishs");

            migrationBuilder.DropColumn(
                name: "DishID",
                table: "Dishs");

            migrationBuilder.DropColumn(
                name: "GroupDishId",
                table: "Dishs");

            migrationBuilder.AddColumn<string>(
                name: "GroupDish",
                table: "Dishs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
