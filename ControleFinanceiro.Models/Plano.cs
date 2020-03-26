using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ControleFinanceiro.Models{
    [DataContract]
    public class Plano{
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        [Required]
        public string Titulo { get; set; }
        public List<Ciclo> Ciclos { get; set; }
        [DataMember]
        public string Usuario { get; set; }
    }
}