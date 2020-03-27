using ControleFinanceiro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface IGanhosDAL : IRepositoryDAL<Ganho>
    {
        Task<IList<Ganho>> BuscarGanhosUsuario(string usuario);
        Task<bool> ValidaUsuario(string usuario, int ganhoId);

    }
}
