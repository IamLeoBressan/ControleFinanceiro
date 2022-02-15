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
        public int AnoContabilizar { get; set; }
        public int MesContabilizar { get; set; }
        [Required]
        [DataMember]
        public int CicloId { get; set; }
        public Ciclo Ciclo { get; set; }

        public bool ValidarTipoEData()
        {
            if (Tipo == TipoMovi.Anual && !ValidaMes(MesContabilizar))
                return false;

            if (Tipo == TipoMovi.Unica && (!ValidaMes(MesContabilizar) || !ValidaAno(AnoContabilizar)))
                return false;

            return true;
        }

        private bool ValidaMes(int mes)
        {
            if (mes > 0 && mes <= 12)
                return true;

            return false;
        }
        private bool ValidaAno(int ano)
        {
            if (ano > 2000 && ano < 3000)
                return true;

            return false;
        }

    }
}