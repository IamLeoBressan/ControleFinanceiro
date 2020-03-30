
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace ControleFinanceiro.Logging
{
    public class GravadorLog : IGravadorLog
    {
        private readonly string path;
        private readonly string aplicacao;

        public GravadorLog(string path, string aplicacao)
        {
            this.path = path;
            this.aplicacao = aplicacao;
        }
        private void ValidarDiretorioExiste(string file)
        {
            string directoryPath = Path.GetDirectoryName(file);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        private string geraCaminhoCompleto(string arquivo)
        {
            DateTime date = DateTime.Now;
            return Path.Combine(path, aplicacao, date.Year.ToString(), date.Month.ToString(), date.Day.ToString(), arquivo + ".txt");
        }

        public string ValidaArquivoVazio(string path)
        {
            if (new FileInfo(path).Length <= 0)
                return string.Empty;

            return ", ";
        }

        private void GravarArquivo(string mensagem, string newPath)
        {
            ValidarDiretorioExiste(newPath);

            using FileStream fs = new FileStream(newPath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(ValidaArquivoVazio(newPath) + mensagem);
            sw.Flush();
        }
        public void GravarLogErro(Exception ex, int codigoErro, string mensagem)
        {
            LogErro logErro = new LogErro(ex, codigoErro, mensagem);

            GeraJsonParaLog(logErro);
        }
        public void GravarLogErro(Exception ex, int codigoErro)
        {
            LogErro logErro = new LogErro(ex, codigoErro);

            GeraJsonParaLog(logErro);
        }
        private void GeraJsonParaLog(LogErro log)
        {
            string newPath = geraCaminhoCompleto("LogErro");

            
            string logTexto = JsonConvert.SerializeObject(log, Formatting.Indented);

            GravarArquivo(logTexto, newPath);
        }
    }
}
