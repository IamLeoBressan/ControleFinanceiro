using System.ComponentModel;

namespace ControleFinanceiro.Models{
    public enum TipoMovi{
        [Description("Mensal")]
        Mensal = 0,
        [Description("Anual")]
        Anual = 1,
        [Description("Única")]
        Unica = 2
    }
}