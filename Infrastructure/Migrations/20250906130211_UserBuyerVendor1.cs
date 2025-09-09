using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserBuyerVendor1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmployeeCode",
                table: "VendorUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Designation",
                table: "VendorUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "VendorUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VendorCompanies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GstNumber",
                table: "VendorCompanies",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "VendorCompanies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Contact",
                table: "VendorCompanies",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "VendorCompanies",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeCode",
                table: "BuyerUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Designation",
                table: "BuyerUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BuyerUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BuyerCompanies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GstNumber",
                table: "BuyerCompanies",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "BuyerCompanies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Contact",
                table: "BuyerCompanies",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "BuyerCompanies",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_VendorUsers_EmployeeCode",
                table: "VendorUsers",
                column: "EmployeeCode");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCompanies_Email",
                table: "VendorCompanies",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCompanies_GstNumber",
                table: "VendorCompanies",
                column: "GstNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorCompanies_Name",
                table: "VendorCompanies",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerUsers_EmployeeCode",
                table: "BuyerUsers",
                column: "EmployeeCode");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerCompanies_Email",
                table: "BuyerCompanies",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerCompanies_GstNumber",
                table: "BuyerCompanies",
                column: "GstNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BuyerCompanies_Name",
                table: "BuyerCompanies",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VendorUsers_EmployeeCode",
                table: "VendorUsers");

            migrationBuilder.DropIndex(
                name: "IX_VendorCompanies_Email",
                table: "VendorCompanies");

            migrationBuilder.DropIndex(
                name: "IX_VendorCompanies_GstNumber",
                table: "VendorCompanies");

            migrationBuilder.DropIndex(
                name: "IX_VendorCompanies_Name",
                table: "VendorCompanies");

            migrationBuilder.DropIndex(
                name: "IX_BuyerUsers_EmployeeCode",
                table: "BuyerUsers");

            migrationBuilder.DropIndex(
                name: "IX_BuyerCompanies_Email",
                table: "BuyerCompanies");

            migrationBuilder.DropIndex(
                name: "IX_BuyerCompanies_GstNumber",
                table: "BuyerCompanies");

            migrationBuilder.DropIndex(
                name: "IX_BuyerCompanies_Name",
                table: "BuyerCompanies");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "VendorUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "BuyerUsers");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeCode",
                table: "VendorUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Designation",
                table: "VendorUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VendorCompanies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "GstNumber",
                table: "VendorCompanies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "VendorCompanies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Contact",
                table: "VendorCompanies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "VendorCompanies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeCode",
                table: "BuyerUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Designation",
                table: "BuyerUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BuyerCompanies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "GstNumber",
                table: "BuyerCompanies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "BuyerCompanies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Contact",
                table: "BuyerCompanies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "BuyerCompanies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
