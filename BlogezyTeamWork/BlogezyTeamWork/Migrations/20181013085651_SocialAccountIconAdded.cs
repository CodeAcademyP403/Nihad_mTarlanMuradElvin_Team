using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogezyTeamWork.Migrations
{
    public partial class SocialAccountIconAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "SocialAccounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "SocialAccounts");
        }
    }
}
