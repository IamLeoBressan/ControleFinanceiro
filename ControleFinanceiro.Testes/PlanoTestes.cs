using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ControleFinanceiro.Testes
{
    public class PlanoTestes
    {
        [Fact]
        public void PlanoSemConfiguracaoCiclos()
        {
            Plano plano = DadosBaseTestes.GeradorPlanoCompleto();

            plano.ConfigCiclos = null;

            Assert.Throws<ArgumentNullException>(() => plano.PrevisaoRendimentos(10));
        }

        [Fact]
        public void RetornandoDiasCorretosPlanoFinanceiro()
        {
            Plano plano = DadosBaseTestes.GeradorPlanoCompleto();

            List<ResumoFinanceiro> resumoFinanceiros = plano.PrevisaoRendimentos(10);

            Assert.Equal(10, resumoFinanceiros.Count);
        }

        [Theory]
        [InlineData("2020-12", 24828.07)]
        [InlineData("2020-10", 23485.14)]
        [InlineData("2021-01", 25502.55)]
        public void RetornaValorCorretoPrevisaoRendimentos(string mes, double esperado)
        {
            Plano plano = DadosBaseTestes.GeradorPlanoSimples();

            List<ResumoFinanceiro> resumosFinanceiros = plano.PrevisaoRendimentos(10);

            ResumoFinanceiro resumo = resumosFinanceiros
                                        .FirstOrDefault(r => r.AnoMes == mes);

            double valorAtual = Math.Round(resumo.ValorTotal, 2);

            Assert.Equal(esperado, valorAtual);
        }
    }
}
