using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphQL_APIs.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedCodeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_booking_Code",
                schema: "Booking",
                table: "booking");

            migrationBuilder.CreateIndex(
                name: "IX_booking_Code",
                schema: "Booking",
                table: "booking",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_booking_Code",
                schema: "Booking",
                table: "booking");

            migrationBuilder.CreateIndex(
                name: "IX_booking_Code",
                schema: "Booking",
                table: "booking",
                column: "Code");
        }
    }
}
