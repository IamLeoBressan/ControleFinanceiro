using ControleFinanceiro.DAL.Base;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL
{
    public class GanhosDAL: RepositoryDAL<Ganho>, IGanhosDAL
    {
        private readonly Contexto contexto;
        public GanhosDAL(Contexto contexto): base(contexto)
        {
            this.contexto = contexto;
        }

        public async Task<IList<Ganho>> BuscarGanhosUsuario(string usuario)
        {
            return await contexto.Ganhos
                            .Where(g => g.Ciclo.Plano.Usuario == usuario)
                            .ToListAsync();
        }

        public async Task<bool> ValidaUsuario(string usuario, int ganhoId)
        {
            return await contexto.Ganhos
                            .AnyAsync(g => g.Ciclo.Plano.Usuario == usuario && g.Id == ganhoId);
        }

    }
}
