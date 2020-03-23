using ControleFinanceiro.DAL.Base;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.DAL
{
    public class PlanosDAL: RepositoryDAL<Plano>, IPlanosDAL
    {
        public PlanosDAL(Contexto contexto): base(contexto)
        {

        }
    }
}
