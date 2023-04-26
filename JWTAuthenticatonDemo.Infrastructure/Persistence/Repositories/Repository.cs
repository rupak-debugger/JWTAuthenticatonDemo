using JWTAuthenticatonDemo.Application.Contracts.Repositories;
using JWTAuthenticatonDemo.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Infrastructure.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Create

        public TEntity Add(TEntity entity)
        {
            _dbContext.Add(entity);
            return entity;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }
        #endregion

        #region Read

        public IQueryable<TEntity> Queryable
        {
            get
            {
                return GetQueryable();
            }
        }
        public IQueryable<TEntity> GetQueryable()
        {
            return _dbContext.Set<TEntity>();
        }
        public List<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(where);
        }
        public async Task<bool>AnyAsync()
        {
            return await _dbContext.Set<TEntity>().AnyAsync();
        }

        #endregion

        #region Update 

        public TEntity Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return entity;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                _dbContext.Set<TEntity>().Update(entity);
            });
            return entity;
        }

        public void UpdateRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task UpdateRangeAsync(List<TEntity> entities)
        {
            await Task.Run(() =>
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.Entry(entity).State = EntityState.Modified;
                    }

                });
        }

        #endregion

        #region Delete
        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(List<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                //_dbContext.Set<TEntity>().Remove(entity);
                _dbContext.Entry(entity).State = EntityState.Deleted;
            });
        }

        public async Task RemoveRangeAsync(List<TEntity> entities)
        {
            await Task.Run(() =>
           {
               foreach (var entity in entities)
               {
                   _dbContext.Entry(entity).State = EntityState.Deleted;
               }
               //_dbContext.RemoveRange(entities);
           });
        }
        #endregion

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
