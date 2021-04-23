using Microsoft.EntityFrameworkCore.Migrations;

namespace Mongoose.Core.Migrations
{
    public partial class Video_Info_Relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VideoInfo_EpisodeId",
                table: "VideoInfo",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoInfo_FilmId",
                table: "VideoInfo",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoInfo_Episodes_EpisodeId",
                table: "VideoInfo",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoInfo_Films_FilmId",
                table: "VideoInfo",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoInfo_Episodes_EpisodeId",
                table: "VideoInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoInfo_Films_FilmId",
                table: "VideoInfo");

            migrationBuilder.DropIndex(
                name: "IX_VideoInfo_EpisodeId",
                table: "VideoInfo");

            migrationBuilder.DropIndex(
                name: "IX_VideoInfo_FilmId",
                table: "VideoInfo");
        }
    }
}
