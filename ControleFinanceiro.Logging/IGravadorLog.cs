using System;

namespace ControleFinanceiro.Logging
{
    public interface IGravadorLog
    {
        void GravarLogErro(Exception ex, int codigoErro);
        void GravarLogErro(Exception ex, int codigoErro, string mensagem);
    }
}