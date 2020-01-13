

using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.Entities;

namespace Xend.CRM.Core.DataAccessLayer.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BASE_ENTITY;

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}
