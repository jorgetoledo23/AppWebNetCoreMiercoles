using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppMiercoles.Migrations
{
    public partial class itemsCarro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblItemsCarro",
                columns: table => new
                {
                    ItemCarroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(nullable: false),
                    ProductoId = table.Column<int>(nullable: false),
                    CarroCompraId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemsCarro", x => x.ItemCarroId);
                    table.ForeignKey(
                        name: "FK_tblItemsCarro_tblProductos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "tblProductos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblItemsCarro_ProductoId",
                table: "tblItemsCarro",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblItemsCarro");
        }
    }
}
