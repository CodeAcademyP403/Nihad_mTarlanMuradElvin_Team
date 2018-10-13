using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogezyTeamWork.Migrations
{
    public partial class CommentRelationChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "ArticleComments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "ArticleComments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComments_AppUserId1",
                table: "ArticleComments",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_AspNetUsers_AppUserId1",
                table: "ArticleComments",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_AspNetUsers_AppUserId1",
                table: "ArticleComments");

            migrationBuilder.DropIndex(
                name: "IX_ArticleComments_AppUserId1",
                table: "ArticleComments");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ArticleComments");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "ArticleComments");
        }
    }
}
