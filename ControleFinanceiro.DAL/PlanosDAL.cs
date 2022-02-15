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
            //InitializeDB.Initialize(contexto);
            this.contexto = contexto;
        }
        public async Task<Plano> BuscarPlanoCompleto(int planoId)
        {
            return await contexto.Planos
                            .Include(p => p.Ciclos).ThenInclude(c => c.Ganhos)
                            .Include(p => p.Ciclos).ThenInclude(c => c.Gastos)
                            .Include(p => p.ConfigCiclos)
                            .FirstOrDefaultAsync(p => p.Id == planoId);
                         
        }

        public async Task<IList<Plano>> BuscarPlanosUsuario(string usuario)
        {
            return await contexto.Planos
                        .Where(p => p.Usuario == usuario)
                        .Include(p => p.Ciclos).ThenInclude(c => c.Ganhos)
                        .Include(p => p.Ciclos).ThenInclude(c => c.Gastos)
                        .Include(p => p.ConfigCiclos)
                        .ToListAsync();
        }

        public async Task<bool> ValidaUsuario(string usuario, int planoId)
        {
            return await contexto.Planos
                            .AnyAsync(p => p.Usuario == usuario && p.Id == planoId);
        }
    }
}
