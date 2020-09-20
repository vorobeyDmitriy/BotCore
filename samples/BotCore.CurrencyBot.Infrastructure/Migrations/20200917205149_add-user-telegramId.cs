using Microsoft.EntityFrameworkCore.Migrations;

namespace BotCore.CurrencyBot.Infrastructure.Migrations
{
    public partial class addusertelegramId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                "TelegramId",
                "User",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "TelegramId",
                "User");
        }
    }
}