using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservationpitch.Infustractur.Migrations
{
    /// <inheritdoc />
    public partial class AddcenterBookingtoDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "StadiumReservations");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "StadiumReservations");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "StadiumReservations");

            migrationBuilder.DropColumn(
                name: "StadiumCenterPhoneNumber",
                table: "StadiumReservations");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "StadiumReservations");

            migrationBuilder.DropColumn(
                name: "UserPhoneNumber",
                table: "StadiumReservations");

            migrationBuilder.AddColumn<Guid>(
                name: "CenterBookingId",
                table: "StadiumReservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StadiumId",
                table: "StadiumReservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "State",
                table: "AspNetUsers",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "UserType",
                table: "AspNetUsers",
                type: "tinyint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CenterBookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    ExchangeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterBookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterBookings_StadiumCenters_CenterId",
                        column: x => x.CenterId,
                        principalTable: "StadiumCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StadiumReservations_CenterBookingId",
                table: "StadiumReservations",
                column: "CenterBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_StadiumReservations_StadiumId",
                table: "StadiumReservations",
                column: "StadiumId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterBookings_CenterId",
                table: "CenterBookings",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterBookings_UserId",
                table: "CenterBookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StadiumReservations_CenterBookings_CenterBookingId",
                table: "StadiumReservations",
                column: "CenterBookingId",
                principalTable: "CenterBookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StadiumReservations_Stadiums_StadiumId",
                table: "StadiumReservations",
                column: "StadiumId",
                principalTable: "Stadiums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StadiumReservations_CenterBookings_CenterBookingId",
                table: "StadiumReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_StadiumReservations_Stadiums_StadiumId",
                table: "StadiumReservations");

            migrationBuilder.DropTable(
                name: "CenterBookings");

            migrationBuilder.DropIndex(
                name: "IX_StadiumReservations_CenterBookingId",
                table: "StadiumReservations");

            migrationBuilder.DropIndex(
                name: "IX_StadiumReservations_StadiumId",
                table: "StadiumReservations");

            migrationBuilder.DropColumn(
                name: "CenterBookingId",
                table: "StadiumReservations");

            migrationBuilder.DropColumn(
                name: "StadiumId",
                table: "StadiumReservations");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "StadiumReservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Payment",
                table: "StadiumReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "StadiumReservations",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "StadiumCenterPhoneNumber",
                table: "StadiumReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "StadiumReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserPhoneNumber",
                table: "StadiumReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
