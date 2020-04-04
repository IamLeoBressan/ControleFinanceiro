using ControleFinanceiro.DAL.Base;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL
{
    public class ConfigCiclosDAL : RepositoryDAL<ConfigCiclos>, IConfigCiclosDAL
    {
        private readonly Contexto contexto;
        public ConfigCiclosDAL(Contexto contexto): base(contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> ValidaUsuario(string usuario, int configCiclosId)
        {
            return await contexto.Ciclos
                            .AnyAsync(c => c.Plano.Usuario == usuario && c.Id == configCiclosId);
        }
    }
}
