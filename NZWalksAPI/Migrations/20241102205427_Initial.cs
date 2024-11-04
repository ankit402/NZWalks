using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "difficulty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_difficulty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "regions",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionImageurl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regions", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "walks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LengthInKm = table.Column<double>(type: "float", nullable: false),
                    WalkImageurl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DifficultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_walks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_walks_difficulty_DifficultyId",
                        column: x => x.DifficultyId,
                        principalTable: "difficulty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_walks_regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "regions",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_walks_DifficultyId",
                table: "walks",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_walks_RegionId",
                table: "walks",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "walks");

            migrationBuilder.DropTable(
                name: "difficulty");

            migrationBuilder.DropTable(
                name: "regions");
        }
    }
}
