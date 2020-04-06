using System;

namespace ControleFinanceiro.Helpers
{
    public static class DatasHelper
    {
        public static string DateToAnoMes(DateTime data)
        {
            string anoMes = $"{data.Year}-{data.Month.ToString().PadLeft(2, '0')}";

            return anoMes;
        }
        public static string AnoMesJoin(int ano, int mes)
        {               
            return $"{ano}-{mes.ToString().PadLeft(2, '0')}";
        }

        public static bool CompareAnoMesWithSplitedAnoMes(string anoMes, int ano, int mes)
        {
            TrySplitAnoMes(anoMes, out int anoComp, out int mesComp);

            return anoComp < ano ? true : anoComp == ano && mesComp <= mes ? true: false ;

        }
        public static bool TrySplitAnoMes(string anoMes, out int ano, out int mes)
        {
            var splitAnoMes = anoMes.Split('-');

            ano = 0;
            mes = 0;

            if (splitAnoMes.Length != 2)
                return false;

            int.TryParse(splitAnoMes[0], out ano);
            int.TryParse(splitAnoMes[1], out mes);

            return true;
        }

        public static void AvancaAnoMes(ref int ano, ref int mes)
        {
            if (mes >= 12)
            {
                ano++;
                mes = 1;
            }
            else
                mes++;

        }
        
    }
}
