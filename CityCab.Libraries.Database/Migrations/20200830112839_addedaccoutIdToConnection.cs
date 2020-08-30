using Microsoft.EntityFrameworkCore.Migrations;

namespace CityCab.Libraries.Database.Migrations
{
    public partial class addedaccoutIdToConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Accounts_AccountID",
                table: "Connections");

            migrationBuilder.AlterColumn<int>(
                name: "AccountID",
                table: "Connections",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Accounts_AccountID",
                table: "Connections",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Accounts_AccountID",
                table: "Connections");

            migrationBuilder.AlterColumn<int>(
                name: "AccountID",
                table: "Connections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Accounts_AccountID",
                table: "Connections",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
