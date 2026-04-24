using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservationpitch.Infustractur.Migrations
{
    /// <inheritdoc />
    public partial class addImageUrlColumnToStadiumCentertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "StadiumCenters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "StadiumCenters");
        }
    }
}
