using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.Logging
{
    public class LogErro : LogBase
    {
        public string Mensagem { get; private set; }
        public string CallStack { get; private set; }
        public string Complementos { get; set; }
        public int CodigoErro { get; set; }

        public LogErro(Exception ex, int codigoErro)
        {
            Mensagem = ex.Message;
            CallStack = ex.StackTrace;
            CodigoErro = codigoErro;
        }

        public LogErro(Exception ex, int codigoErro, string mensagem): this(ex, codigoErro)
        {
            Complementos = mensagem;
        }
        
    }
}
