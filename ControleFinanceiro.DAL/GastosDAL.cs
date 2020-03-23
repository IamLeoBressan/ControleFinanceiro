using ControleFinanceiro.DAL.Base;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.DAL
{
    public class GastosDAL: RepositoryDAL<Gasto>, IGastosDAL
    {
        public GastosDAL(Contexto contexto): base(contexto)
        {

        }
    }
}
