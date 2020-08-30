using Microsoft.EntityFrameworkCore.Migrations;

namespace CityCab.Libraries.Database.Migrations
{
    public partial class CorrectedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connection_Accounts_AccountID",
                table: "Connection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Connection",
                table: "Connection");

            migrationBuilder.RenameTable(
                name: "Connection",
                newName: "Connections");

            migrationBuilder.RenameIndex(
                name: "IX_Connection_AccountID",
                table: "Connections",
                newName: "IX_Connections_AccountID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connections",
                table: "Connections",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Accounts_AccountID",
                table: "Connections",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Accounts_AccountID",
                table: "Connections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Connections",
                table: "Connections");

            migrationBuilder.RenameTable(
                name: "Connections",
                newName: "Connection");

            migrationBuilder.RenameIndex(
                name: "IX_Connections_AccountID",
                table: "Connection",
                newName: "IX_Connection_AccountID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connection",
                table: "Connection",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Connection_Accounts_AccountID",
                table: "Connection",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
