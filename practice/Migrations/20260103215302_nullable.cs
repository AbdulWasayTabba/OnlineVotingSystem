using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace practice.Migrations
{
    /// <inheritdoc />
    public partial class nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Elections_ElectionId",
                table: "Candidates");

            migrationBuilder.AlterColumn<int>(
                name: "ElectionId",
                table: "Candidates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2026, 1, 3, 21, 53, 2, 452, DateTimeKind.Utc).AddTicks(9842), new DateTime(2026, 2, 3, 21, 53, 2, 452, DateTimeKind.Utc).AddTicks(9809), new DateTime(2026, 1, 3, 21, 53, 2, 452, DateTimeKind.Utc).AddTicks(9809) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 3, 21, 53, 2, 452, DateTimeKind.Utc).AddTicks(9176), "$2a$11$s8WK4Z8q/CD4RHA.0xRFv.3NVi5J36C9PLy5Gkh.qNL4zVjpoUP/G" });

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Elections_ElectionId",
                table: "Candidates",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Elections_ElectionId",
                table: "Candidates");

            migrationBuilder.AlterColumn<int>(
                name: "ElectionId",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2026, 1, 3, 20, 41, 25, 126, DateTimeKind.Utc).AddTicks(1351), new DateTime(2026, 2, 3, 20, 41, 25, 126, DateTimeKind.Utc).AddTicks(1318), new DateTime(2026, 1, 3, 20, 41, 25, 126, DateTimeKind.Utc).AddTicks(1317) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 3, 20, 41, 25, 126, DateTimeKind.Utc).AddTicks(374), "$2a$11$7mXMkFU8k/FpIatnZhb7TOte4U9EcbUtzRlay1.srLJeeiZ8LmG8G" });

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Elections_ElectionId",
                table: "Candidates",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
