using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booka.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkplaceUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Workplaces_WorkspaceId",
                table: "Workplaces");

            migrationBuilder.CreateIndex(
                name: "IX_Workplaces_WorkspaceId_Number",
                table: "Workplaces",
                columns: new[] { "WorkspaceId", "Number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Workplaces_WorkspaceId_Number",
                table: "Workplaces");

            migrationBuilder.CreateIndex(
                name: "IX_Workplaces_WorkspaceId",
                table: "Workplaces",
                column: "WorkspaceId");
        }
    }
}
