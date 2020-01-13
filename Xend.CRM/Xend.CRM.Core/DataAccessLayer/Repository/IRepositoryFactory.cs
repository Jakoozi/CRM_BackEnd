

using Xend.CRM.ModelLayer.Entities;

namespace Xend.CRM.Core.DataAccessLayer.Repository
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : BASE_ENTITY;
        IRepositoryReadOnly<T> GetReadOnlyRepository<T>() where T : BASE_ENTITY;
    }
}
