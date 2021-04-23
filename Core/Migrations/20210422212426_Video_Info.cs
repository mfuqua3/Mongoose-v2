using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Mongoose.Core.Migrations
{
    public partial class Video_Info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Films_FilePath",
                table: "Films");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_FilePath",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Episodes");

            migrationBuilder.AddColumn<int>(
                name: "VideoInfoId",
                table: "Films",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoInfoId",
                table: "Episodes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VideoInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    EpisodeId = table.Column<int>(type: "integer", nullable: true),
                    FilmId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Films_VideoInfoId",
                table: "Films",
                column: "VideoInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_VideoInfoId",
                table: "Episodes",
                column: "VideoInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoInfo_FilePath",
                table: "VideoInfo",
                column: "FilePath",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_VideoInfo_VideoInfoId",
                table: "Episodes",
                column: "VideoInfoId",
                principalTable: "VideoInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Films_VideoInfo_VideoInfoId",
                table: "Films",
                column: "VideoInfoId",
                principalTable: "VideoInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_VideoInfo_VideoInfoId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Films_VideoInfo_VideoInfoId",
                table: "Films");

            migrationBuilder.DropTable(
                name: "VideoInfo");

            migrationBuilder.DropIndex(
                name: "IX_Films_VideoInfoId",
                table: "Films");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_VideoInfoId",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "VideoInfoId",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "VideoInfoId",
                table: "Episodes");

            migrationBuilder.AddColumn<long>(
                name: "Duration",
                table: "Films",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Films",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Duration",
                table: "Episodes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Episodes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Films_FilePath",
                table: "Films",
                column: "FilePath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_FilePath",
                table: "Episodes",
                column: "FilePath",
                unique: true);
        }
    }
}
