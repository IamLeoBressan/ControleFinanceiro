using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface IRepositoryDAL<TEntity> where TEntity : class
    {
        Task Create(TEntity entity);
        Task Delete(int id);
        Task<TEntity> Find(int id);
        Task<IList<TEntity>> GetALL();
        Task Update(TEntity entity);
    }
}