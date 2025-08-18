using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_basic.Migrations
{
    /// <inheritdoc />
    public partial class AddSlider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sliderlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Baslik = table.Column<string>(type: "TEXT", nullable: true),
                    Aciklama = table.Column<string>(type: "TEXT", nullable: true),
                    Resim = table.Column<string>(type: "TEXT", nullable: false),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Aktif = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliderlar", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Sliderlar",
                columns: new[] { "Id", "Aciklama", "Aktif", "Baslik", "Index", "Resim" },
                values: new object[,]
                {
                    { 1, "Slider 1 Aciklama", true, "Slider Başlık 1", 0, "Slider-1.jpeg" },
                    { 2, "Slider 2 Aciklama", true, "Slider Başlık 2", 1, "Slider-2.jpeg" },
                    { 3, "Slider 3 Aciklama", true, "Slider Başlık 3", 2, "Slider-3.jpeg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sliderlar");
        }
    }
}
