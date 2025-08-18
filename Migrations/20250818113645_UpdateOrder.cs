using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_basic.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Orders",
                newName: "SparişNotu");

            migrationBuilder.AddColumn<string>(
                name: "AdSoyad",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdSoyad",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "SparişNotu",
                table: "Orders",
                newName: "Email");
        }
    }
}
