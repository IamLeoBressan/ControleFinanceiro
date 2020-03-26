using ControleFinanceiro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface IPlanosDAL: IRepositoryDAL<Plano>
    {
        Task<IList<Plano>> BuscarPlanosUsuario(string usuario);

        Task<bool> ValidaUsuario(string usuario, int planoId);
    }
}
