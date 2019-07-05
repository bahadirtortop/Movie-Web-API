using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movie.Domain.Migrations
{
    public partial class MovieDbInitialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Movie");

            migrationBuilder.CreateTable(
                name: "Movie",
                schema: "Movie",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 250, nullable: false),
                    Year = table.Column<short>(nullable: false),
                    Released = table.Column<DateTime>(type: "datetime", nullable: false),
                    Runtime = table.Column<string>(maxLength: 10, nullable: false),
                    Genre = table.Column<string>(maxLength: 250, nullable: false),
                    Director = table.Column<string>(maxLength: 50, nullable: false),
                    Writer = table.Column<string>(maxLength: 500, nullable: false),
                    Actors = table.Column<string>(maxLength: 500, nullable: false),
                    Language = table.Column<string>(maxLength: 50, nullable: false),
                    Country = table.Column<string>(maxLength: 50, nullable: false),
                    Poster = table.Column<string>(maxLength: 250, nullable: false),
                    ImdbRating = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie",
                schema: "Movie");
        }
    }
}
