using ControleFinanceiro.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Base
{
    public class RepositoryDAL<TEntity> : IRepositoryDAL<TEntity> where TEntity : class
    {
        private readonly Contexto contexto;
        public RepositoryDAL(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<IList<TEntity>> GetALL()
        {
            return await contexto.Set<TEntity>()
                            .ToListAsync();
        }

        public async Task<TEntity> Find(int id)
        {
            return await contexto.Set<TEntity>()
                        .FindAsync(id);
        }

        public async Task Create(TEntity entity)
        {
            await contexto.Set<TEntity>().AddAsync(entity);
            await contexto.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            contexto.Update<TEntity>(entity);
            await contexto.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            TEntity entity = await Find(id);
            contexto.Set<TEntity>().Remove(entity);
            await contexto.SaveChangesAsync();
        }


    }
}
