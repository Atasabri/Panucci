using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Panucci.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void AddOrUpdate(TEntity entity);
        IEnumerable<TEntity> AllData();
        void Delete(TEntity entity);

        IEnumerable<TEntity> FindData(Expression<Func<TEntity,bool>> func);
        TEntity Find(int ID);
        TEntity FindElement(Expression<Func<TEntity, bool>> func);
    }
}
