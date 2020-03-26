﻿using System;

namespace ControleFinanceiro.Logging
{
    public interface IGravadorLog
    {
        void GravarLogErro(Exception ex);
        void GravarLogErro(Exception ex, string mensagem);
    }
}