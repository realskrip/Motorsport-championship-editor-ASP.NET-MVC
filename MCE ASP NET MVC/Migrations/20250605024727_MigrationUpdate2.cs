using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCE_ASP_NET_MVC.Migrations
{
    /// <inheritdoc />
    public partial class MigrationUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "granprixes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ChampionshipId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Game = table.Column<string>(type: "text", nullable: false),
                    Discipline = table.Column<string>(type: "text", nullable: false),
                    CarClass = table.Column<string>(type: "text", nullable: false),
                    Track = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_granprixes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_granprixes_championships_ChampionshipId",
                        column: x => x.ChampionshipId,
                        principalTable: "championships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_granprixes_ChampionshipId",
                table: "granprixes",
                column: "ChampionshipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "granprixes");
        }
    }
}
