using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace ControleFinanceiro
{
    public class CicloTestes
    {

        [Theory]
        [InlineData(3, 2019, 3100)] // Mensal
        [InlineData(11, 2019, 4100)] // Anual
        [InlineData(1, 2019, 2000)] // Anual
        [InlineData(1, 2020, 2300)] // Unico
        [InlineData(2, 2020, 2700)] // Unico

        public void TesteRendimentoMensal(int mes, int ano, int esperado)
        {
            Ciclo ciclo = new Ciclo()
            {
                JurosMensal = 0.3,
                Ganhos = new List<Ganho>()
                {
                    new Ganho()
                    {
                        Tipo = TipoMovi.Mensal,
                        Titulo = "Sálario",
                        Valor = 3000.00
                    },
                    new Ganho()
                    {
                        Tipo = TipoMovi.Anual,
                        Titulo = "13º",
                        Valor = 3000.0,
                        MesContabilizar = 11
                    },
                    new Ganho()
                    {
                        Tipo = TipoMovi.Unica,
                        Titulo = "Frela",
                        Valor = 300.0,
                        MesContabilizar = 1,
                        AnoContabilizar = 2020
                    }
                },
                Gastos = new List<Gasto>()
                {
                    new Gasto()
                    {
                        Tipo = TipoMovi.Mensal,
                        Titulo = "Aluguel",
                        Valor = 800.0
                    },
                    new Gasto()
                    {
                        Tipo = TipoMovi.Mensal,
                        Titulo = "Contas",
                        Valor = 400.0
                    },
                    new Gasto()
                    {
                        Tipo = TipoMovi.Anual,
                        Titulo = "IPVA",
                        Valor = 1100.00,
                        MesContabilizar = 1
                    },
                    new Gasto()
                    {
                        Tipo = TipoMovi.Unica,
                        Titulo = "Roupas",
                        Valor = 400.0,
                        MesContabilizar = 2,
                        AnoContabilizar = 2020
                    },
                    new Gasto()
                    {
                        Tipo = TipoMovi.Unica,
                        Titulo = "Nintendo Switch",
                        Valor = 2000.0,
                        MesContabilizar = 11,
                        AnoContabilizar = 2019
                    }
                }
            };

            double valorBase = 1000.0;

            var resumo = ciclo.RendimentoMensal(valorBase, mes, ano);

            Assert.Equal(esperado, resumo.ValorTotal);
        }
    }
}
