using Microsoft.EntityFrameworkCore.Migrations;

namespace BotCore.Infrastructure.Test.Migrations
{
    public partial class addusertelegramId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TelegramId",
                table: "User",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelegramId",
                table: "User");
        }
    }
}
