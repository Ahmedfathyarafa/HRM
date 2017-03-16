using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HRM.Domain;

namespace HRM.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Properties

        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        #endregion Properties

        #region Methods

        TEntity GetById(int id);

        Task<TEntity> GetByIdAsync(int id);

        TEntity GetFirstOrDefault(Func<TEntity, bool> where);

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> where);

        TEntity Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void Delete(int id);

        void Delete(Func<TEntity, bool> where);

        void Delete(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        int SaveChanges();

        Task<int> SaveChangesAsync();

        #endregion Methods
    }
}