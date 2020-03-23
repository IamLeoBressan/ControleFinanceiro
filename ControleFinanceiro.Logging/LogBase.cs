using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.Logging
{
    public class LogBase
    {
        public DateTime Date { get; private set; }


        public LogBase()
        {
            Date = DateTime.Now;
        }
    }
}
