using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Sirius.Repositories.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sirius");

            migrationBuilder.CreateTable(
                name: "deposits",
                schema: "sirius",
                columns: table => new
                {
                    BlockchainId = table.Column<string>(nullable: false),
                    NetworkId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    TransactionHash = table.Column<string>(nullable: true),
                    AssetId = table.Column<string>(nullable: true),
                    WalletId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deposits", x => new { x.BlockchainId, x.NetworkId, x.Id });
                });

            migrationBuilder.CreateTable(
                name: "wallet_groups",
                schema: "sirius",
                columns: table => new
                {
                    BlockchainId = table.Column<string>(nullable: false),
                    NetworkId = table.Column<string>(nullable: false),
                    GroupName = table.Column<string>(nullable: false),
                    WalletId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallet_groups", x => new { x.BlockchainId, x.NetworkId, x.GroupName });
                });

            migrationBuilder.CreateTable(
                name: "deposit_sources",
                schema: "sirius",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DepositBlockchainId = table.Column<string>(nullable: true),
                    DepositNetworkId = table.Column<string>(nullable: true),
                    DepositId = table.Column<string>(nullable: true),
                    SourceAddress = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deposit_sources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_deposit_sources_deposits_DepositBlockchainId_DepositNetwork~",
                        columns: x => new { x.DepositBlockchainId, x.DepositNetworkId, x.DepositId },
                        principalSchema: "sirius",
                        principalTable: "deposits",
                        principalColumns: new[] { "BlockchainId", "NetworkId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "deposit_wallets",
                schema: "sirius",
                columns: table => new
                {
                    BlockchainId = table.Column<string>(nullable: false),
                    NetworkId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    WalletAddress = table.Column<string>(nullable: true),
                    OriginalWalletAddress = table.Column<string>(nullable: true),
                    PublicKey = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deposit_wallets", x => new { x.BlockchainId, x.NetworkId, x.Id });
                    table.ForeignKey(
                        name: "FK_deposit_wallets_wallet_groups_BlockchainId_NetworkId_GroupN~",
                        columns: x => new { x.BlockchainId, x.NetworkId, x.GroupName },
                        principalSchema: "sirius",
                        principalTable: "wallet_groups",
                        principalColumns: new[] { "BlockchainId", "NetworkId", "GroupName" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "hot_wallets",
                schema: "sirius",
                columns: table => new
                {
                    BlockchainId = table.Column<string>(nullable: false),
                    NetworkId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    WalletAddress = table.Column<string>(nullable: true),
                    OriginalWalletAddress = table.Column<string>(nullable: true),
                    PublicKey = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hot_wallets", x => new { x.BlockchainId, x.NetworkId, x.Id });
                    table.ForeignKey(
                        name: "FK_hot_wallets_wallet_groups_BlockchainId_NetworkId_GroupName",
                        columns: x => new { x.BlockchainId, x.NetworkId, x.GroupName },
                        principalSchema: "sirius",
                        principalTable: "wallet_groups",
                        principalColumns: new[] { "BlockchainId", "NetworkId", "GroupName" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_deposit_sources_DepositBlockchainId_DepositNetworkId_Deposi~",
                schema: "sirius",
                table: "deposit_sources",
                columns: new[] { "DepositBlockchainId", "DepositNetworkId", "DepositId" });

            migrationBuilder.CreateIndex(
                name: "IX_deposit_wallets_BlockchainId_NetworkId_GroupName",
                schema: "sirius",
                table: "deposit_wallets",
                columns: new[] { "BlockchainId", "NetworkId", "GroupName" });

            migrationBuilder.CreateIndex(
                name: "IX_DW_BlockchainId_NetworkId_WalletAddress",
                schema: "sirius",
                table: "deposit_wallets",
                columns: new[] { "BlockchainId", "NetworkId", "WalletAddress" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlockchainId_NetworkId_TransactionHash",
                schema: "sirius",
                table: "deposits",
                columns: new[] { "BlockchainId", "NetworkId", "TransactionHash" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hot_wallets_BlockchainId_NetworkId_GroupName",
                schema: "sirius",
                table: "hot_wallets",
                columns: new[] { "BlockchainId", "NetworkId", "GroupName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HW_BlockchainId_NetworkId_WalletAddress",
                schema: "sirius",
                table: "hot_wallets",
                columns: new[] { "BlockchainId", "NetworkId", "WalletAddress" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deposit_sources",
                schema: "sirius");

            migrationBuilder.DropTable(
                name: "deposit_wallets",
                schema: "sirius");

            migrationBuilder.DropTable(
                name: "hot_wallets",
                schema: "sirius");

            migrationBuilder.DropTable(
                name: "deposits",
                schema: "sirius");

            migrationBuilder.DropTable(
                name: "wallet_groups",
                schema: "sirius");
        }
    }
}
