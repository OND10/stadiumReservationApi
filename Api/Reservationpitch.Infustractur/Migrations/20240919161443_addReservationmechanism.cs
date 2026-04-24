using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservationpitch.Infustractur.Migrations
{
    /// <inheritdoc />
    public partial class addReservationmechanism : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StadiumCenterPhoneNumber",
                table: "StadiumReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NoneHashedPassword",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StadiumCenterPhoneNumber",
                table: "StadiumReservations");

            migrationBuilder.DropColumn(
                name: "NoneHashedPassword",
                table: "AspNetUsers");
        }
    }
}
