using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xend.CRM.Common.ExtensionMethods;
using Xend.CRM.Core.DataAccessLayer.Repository;
using Xend.CRM.DataAccessLayer;
using Xend.CRM.ModelLayer.Entities;

namespace Xend.Core.DataAccessLayer.Repository
{
    public class Repository<T> : BaseRepository<T>, IRepository<T> where T : BASE_ENTITY
    {
        public Repository(DbContext context) : base(context)
        {
        }

        public T Add(T entity)
        {
            entity.CreatedAtTimeStamp = entity.CreatedAt.ToTimeStamp();
            entity.UpdatedAtTimeStamp = entity.CreatedAt.ToTimeStamp();
            return _dbSet.Add(entity).Entity;
        }

        public void AddAndSetTimeStamp(params T[] entities)
        {

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            foreach (var entity in entities)
            {
                entity.UpdatedAtTimeStamp = entity.UpdatedAt.ToTimeStamp();
                entity.CreatedAtTimeStamp = entity.CreatedAt.ToTimeStamp();
                _dbSet.Add(entity);
            }
            _dbContext.ChangeTracker.DetectChanges();

        }

        public void Add(params T[] entities)
        {
            _dbSet.AddRange(entities);
        }


        public void AddAndSetTimeStamp(IEnumerable<T> entities)
        {

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            foreach (var entity in entities)
            {
                entity.UpdatedAtTimeStamp = entity.UpdatedAt.ToTimeStamp();
                entity.CreatedAtTimeStamp = entity.CreatedAt.ToTimeStamp();
                _dbSet.Add(entity);
            }
            _dbContext.ChangeTracker.DetectChanges();

        }

        public void Add(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }


        public void Delete(T entity)
        {
            var existing = _dbSet.Find(entity);
            if (existing != null) _dbSet.Remove(existing);
        }


        public void Delete(object id)
        {

            /* 
             * This whole long process is done to just delete a record without loading the whole of the object record into memory. 
             * I must say it is very efficient
             * 
             * If for some reason we can't delete it that way. Then we fall back to our old method of deleting
             
             */

            //var typeInfo = typeof(T).GetTypeInfo();
            //var key = _dbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            //var property = typeInfo.GetProperty(key?.Name);
            //if (property != null)
            //{
            //    var entity = Activator.CreateInstance<T>();
            //    property.SetValue(entity, id);
            //    _dbContext.Entry(entity).State = EntityState.Deleted;
            //}


            //else
            //{
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
            //}
        }

        public void Delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }


        [Obsolete("Method is replaced by GetList")]
        public IEnumerable<T> Get()
        {
            return _dbSet.AsEnumerable();
        }

        [Obsolete("Method is replaced by GetList")]
        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsEnumerable();
        }

        public void Update(T entity)
        {
            entity.UpdatedAtTimeStamp = entity.UpdatedAt.ToTimeStamp();
            _dbSet.Update(entity);
        }

        public void Update(params T[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void UpdateAndSetTimeStamp(params T[] entities)
        {
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            foreach (var entity in entities)
            {
                entity.UpdatedAtTimeStamp = entity.UpdatedAt.ToTimeStamp();
                _dbSet.Update(entity);
            }
            _dbContext.ChangeTracker.DetectChanges();
        }


        public void Update(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void UpdateAndSetTimeStamp(IEnumerable<T> entities)
        {
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            foreach (var entity in entities)
            {
                entity.UpdatedAtTimeStamp = entity.UpdatedAt.ToTimeStamp();
                _dbSet.Update(entity);
            }
            _dbContext.ChangeTracker.DetectChanges();
        }
    }
}
