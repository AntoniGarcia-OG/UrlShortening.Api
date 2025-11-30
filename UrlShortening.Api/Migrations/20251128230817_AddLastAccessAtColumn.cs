using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortening.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLastAccessAtColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastAccessAt",
                table: "UrlMappings",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastAccessAt",
                table: "UrlMappings");
        }
    }
}
