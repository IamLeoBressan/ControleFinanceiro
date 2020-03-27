using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface IGastosDAL: IRepositoryDAL<Gasto>
    {
        Task<IList<Gasto>> BuscarGastosUsuario(string usuario);
        Task<bool> ValidaUsuario(string usuario, int gastoId);
    }
}
