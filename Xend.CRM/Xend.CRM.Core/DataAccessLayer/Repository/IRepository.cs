using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.Core.DataAccessLayer.Repository
{
    public interface IRepository<T> : IReadRepository<T> where T : class
    {
        T Add(T entity);
        void Add(params T[] entities);
        void AddAndSetTimeStamp(params T[] entities);
        void Add(IEnumerable<T> entities);
        void AddAndSetTimeStamp(IEnumerable<T> entities);

        //void Delete(T entity);
        void Delete(object id);
        void Delete(params T[] entities);
        void Delete(IEnumerable<T> entities);


        void Update(T entity);
        void Update(params T[] entities);
        void UpdateAndSetTimeStamp(params T[] entities);

        void Update(IEnumerable<T> entities);
        void UpdateAndSetTimeStamp(IEnumerable<T> entities);

    }
}
