using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFinanceiro.DAL.Migrations
{
    public partial class AjustaIdObrigatorio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ganhos_Ciclos_CicloId",
                table: "Ganhos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Ciclos_CicloId",
                table: "Gastos");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Planos",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Gastos",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CicloId",
                table: "Gastos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Ganhos",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CicloId",
                table: "Ganhos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Ciclos",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ganhos_Ciclos_CicloId",
                table: "Ganhos",
                column: "CicloId",
                principalTable: "Ciclos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Ciclos_CicloId",
                table: "Gastos",
                column: "CicloId",
                principalTable: "Ciclos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ganhos_Ciclos_CicloId",
                table: "Ganhos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Ciclos_CicloId",
                table: "Gastos");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Planos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Gastos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "CicloId",
                table: "Gastos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Ganhos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "CicloId",
                table: "Ganhos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Ciclos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Ganhos_Ciclos_CicloId",
                table: "Ganhos",
                column: "CicloId",
                principalTable: "Ciclos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Ciclos_CicloId",
                table: "Gastos",
                column: "CicloId",
                principalTable: "Ciclos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
