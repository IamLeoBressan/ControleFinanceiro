using ControleFinanceiro.DAL.Base;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL
{
    public class CiclosDAL: RepositoryDAL<Ciclo>, ICiclosDAL
    {
        private readonly Contexto contexto;
        public CiclosDAL(Contexto contexto): base(contexto)
        {
            this.contexto = contexto;
        }

        public async Task<IList<Ciclo>> BuscarCiclosUsuario(string usuario)
        {
            return await contexto.Ciclos
                            .Where(p => p.Plano.Usuario == usuario)
                            .ToListAsync();
        }

        public async Task<bool> ValidaUsuario(string usuario, int cicloId)
        {
            return await contexto.Ciclos
                            .AnyAsync(c => c.Plano.Usuario == usuario && c.Id == cicloId);
        }
    }
}
