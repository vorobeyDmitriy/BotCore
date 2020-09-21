using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BotCore.CurrencyBot.Infrastructure.Migrations
{
    public partial class currencyrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Currency",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true),
                    Scale = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Currency", x => x.Id); });

            migrationBuilder.CreateTable(
                "CurrencyRates",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromId = table.Column<int>(nullable: false),
                    ToId = table.Column<int>(nullable: false),
                    Rate = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRates", x => x.Id);
                    table.ForeignKey(
                        "FK_CurrencyRates_Currency_FromId",
                        x => x.FromId,
                        "Currency",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_CurrencyRates_Currency_ToId",
                        x => x.ToId,
                        "Currency",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_CurrencyRates_FromId",
                "CurrencyRates",
                "FromId");

            migrationBuilder.CreateIndex(
                "IX_CurrencyRates_ToId",
                "CurrencyRates",
                "ToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "CurrencyRates");

            migrationBuilder.DropTable(
                "Currency");
        }
    }
}