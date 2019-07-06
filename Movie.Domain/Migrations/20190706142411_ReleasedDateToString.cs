using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movie.Domain.Migrations
{
    public partial class ReleasedDateToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Released",
                schema: "Movie",
                table: "Movie",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Released",
                schema: "Movie",
                table: "Movie",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }
    }
}
