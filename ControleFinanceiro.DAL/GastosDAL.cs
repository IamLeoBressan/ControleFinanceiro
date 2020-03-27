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
    public class GastosDAL: RepositoryDAL<Gasto>, IGastosDAL
    {
        private readonly Contexto contexto;
        public GastosDAL(Contexto contexto): base(contexto)
        {
            this.contexto = contexto;
        }

        public async Task<IList<Gasto>> BuscarGastosUsuario(string usuario)
        {
            return await contexto.Gastos
                            .Where(g => g.Ciclo.Plano.Usuario == usuario)
                            .ToListAsync();
        }

        public async Task<bool> ValidaUsuario(string usuario, int gastoId)
        {
            return await contexto.Gastos
                            .AnyAsync(g => g.Ciclo.Plano.Usuario == usuario && g.Id == gastoId);
        }
    }
}
