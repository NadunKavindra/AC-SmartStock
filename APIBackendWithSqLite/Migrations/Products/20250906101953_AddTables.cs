using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIBackend.Migrations.Products
{
    /// <inheritdoc />
    public partial class AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerIdCode = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerIdCode);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocationName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerIdCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Location_Customers_CustomerIdCode",
                        column: x => x.CustomerIdCode,
                        principalTable: "Customers",
                        principalColumn: "CustomerIdCode");
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UnitIdCode = table.Column<string>(type: "TEXT", nullable: false),
                    UnitName = table.Column<string>(type: "TEXT", nullable: false),
                    UnitDescription = table.Column<string>(type: "TEXT", nullable: true),
                    Capacity = table.Column<string>(type: "TEXT", nullable: true),
                    InstallationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ServiceDueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.UnitIdCode);
                    table.ForeignKey(
                        name: "FK_Units_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Technician = table.Column<string>(type: "TEXT", nullable: true),
                    ServiceType = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    InvoiceCost = table.Column<decimal>(type: "TEXT", nullable: false),
                    ActualCost = table.Column<decimal>(type: "TEXT", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    UnitIdCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitHistories_Units_UnitIdCode",
                        column: x => x.UnitIdCode,
                        principalTable: "Units",
                        principalColumn: "UnitIdCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_CustomerIdCode",
                table: "Location",
                column: "CustomerIdCode");

            migrationBuilder.CreateIndex(
                name: "IX_UnitHistories_UnitIdCode",
                table: "UnitHistories",
                column: "UnitIdCode");

            migrationBuilder.CreateIndex(
                name: "IX_Units_LocationId",
                table: "Units",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitHistories");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
