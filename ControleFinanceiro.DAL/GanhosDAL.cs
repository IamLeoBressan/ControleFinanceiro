using ControleFinanceiro.DAL.Base;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.DAL
{
    public class GanhosDAL: RepositoryDAL<Ganho>, IGanhosDAL
    {
        public GanhosDAL(Contexto contexto): base(contexto)
        {

        }
        
    }
}
