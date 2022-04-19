using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAPIServices.Migrations
{
    public partial class userContextMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airline",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    ContactNumber = table.Column<string>(maxLength: 500, nullable: true),
                    ContactAddress = table.Column<string>(maxLength: 500, nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airline", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRegistrestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Mobile = table.Column<string>(maxLength: 500, nullable: true),
                    Email = table.Column<string>(maxLength: 500, nullable: true),
                    Role = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRegistrestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FlightId = table.Column<string>(maxLength: 500, nullable: true),
                    AirlineId = table.Column<Guid>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: true),
                    ToDate = table.Column<DateTime>(nullable: true),
                    FromLocation = table.Column<string>(maxLength: 500, nullable: true),
                    ToLocation = table.Column<string>(maxLength: 500, nullable: true),
                    Veg = table.Column<bool>(nullable: false),
                    NonVeg = table.Column<bool>(nullable: false),
                    NoOfBUSeats = table.Column<int>(nullable: false),
                    NoOfNONBUSeats = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true),
                    NoOfRows = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Sheduled = table.Column<string>(maxLength: 500, nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Airline_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlightBooking",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FlightId = table.Column<Guid>(nullable: true),
                    FlightNumber = table.Column<string>(nullable: true),
                    AirlineId = table.Column<Guid>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    FromLocation = table.Column<string>(maxLength: 500, nullable: true),
                    ToLocation = table.Column<string>(maxLength: 500, nullable: true),
                    Veg = table.Column<bool>(nullable: false),
                    NonVeg = table.Column<bool>(nullable: false),
                    NoOfBUSeats = table.Column<int>(nullable: false),
                    NoOfNONBUSeats = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 500, nullable: true),
                    SeatNo = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    PNRNumber = table.Column<string>(maxLength: 500, nullable: true),
                    MailId = table.Column<string>(maxLength: 500, nullable: true),
                    ContactNumber = table.Column<string>(maxLength: 500, nullable: true),
                    UserRegistrestionId = table.Column<string>(maxLength: 500, nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightBooking_Airline_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightBooking_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightBooking_AirlineId",
                table: "FlightBooking",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBooking_FlightId",
                table: "FlightBooking",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirlineId",
                table: "Flights",
                column: "AirlineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightBooking");

            migrationBuilder.DropTable(
                name: "UserRegistrestion");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airline");
        }
    }
}
