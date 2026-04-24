using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservationpitch.Infustractur.Migrations
{
    /// <inheritdoc />
    public partial class addTimeSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndBreakingTime",
                table: "WorkDays",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartBreakingTime",
                table: "WorkDays",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "StadiumCenters",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TimeSlotsId",
                table: "CenterBookings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfTimeSlot = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeSlotValue = table.Column<TimeOnly>(type: "time", nullable: false),
                    TimeStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_StadiumCenters_CenterId",
                        column: x => x.CenterId,
                        principalTable: "StadiumCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CenterBookings_TimeSlotsId",
                table: "CenterBookings",
                column: "TimeSlotsId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_CenterId",
                table: "TimeSlots",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterBookings_TimeSlots_TimeSlotsId",
                table: "CenterBookings",
                column: "TimeSlotsId",
                principalTable: "TimeSlots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterBookings_TimeSlots_TimeSlotsId",
                table: "CenterBookings");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_CenterBookings_TimeSlotsId",
                table: "CenterBookings");

            migrationBuilder.DropColumn(
                name: "EndBreakingTime",
                table: "WorkDays");

            migrationBuilder.DropColumn(
                name: "StartBreakingTime",
                table: "WorkDays");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StadiumCenters");

            migrationBuilder.DropColumn(
                name: "TimeSlotsId",
                table: "CenterBookings");
        }
    }
}
