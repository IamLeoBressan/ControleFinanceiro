using ControleFinanceiro.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace ControleFinanceiro.Models
{
    [DataContract]
    public class Plano
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        [Required]
        public string Titulo { get; set; }
        [DataMember]
        public List<Ciclo> Ciclos { get; set; }
        [Required]
        [DataMember]
        public double ValorBase { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        public List<ConfigCiclos> ConfigCiclos { get; set; }

        public List<ResumoFinanceiro> PrevisaoRendimentos(int mesesPrevisao)
        {
            if (ConfigCiclos == null || !ConfigCiclos.Any())
                throw new KeyNotFoundException("Configuração dos ciclos está vazia");

            ConfigCiclos ConfigAtual = ConfigCiclos.OrderBy(c => c.AnoMes)
                                            .FirstOrDefault();

            List<ResumoFinanceiro> resumosFinanceiros = new List<ResumoFinanceiro>();

            string AnoMes = ConfigAtual.AnoMes;
            DatasHelper.TrySplitAnoMes(ConfigAtual.AnoMes, out int ano, out int mes);
            double valorAtual = ValorBase;

            for (int i = 0; i < mesesPrevisao; i++)
            {
                ResumoFinanceiro resumoFinanceiro = ConfigAtual.Ciclo.RendimentoMensal(valorAtual, ano, mes);
                resumosFinanceiros.Add(resumoFinanceiro);

                DatasHelper.AvancaAnoMes(ref ano, ref mes);
                valorAtual = resumoFinanceiro.ValorTotal;
                ConfigAtual = PegarUltimoConfigData(ano, mes);
            }

            return resumosFinanceiros;
        }

        private ConfigCiclos PegarUltimoConfigData(int ano, int mes)
        {
            ConfigCiclos configCiclos = ConfigCiclos
                                            .Where(c => DatasHelper.CompareAnoMesWithSplitedAnoMes(c.AnoMes, ano, mes))
                                            .OrderByDescending(c => c.AnoMes)
                                            .FirstOrDefault();

            return configCiclos;
        }
    }
}