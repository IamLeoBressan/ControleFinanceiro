using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFinanceiro.DAL.Migrations
{
    public partial class AdicionandoDataCobrançaGastoseGanhos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnoContabilizar",
                table: "Gastos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MesContabilizar",
                table: "Gastos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AnoContabilizar",
                table: "Ganhos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MesContabilizar",
                table: "Ganhos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnoContabilizar",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "MesContabilizar",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "AnoContabilizar",
                table: "Ganhos");

            migrationBuilder.DropColumn(
                name: "MesContabilizar",
                table: "Ganhos");
        }
    }
}
