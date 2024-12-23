using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booka.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemovePhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactPhoneNumber",
                table: "Workspaces");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ContactEmail",
                table: "Workspaces",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Workspaces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Workspaces");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Workspaces",
                newName: "ContactEmail");

            migrationBuilder.AddColumn<string>(
                name: "ContactPhoneNumber",
                table: "Workspaces",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
