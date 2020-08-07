using Panucci.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Panucci.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DB db;
        public Repository(DB db)
        {
            this.db = db;
        }

        public void AddOrUpdate(TEntity entity)
        {
            db.Set<TEntity>().Update(entity);
        }

        public IEnumerable<TEntity> AllData()
        {
            return db.Set<TEntity>().ToList();
        }

        public void Delete(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
        }

        public TEntity Find(int ID)
        {
            return db.Set<TEntity>().Find(ID);
        }

        public IEnumerable<TEntity> FindData(Expression<Func<TEntity, bool>> func)
        {
            return db.Set<TEntity>().Where(func);
        }

        public TEntity FindElement(Expression<Func<TEntity, bool>> func)
        {
            return db.Set<TEntity>().FirstOrDefault(func);
        }

    }
}
