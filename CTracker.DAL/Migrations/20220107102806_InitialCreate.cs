using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTracker.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioId = table.Column<int>(type: "int", nullable: false),
                    CoinId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assets_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,10)", precision: 18, scale: 10, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,10)", precision: 18, scale: 10, nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,10)", precision: 18, scale: 10, nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoinId = table.Column<int>(type: "int", nullable: false),
                    PortfolioId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trade_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trade_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    AverageCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Investment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unrealized = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnrealizedPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetHistories_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coins",
                columns: new[] { "Id", "Created", "IsDeleted", "LastModified", "Name", "Symbol" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6463), false, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6464), "Bitcoin", "BTC" },
                    { 2, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6466), false, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6467), "Ethereum", "ETH" },
                    { 3, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6468), false, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6469), "Cardano", "ADA" },
                    { 4, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6470), false, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6471), "USD Coin", "USDC" }
                });

            migrationBuilder.InsertData(
                table: "Portfolios",
                columns: new[] { "Id", "Created", "IsDeleted", "LastModified", "Name" },
                values: new object[] { 1, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6363), false, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6404), "Test" });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "CoinId", "Created", "IsDeleted", "LastModified", "PortfolioId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6484), false, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6485), 1 },
                    { 2, 2, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6487), false, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6488), 1 },
                    { 3, 3, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6489), false, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6490), 1 },
                    { 4, 4, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6491), false, new DateTime(2022, 1, 7, 11, 28, 6, 86, DateTimeKind.Local).AddTicks(6492), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetHistories_AssetId",
                table: "AssetHistories",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_CoinId",
                table: "Assets",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_PortfolioId",
                table: "Assets",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Trade_CoinId",
                table: "Trade",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_Trade_PortfolioId",
                table: "Trade",
                column: "PortfolioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetHistories");

            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "Portfolios");
        }
    }
}
