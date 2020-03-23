using ControleFinanceiro.DAL.Base;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.DAL
{
    public class CiclosDAL: RepositoryDAL<Ciclo>, ICiclosDAL
    {
        public CiclosDAL(Contexto contexto): base(contexto)
        {

        }
    }
}
