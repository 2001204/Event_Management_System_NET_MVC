using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationIdentity.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventDetail_EventCategory_EventCategoryId",
                table: "EventDetail");

            migrationBuilder.DropIndex(
                name: "IX_EventDetail_EventCategoryId",
                table: "EventDetail");

            migrationBuilder.DropColumn(
                name: "EventCategoryId",
                table: "EventDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventCategoryId",
                table: "EventDetail",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EventDetail_EventCategoryId",
                table: "EventDetail",
                column: "EventCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventDetail_EventCategory_EventCategoryId",
                table: "EventDetail",
                column: "EventCategoryId",
                principalTable: "EventCategory",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
