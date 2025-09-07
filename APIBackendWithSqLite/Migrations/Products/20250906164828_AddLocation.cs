using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIBackend.Migrations.Products
{
    /// <inheritdoc />
    public partial class AddLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Customers_CustomerIdCode",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Location_LocationId",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameIndex(
                name: "IX_Location_CustomerIdCode",
                table: "Locations",
                newName: "IX_Locations_CustomerIdCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Customers_CustomerIdCode",
                table: "Locations",
                column: "CustomerIdCode",
                principalTable: "Customers",
                principalColumn: "CustomerIdCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Locations_LocationId",
                table: "Units",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Customers_CustomerIdCode",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Locations_LocationId",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_CustomerIdCode",
                table: "Location",
                newName: "IX_Location_CustomerIdCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Customers_CustomerIdCode",
                table: "Location",
                column: "CustomerIdCode",
                principalTable: "Customers",
                principalColumn: "CustomerIdCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Location_LocationId",
                table: "Units",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
