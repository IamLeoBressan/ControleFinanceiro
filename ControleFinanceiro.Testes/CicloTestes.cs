using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace ControleFinanceiro.Testes
{
    public class CicloTestes
    {

        [Theory]
        [InlineData(3, 2019, 2803)] // Mensal
        [InlineData(11, 2019, 3803)] // Anual
        [InlineData(1, 2019, 1703)] // Anual
        [InlineData(1, 2020, 2003)] // Unico
        [InlineData(2, 2020, 2403)] // Unico

        public void TesteRendimentoMensal(int mes, int ano, int esperado)
        {
            Ciclo ciclo = DadosBaseTestes.GerarCicloCompleto();

            double valorBase = 1000.0;

            var resumo = ciclo.RendimentoMensal(valorBase, ano, mes);

            Assert.Equal(esperado, resumo.ValorTotal);
        }
    }
}
