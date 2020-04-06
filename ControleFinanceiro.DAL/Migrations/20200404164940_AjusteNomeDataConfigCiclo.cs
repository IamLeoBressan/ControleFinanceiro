using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFinanceiro.DAL.Migrations
{
    public partial class AjusteNomeDataConfigCiclo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MesAno",
                table: "ConfigCiclos");

            migrationBuilder.AddColumn<string>(
                name: "AnoMes",
                table: "ConfigCiclos",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnoMes",
                table: "ConfigCiclos");

            migrationBuilder.AddColumn<string>(
                name: "MesAno",
                table: "ConfigCiclos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
