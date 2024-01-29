using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostTrades.Migrations
{
    public partial class MigrationChangeBid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bid",
                table: "Bid");

            migrationBuilder.RenameTable(
                name: "Bid",
                newName: "Bids");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                table: "Bids",
                column: "BidId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.RenameTable(
                name: "Bids",
                newName: "Bid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bid",
                table: "Bid",
                column: "BidId");
        }
    }
}
