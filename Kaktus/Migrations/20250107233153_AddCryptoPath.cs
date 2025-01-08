﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaktus.Migrations
{
    /// <inheritdoc />
    public partial class AddCryptoPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CryptoPath",
                table: "Files",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CryptoPath",
                table: "Files");
        }
    }
}
