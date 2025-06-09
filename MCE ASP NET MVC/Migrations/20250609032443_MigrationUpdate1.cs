using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCE_ASP_NET_MVC.Migrations
{
    /// <inheritdoc />
    public partial class MigrationUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "championship_members",
                columns: table => new
                {
                    ChampionshipId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_championship_members", x => new { x.ChampionshipId, x.UserId });
                    table.ForeignKey(
                        name: "FK_championship_members_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_championship_members_championships_ChampionshipId",
                        column: x => x.ChampionshipId,
                        principalTable: "championships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_championship_members_UserId",
                table: "championship_members",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "championship_members");
        }
    }
}
