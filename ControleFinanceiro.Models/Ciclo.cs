using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ControleFinanceiro.Models
{
    [DataContract]
    public class Ciclo
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        [Required]
        public string Titulo { get; set; }
        public DateTime DataCriacao { get; private set; }
        [DataMember]
        [Required]
        public double JurosMensal { get; set; }
        [Required]
        [DataMember]
        public int PlanoId { get; set; }
        public Plano Plano { get; set; }
        public List<Ganho> Ganhos { get; set; }
        public List<Gasto> Gastos { get; set; }

        public Ciclo()
        {
            DataCriacao = DateTime.Now;
        }

        public double LucroMensal(int mes, int ano)
        {
            double ganhoMensal = GanhosMensais() + GanhosAnuais(mes) + GanhosUnicos(mes, ano);
            double gastoMensal = GastosMensais() + GastosAnuais(mes) + GastosUnicos(mes, ano);

            return ganhoMensal - gastoMensal;
        }

        private double GanhosUnicos(int mes, int ano)
        {
            return Ganhos
                    .Where(g => g.Tipo == TipoMovi.Unica && g.MesContabilizar == mes && g.AnoContabilizar == ano)
                    .Sum(g => g.Valor);
        }

        private double GanhosAnuais(int mes)
        {
            return Ganhos
                    .Where(g => g.Tipo == TipoMovi.Anual && g.MesContabilizar == mes)
                    .Sum(g => g.Valor);
        }

        private double GanhosMensais()
        {
            return Ganhos
                    .Where(g => g.Tipo == TipoMovi.Mensal)
                    .Sum(g => g.Valor);
        }

        private double GastosUnicos(int mes, int ano)
        {
            return Gastos
                    .Where(g => g.Tipo == TipoMovi.Unica && g.MesContabilizar == mes && g.AnoContabilizar == ano)
                    .Sum(g => g.Valor);
        }

        private double GastosAnuais(int mes)
        {
            return Gastos
                    .Where(g => g.Tipo == TipoMovi.Anual && g.MesContabilizar == mes)
                    .Sum(g => g.Valor);
        }
        private double GastosMensais()
        {
            return Gastos
                    .Where(g => g.Tipo == TipoMovi.Mensal)
                    .Sum(g => g.Valor);
        }
    }
}
