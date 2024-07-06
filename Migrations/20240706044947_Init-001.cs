using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphQL_APIs.Migrations
{
    /// <inheritdoc />
    public partial class Init001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Booking");

            migrationBuilder.CreateTable(
                name: "booking",
                schema: "Booking",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    BookingDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_booking_Code",
                schema: "Booking",
                table: "booking",
                column: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking",
                schema: "Booking");
        }
    }
}
