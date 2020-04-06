using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Helpers;
using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.DAL.Base
{
    public static class InitializeDB
    {
        public static void Initialize(Contexto contexto)
        {
            Plano plano = GeradorPlanoCompleto();

            contexto.Planos.Add(plano);

            contexto.SaveChanges();
        }


        private static Plano GeradorPlanoCompleto()
        {
            Plano plano = new Plano();

            Ciclo cicloCompleto = GerarCicloCompleto();
            Ciclo cicloSimples = GerarCicloSimples();

            plano.Ciclos = new List<Ciclo>()
            {
                cicloCompleto,
                cicloSimples
            };

            plano.Usuario = "catman3";
            plano.Titulo = "Plano de Testes";
            plano.ValorBase = 10000;

            plano.ConfigCiclos = new List<ConfigCiclos>()
            {
                new ConfigCiclos()
                {
                    AnoMes = DatasHelper.DateToAnoMes(DateTime.Now),
                    Ciclo  = cicloCompleto
                },
                new ConfigCiclos()
                {
                    AnoMes = DatasHelper.DateToAnoMes(DateTime.Now.AddMonths(2)),
                    Ciclo = cicloSimples
                }
            };

            return plano;
        }

        private static Plano GeradorPlanoSimples()
        {
            Plano plano = new Plano();

            Ciclo cicloSimples1 = GerarCicloSimples();
            Ciclo cicloSimples2 = GerarCicloSimples2();

            plano.Ciclos = new List<Ciclo>()
            {
                cicloSimples1,
                cicloSimples2
            };

            plano.Titulo = "Plano de Testes Simples";
            plano.ValorBase = 10000;

            plano.ConfigCiclos = new List<ConfigCiclos>()
            {
                new ConfigCiclos()
                {
                    AnoMes = "2020-04",
                    Ciclo  = cicloSimples1
                },
                new ConfigCiclos()
                {
                    AnoMes = "2020-08",
                    Ciclo = cicloSimples2
                }
            };

            return plano;
        }

        private static Ciclo GerarCicloCompleto()
        {
            Ciclo ciclo = new Ciclo()
            {
                Titulo = "Ciclo Completo",
                JurosMensal = 0.003,
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
            return ciclo;
        }

        private static Ciclo GerarCicloSimples()
        {
            Ciclo ciclo = new Ciclo()
            {
                Titulo = "Ciclo simples",
                JurosMensal = 0.005,
                Ganhos = new List<Ganho>()
                {
                    new Ganho()
                    {
                        Tipo = TipoMovi.Mensal,
                        Titulo = "Sálario",
                        Valor = 4000.00
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
                    }
                }
            };
            return ciclo;
        }

        private static Ciclo GerarCicloSimples2()
        {
            Ciclo ciclo = new Ciclo()
            {
                Titulo = "Ciclo Simples 2",
                JurosMensal = 0.003,
                Ganhos = new List<Ganho>()
                {
                    new Ganho()
                    {
                        Tipo = TipoMovi.Mensal,
                        Titulo = "Sálario",
                        Valor = 3000.00
                    }
                },
                Gastos = new List<Gasto>()
                {
                    new Gasto()
                    {
                        Tipo = TipoMovi.Mensal,
                        Titulo = "Aluguel",
                        Valor = 1800.0
                    },
                    new Gasto()
                    {
                        Tipo = TipoMovi.Mensal,
                        Titulo = "Contas",
                        Valor = 600.0
                    }
                }
            };
            return ciclo;
        }

    }
}
