using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.Models
{
    public class ResumoFinanceiro
    {
        public string AnoMes { get; set; }
        public double RendimentoJuros { get; set; }
        public double RendimentoSalario { get; set; }

        public double GastosMes { get; set; }
        public double Lucro => RendimentoJuros + RendimentoSalario - GastosMes;
        public double ValorTotal { get; set; }
    }
}
