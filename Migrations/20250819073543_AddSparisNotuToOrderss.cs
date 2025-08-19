using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_basic.Migrations
{
    /// <inheritdoc />
    public partial class AddSparisNotuToOrderss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SparişNotu",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "SparisNotu",
                table: "Orders",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SparisNotu",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "SparişNotu",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
