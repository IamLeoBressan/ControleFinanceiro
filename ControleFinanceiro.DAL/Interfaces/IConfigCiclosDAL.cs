using ControleFinanceiro.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface IConfigCiclosDAL: IRepositoryDAL<ConfigCiclos>
    {
        Task<bool> ValidaUsuario(string usuario, int configCiclosId);
    }
}
