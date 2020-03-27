using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    }
}
