using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_basic.Migrations
{
    /// <inheritdoc />
    public partial class AddKategoriTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KategoriId",
                table: "Urunler",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KategoriAdi = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Kategoriler",
                columns: new[] { "Id", "KategoriAdi", "Url" },
                values: new object[,]
                {
                    { 1, "Telefon", "telefon" },
                    { 2, "Elektronik", "elektronik" },
                    { 3, "Beyaz Eşya", "beyaz-esya" },
                    { 4, "Giyim", "giyim" },
                    { 5, "Kozmetik", "kozmetik" },
                    { 6, "Saat", "saat" },
                    { 7, "Araba", "araba" },
                    { 8, "Kitap", "kitap" },
                    { 9, "Mobilya", "mobilya" }
                });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 1,
                column: "KategoriId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 2,
                column: "KategoriId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 3,
                column: "KategoriId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 4,
                column: "KategoriId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 5,
                column: "KategoriId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 6,
                column: "KategoriId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 7,
                column: "KategoriId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 8,
                column: "KategoriId",
                value: 6);

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_KategoriId",
                table: "Urunler",
                column: "KategoriId");

            migrationBuilder.AddForeignKey(
                name: "FK_Urunler_Kategoriler_KategoriId",
                table: "Urunler",
                column: "KategoriId",
                principalTable: "Kategoriler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urunler_Kategoriler_KategoriId",
                table: "Urunler");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropIndex(
                name: "IX_Urunler_KategoriId",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "KategoriId",
                table: "Urunler");
        }
    }
}
