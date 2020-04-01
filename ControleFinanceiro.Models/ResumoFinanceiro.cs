using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.Models
{
    public class ResumoFinanceiro
    {
        public string Mes { get; set; }
        public double RendimentoJuros { get; set; }
        public double RedimentoSalario { get; set; }

        public double GastosMes { get; set; }
        public double ValorTotal { get; set; }

        public double LucroMes()
        {
            return RendimentoJuros + RedimentoSalario - GastosMes;
        }
    }
}
