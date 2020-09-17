using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HolidayMakerAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accommodation",
                columns: table => new
                {
                    AccommodationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationName = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    TypeOfAccommodation = table.Column<string>(nullable: true),
                    HasPool = table.Column<bool>(nullable: false),
                    HasEntertainment = table.Column<bool>(nullable: false),
                    HasKidsClub = table.Column<bool>(nullable: false),
                    HasRestaurant = table.Column<bool>(nullable: false),
                    DistanceToBeach = table.Column<int>(nullable: false),
                    DistanceToCenter = table.Column<int>(nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(10,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodation", x => x.AccommodationID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<int>(nullable: false),
                    RoomType = table.Column<string>(nullable: true),
                    IsAvailable = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,1)", nullable: false),
                    AccommodationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Room_Accommodation_AccommodationID",
                        column: x => x.AccommodationID,
                        principalTable: "Accommodation",
                        principalColumn: "AccommodationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingNumber = table.Column<string>(nullable: true),
                    CheckIn = table.Column<DateTime>(nullable: true),
                    CheckOut = table.Column<DateTime>(nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,1)", nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Booking_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingRoom",
                columns: table => new
                {
                    BookingID = table.Column<int>(nullable: false),
                    RoomID = table.Column<int>(nullable: false),
                    ExtraBedBooked = table.Column<bool>(nullable: false),
                    FullBoard = table.Column<bool>(nullable: false),
                    HalfBoard = table.Column<bool>(nullable: false),
                    AllInclusive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingRoom", x => new { x.BookingID, x.RoomID });
                    table.ForeignKey(
                        name: "FK_BookingRoom_Booking_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Booking",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingRoom_Room_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UserID",
                table: "Booking",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingRoom_RoomID",
                table: "BookingRoom",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Room_AccommodationID",
                table: "Room",
                column: "AccommodationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingRoom");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Accommodation");
        }
    }
}
