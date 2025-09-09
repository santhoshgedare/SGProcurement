using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserBuyerVendor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyerCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GstNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GstNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyerUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyerUsers_BuyerCompanies_BuyerCompanyId",
                        column: x => x.BuyerCompanyId,
                        principalTable: "BuyerCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyerUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorUsers_VendorCompanies_VendorCompanyId",
                        column: x => x.VendorCompanyId,
                        principalTable: "VendorCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyerCompanies_IsDeleted",
                table: "BuyerCompanies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerUsers_BuyerCompanyId",
                table: "BuyerUsers",
                column: "BuyerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerUsers_IsDeleted",
                table: "BuyerUsers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerUsers_UserId",
                table: "BuyerUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorCompanies_IsDeleted",
                table: "VendorCompanies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_VendorUsers_IsDeleted",
                table: "VendorUsers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_VendorUsers_UserId",
                table: "VendorUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorUsers_VendorCompanyId",
                table: "VendorUsers",
                column: "VendorCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyerUsers");

            migrationBuilder.DropTable(
                name: "VendorUsers");

            migrationBuilder.DropTable(
                name: "BuyerCompanies");

            migrationBuilder.DropTable(
                name: "VendorCompanies");
        }
    }
}
