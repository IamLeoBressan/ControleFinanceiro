using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ControleFinanceiro.Models
{
    [DataContract]
    public class ConfigCiclos
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        [Required]
        public int CicloId { get; set; }

        public Ciclo Ciclo { get; set; }

        [Required]
        [DataMember]
        public string AnoMes { get; set; }
        [Required]
        [DataMember]
        public int PlanoId { get; set; }
        public Plano Plano { get; set; }
    }
}
