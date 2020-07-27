using Microsoft.EntityFrameworkCore.Migrations;

namespace Async_Inn.Migrations
{
    public partial class updatingHotelRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Hotels_AmenityId",
                table: "RoomAmenities");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityId",
                table: "RoomAmenities",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityId",
                table: "RoomAmenities");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Hotels_AmenityId",
                table: "RoomAmenities",
                column: "AmenityId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
