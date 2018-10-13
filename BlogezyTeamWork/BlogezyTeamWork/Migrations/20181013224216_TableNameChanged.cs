using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogezyTeamWork.Migrations
{
    public partial class TableNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_AspNetUsers_AppUserId1",
                table: "ArticleComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_Articles_ArticleId",
                table: "ArticleComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_Comments_CommentId",
                table: "ArticleComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleComments",
                table: "ArticleComments");

            migrationBuilder.DropIndex(
                name: "IX_ArticleComments_AppUserId1",
                table: "ArticleComments");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "ArticleComments");

            migrationBuilder.RenameTable(
                name: "ArticleComments",
                newName: "ArticleUserComments");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleComments_CommentId",
                table: "ArticleUserComments",
                newName: "IX_ArticleUserComments_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleComments_ArticleId",
                table: "ArticleUserComments",
                newName: "IX_ArticleUserComments_ArticleId");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "ArticleUserComments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleUserComments",
                table: "ArticleUserComments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleUserComments_AppUserId",
                table: "ArticleUserComments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleUserComments_AspNetUsers_AppUserId",
                table: "ArticleUserComments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleUserComments_Articles_ArticleId",
                table: "ArticleUserComments",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleUserComments_Comments_CommentId",
                table: "ArticleUserComments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleUserComments_AspNetUsers_AppUserId",
                table: "ArticleUserComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleUserComments_Articles_ArticleId",
                table: "ArticleUserComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleUserComments_Comments_CommentId",
                table: "ArticleUserComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleUserComments",
                table: "ArticleUserComments");

            migrationBuilder.DropIndex(
                name: "IX_ArticleUserComments_AppUserId",
                table: "ArticleUserComments");

            migrationBuilder.RenameTable(
                name: "ArticleUserComments",
                newName: "ArticleComments");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleUserComments_CommentId",
                table: "ArticleComments",
                newName: "IX_ArticleComments_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleUserComments_ArticleId",
                table: "ArticleComments",
                newName: "IX_ArticleComments_ArticleId");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "ArticleComments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "ArticleComments",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleComments",
                table: "ArticleComments",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_Articles_ArticleId",
                table: "ArticleComments",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_Comments_CommentId",
                table: "ArticleComments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
