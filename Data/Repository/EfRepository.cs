using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using HRM.Domain;

namespace HRM.Data.Repository
{
        /// <summary>
        ///  Generic Repository class for Entity Operations
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
        {
            #region Fields

            private readonly DbContext _context;

            private DbSet<TEntity> _dbSet;

            #endregion Fields

            #region Properties

            public virtual IQueryable<TEntity> Table
            {
                get
                {
                    return this.Entities;
                }
            }

            public virtual IQueryable<TEntity> TableNoTracking
            {
                get
                {
                    return this.Entities.AsNoTracking();
                }
            }

            private DbSet<TEntity> Entities
            {
                get
                {
                    if (this._dbSet == null)
                        this._dbSet = _context.Set<TEntity>();
                    return this._dbSet;
                }
            }

            #endregion Properties

            #region Constructors

            /// <summary>
            ///  Public Constructor,initializes privately declared local variables.
            /// </summary>
            /// <param name="context"></param>
            public EfRepository(DbContext context)
            {
                this._context = context;

            }

            #endregion Constructors

            #region Utilities

            /// <summary>
            /// Get full error
            /// </summary>
            /// <param name="exc">Exception</param>
            /// <returns>Error</returns>
            protected string GetFullErrorText(DbEntityValidationException exc)
            {
                var msg = string.Empty;
                foreach (var validationErrors in exc.EntityValidationErrors)
                    foreach (var error in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
                return msg;
            }

            #endregion

            #region Methods
            public virtual TEntity GetById(int  id)
            {
                return this.Entities.Find(id);
            }

            public async virtual Task<TEntity> GetByIdAsync(int id)
            {
                var entity = await this.Entities.FindAsync(id);
                return entity;
            }

            /// <summary>
            /// generic get method , fetches data for the entities on the basis of condition.
            /// </summary>
            /// <param name="where"></param>
            /// <returns></returns>
            public TEntity GetFirstOrDefault(Func<TEntity, bool> where)
            {
                return this.Entities.Where(where).FirstOrDefault<TEntity>();
            }

            public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> where)
            {
                var entity = await this.Entities.FirstOrDefaultAsync<TEntity>(where);
                return entity;
            }

            /// <summary>
            /// Insert entity
            /// </summary>
            /// <param name="entity">Entity</param>
            public virtual TEntity Insert(TEntity entity)
            {
                try
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");

                    var result = this.Entities.Add(entity);
                    this._context.SaveChanges();
                    return result;
                }
                catch (DbEntityValidationException dbEx)
                {
                    throw new Exception(GetFullErrorText(dbEx), dbEx);
                }
            }

            /// <summary>
            /// Insert entities
            /// </summary>
            /// <param name="entities">Entities</param>
            public virtual void Insert(IEnumerable<TEntity> entities)
            {
                try
                {
                    if (entities == null)
                        throw new ArgumentNullException("entities");

                    foreach (var entity in entities)
                        this.Entities.Add(entity);

                    this._context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    throw new Exception(GetFullErrorText(dbEx), dbEx);
                }


            }

            public virtual void Delete(TEntity entity)
            {
                try
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");

                    this.Entities.Remove(entity);

                    this._context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    throw new Exception(GetFullErrorText(dbEx), dbEx);
                }
            }

            public virtual void Delete(int  id)
            {
                TEntity entityToDelete = this.Entities.Find(id);
                this.Delete(entityToDelete);
            }

            /// <summary>
            /// generic delete method , deletes data for the entities on the basis of condition.
            /// </summary>
            /// <param name="where"></param>
            /// <returns></returns>
            public virtual void Delete(Func<TEntity, bool> where)
            {
                try
                {
                    if (where == null)
                        throw new ArgumentException(nameof(where));

                    IQueryable<TEntity> objects = Entities.Where(where).AsQueryable();
                    foreach (TEntity obj in objects)
                        this.Entities.Remove(obj);

                    this._context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    throw new Exception(GetFullErrorText(dbEx), dbEx);
                }

            }

            /// <summary>
            /// Delete entities
            /// </summary>
            /// <param name="entities">Entities</param>
            public virtual void Delete(IEnumerable<TEntity> entities)
            {
                try
                {
                    if (entities == null)
                        throw new ArgumentNullException("entities");

                    foreach (var entity in entities)
                        this.Entities.Remove(entity);

                    this._context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    throw new Exception(GetFullErrorText(dbEx), dbEx);
                }

            }

            public virtual void Update(TEntity entity)
            {
                try
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");
                    _context.Entry(entity).State = EntityState.Modified;
                    this._context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    throw new Exception(GetFullErrorText(dbEx), dbEx);
                }
            }

            /// <summary>
            /// Update entities
            /// </summary>
            /// <param name="entities">Entities</param>
            public virtual void Update(IEnumerable<TEntity> entities)
            {
                try
                {
                    if (entities == null)
                        throw new ArgumentNullException(nameof(entities));


                    foreach (var entity in entities)
                        this.Entities.Add(entity);

                    this._context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    throw new Exception(GetFullErrorText(dbEx), dbEx);
                }
            }

            public virtual int SaveChanges()
            {
                return _context.SaveChanges();
            }

            public async virtual Task<int> SaveChangesAsync()
            {
                return await _context.SaveChangesAsync();
            }
            #endregion
        }
    }