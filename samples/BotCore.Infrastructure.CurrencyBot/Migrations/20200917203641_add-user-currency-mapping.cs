using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BotCore.Infrastructure.CurrencyBot.Migrations
{
    public partial class addusercurrencymapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "User",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_User", x => x.Id); });

            migrationBuilder.CreateTable(
                "UserCurrencyMappings",
                table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCurrencyMappings", x => new {x.UserId, x.CurrencyId});
                    table.ForeignKey(
                        "FK_UserCurrencyMappings_Currency_CurrencyId",
                        x => x.CurrencyId,
                        "Currency",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_UserCurrencyMappings_User_UserId",
                        x => x.UserId,
                        "User",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_UserCurrencyMappings_CurrencyId",
                "UserCurrencyMappings",
                "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "UserCurrencyMappings");

            migrationBuilder.DropTable(
                "User");
        }
    }
}