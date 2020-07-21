using Microsoft.EntityFrameworkCore.Migrations;

namespace Async_Inn.Migrations
{
    public partial class addedHotelAndRoomSeededData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FloorPlan = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "City", "Name", "Phone", "State", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "Seattle", "Dummy Hotel", "3606655432", "Wa", "123 Dummy St." },
                    { 2, "Redmond", "Dummy Hotel v2", "3606332332", "Wash", "123456 Dummy St." },
                    { 3, "Everett", "Dummy Hotel v3", "36066553231222", "Washington", "123456789 Dummy St." }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "FloorPlan", "Name" },
                values: new object[,]
                {
                    { 1, 0, "Dummy Room" },
                    { 2, 2, "Dummy Room v2" },
                    { 3, 4, "Dummy Room v3" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
