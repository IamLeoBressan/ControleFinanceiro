using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFinanceiro.DAL.Migrations
{
    public partial class AjustesPlanos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ganhos_Planos_PlanoId",
                table: "Ganhos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Planos_PlanoId",
                table: "Gastos");

            migrationBuilder.DropIndex(
                name: "IX_Gastos_PlanoId",
                table: "Gastos");

            migrationBuilder.DropIndex(
                name: "IX_Ganhos_PlanoId",
                table: "Ganhos");

            migrationBuilder.DropColumn(
                name: "JurosMensal",
                table: "Planos");

            migrationBuilder.DropColumn(
                name: "PlanoId",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "PlanoId",
                table: "Ganhos");

            migrationBuilder.AddColumn<int>(
                name: "CicloId",
                table: "Gastos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CicloId",
                table: "Ganhos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ciclos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    JurosMensal = table.Column<double>(nullable: false),
                    PlanoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciclos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ciclos_Planos_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Planos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_CicloId",
                table: "Gastos",
                column: "CicloId");

            migrationBuilder.CreateIndex(
                name: "IX_Ganhos_CicloId",
                table: "Ganhos",
                column: "CicloId");

            migrationBuilder.CreateIndex(
                name: "IX_Ciclos_PlanoId",
                table: "Ciclos",
                column: "PlanoId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ganhos_Ciclos_CicloId",
                table: "Ganhos");

            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_Ciclos_CicloId",
                table: "Gastos");

            migrationBuilder.DropTable(
                name: "Ciclos");

            migrationBuilder.DropIndex(
                name: "IX_Gastos_CicloId",
                table: "Gastos");

            migrationBuilder.DropIndex(
                name: "IX_Ganhos_CicloId",
                table: "Ganhos");

            migrationBuilder.DropColumn(
                name: "CicloId",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "CicloId",
                table: "Ganhos");

            migrationBuilder.AddColumn<double>(
                name: "JurosMensal",
                table: "Planos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PlanoId",
                table: "Gastos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanoId",
                table: "Ganhos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_PlanoId",
                table: "Gastos",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ganhos_PlanoId",
                table: "Ganhos",
                column: "PlanoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ganhos_Planos_PlanoId",
                table: "Ganhos",
                column: "PlanoId",
                principalTable: "Planos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_Planos_PlanoId",
                table: "Gastos",
                column: "PlanoId",
                principalTable: "Planos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
