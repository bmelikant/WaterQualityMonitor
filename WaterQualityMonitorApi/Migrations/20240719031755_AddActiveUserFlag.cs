using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterQualityMonitorApi.Migrations
{
    /// <inheritdoc />
    public partial class AddActiveUserFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActiveUser",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveUser",
                table: "Users");
        }
    }
}
