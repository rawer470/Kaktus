using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaktus.Migrations
{
    /// <inheritdoc />
    public partial class CreatePassForFile3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Files",
                newName: "PasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Files",
                newName: "Password");
        }
    }
}
