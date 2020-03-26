using ControleFinanceiro.DAL.Base;
using ControleFinanceiro.DAL.Interfaces;
using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL
{
    public class PlanosDAL : RepositoryDAL<Plano>, IPlanosDAL
    {
        private readonly Contexto contexto;
        public PlanosDAL(Contexto contexto) : base(contexto)
        {
            this.contexto = contexto;
        }

        public async Task<IList<Plano>> BuscarPlanosUsuario(string usuario)
        {
            return await contexto.Planos
                            .Where(p => p.Usuario == usuario)
                            .ToListAsync();
        }

        public async Task<bool> ValidaUsuario(string usuario, int planoId)
        {
            return await contexto.Planos
                            .AnyAsync(p => p.Usuario == usuario && p.Id == planoId);
        }
    }
}
