using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class Hello2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Halls_Hall_ID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_Hall_ID",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Events_Hall_ID",
                table: "Events",
                column: "Hall_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Halls_Hall_ID",
                table: "Events",
                column: "Hall_ID",
                principalTable: "Halls",
                principalColumn: "HallID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
