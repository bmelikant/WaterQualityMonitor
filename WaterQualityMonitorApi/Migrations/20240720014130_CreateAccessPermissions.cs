using Microsoft.EntityFrameworkCore.Migrations;
using WaterQualityMonitorApi.Models.Permissions;

#nullable disable

namespace WaterQualityMonitorApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateAccessPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Permission",
                columns: [ "Id", "PermissionName" ],
                values: new object [,] {
                    { 1, Permissions.API_Access },
                    { 2, Permissions.API_AdminRead },
                    { 3, Permissions.API_AdminWrite }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Permission",
                keyColumn: "Id",
                keyValues: [ 1, 2, 3 ]
            );
        }
    }
}
