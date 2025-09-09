using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserBuyerVendor1p : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserAgent",
                table: "UserSessions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "UserSessions",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Browser",
                table: "UserSessions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeviceType",
                table: "UserSessions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ExpiredAutomatically",
                table: "UserSessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserSessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "UserSessions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OperatingSystem",
                table: "UserSessions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_IsActive",
                table: "UserSessions",
                column: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSessions_IsActive",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "Browser",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "DeviceType",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "ExpiredAutomatically",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "OperatingSystem",
                table: "UserSessions");

            migrationBuilder.AlterColumn<string>(
                name: "UserAgent",
                table: "UserSessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "UserSessions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);
        }
    }
}
