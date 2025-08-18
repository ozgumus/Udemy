using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_basic.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUrunEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Fiyat",
                table: "Urunler",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Aciklama",
                table: "Urunler",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Anasayfa",
                table: "Urunler",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Resim",
                table: "Urunler",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Aciklama", "Anasayfa", "Fiyat", "Resim" },
                values: new object[] { "NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor.", false, 10000.0, "1.jpeg" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Aciklama", "Anasayfa", "Fiyat", "Resim" },
                values: new object[] { "NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor.", true, 20000.0, "2.jpeg" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Aciklama", "Aktif", "Anasayfa", "Fiyat", "Resim" },
                values: new object[] { "NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor.", false, true, 30000.0, "3.jpeg" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Aciklama", "Anasayfa", "Fiyat", "Resim" },
                values: new object[] { "NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor.", true, 40000.0, "4.jpeg" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Aciklama", "Anasayfa", "Fiyat", "Resim" },
                values: new object[] { "NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor.", false, 50000.0, "5.jpeg" });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Aciklama", "Anasayfa", "Fiyat", "Resim" },
                values: new object[] { "NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor.", true, 60000.0, "6.jpeg" });

            migrationBuilder.InsertData(
                table: "Urunler",
                columns: new[] { "Id", "Aciklama", "Aktif", "Anasayfa", "Fiyat", "Resim", "UrunAdi" },
                values: new object[,]
                {
                    { 7, "NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor.", false, true, 70000.0, "7.jpeg", "Apple Watch 13" },
                    { 8, "NEDEN APPLE WATCH SE SATIN ALMALISINIZ? — Motive ve aktif olmanız, sağlık durumunuzu takip etmeniz, bağlantıda ve güvende kalmanız için gereken her şey bileğinizde. watchOS 11 daha fazla yapay öğrenme, kişiselleştirme ve bağlantı özellikleri sunuyor. Düşme Algılama ve gelişmiş antrenman ölçümleri gibi özelliklere sahip Apple Watch SE, harika bir fiyatla sunuluyor.", true, false, 80000.0, "8.jpeg", "Apple Watch 14" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "Aciklama",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "Anasayfa",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "Resim",
                table: "Urunler");

            migrationBuilder.AlterColumn<int>(
                name: "Fiyat",
                table: "Urunler",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 1,
                column: "Fiyat",
                value: 10000);

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 2,
                column: "Fiyat",
                value: 20000);

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Aktif", "Fiyat" },
                values: new object[] { true, 30000 });

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 4,
                column: "Fiyat",
                value: 40000);

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 5,
                column: "Fiyat",
                value: 50000);

            migrationBuilder.UpdateData(
                table: "Urunler",
                keyColumn: "Id",
                keyValue: 6,
                column: "Fiyat",
                value: 60000);
        }
    }
}
