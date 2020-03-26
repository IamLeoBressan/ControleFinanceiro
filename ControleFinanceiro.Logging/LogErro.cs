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

        public LogErro(Exception ex)
        {
            Mensagem = ex.Message;
            CallStack = ex.StackTrace;
        }

        public LogErro(Exception ex, string mensagem): this(ex)
        {
            Complementos = mensagem;
        }
        
    }
}
