using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ControleFinanceiro.Models
{
    [DataContract]
    public class MoviFinanceira
    {
        [DataMember]
        public int? Id { get; set; }
        [Required]
        [DataMember]
        public string Titulo { get; set; }
        [Required]
        [DataMember]
        public double Valor { get; set; }
        [Required]
        [DataMember]
        public TipoMovi Tipo { get; set; }
        [Required]
        [DataMember]
        public int? CicloId { get; set; }
        public Ciclo Ciclo { get; set; }

    }
}