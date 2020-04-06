using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFinanceiro.DAL.Migrations
{
    public partial class AdicionaReferenciadeCicloConfiCiclos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCiclos_Planos_PlanoId",
                table: "ConfigCiclos");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigCiclos_CicloId",
                table: "ConfigCiclos",
                column: "CicloId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCiclos_Ciclos_CicloId",
                table: "ConfigCiclos",
                column: "CicloId",
                principalTable: "Ciclos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCiclos_Planos_PlanoId",
                table: "ConfigCiclos",
                column: "PlanoId",
                principalTable: "Planos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCiclos_Ciclos_CicloId",
                table: "ConfigCiclos");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfigCiclos_Planos_PlanoId",
                table: "ConfigCiclos");

            migrationBuilder.DropIndex(
                name: "IX_ConfigCiclos_CicloId",
                table: "ConfigCiclos");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigCiclos_Planos_PlanoId",
                table: "ConfigCiclos",
                column: "PlanoId",
                principalTable: "Planos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
