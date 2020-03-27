using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface ICiclosDAL: IRepositoryDAL<Ciclo>
    {
        Task<bool> ValidaUsuario(string usuario, int cicloId);
        Task<IList<Ciclo>> BuscarCiclosUsuario(string usuario);

    }
}
