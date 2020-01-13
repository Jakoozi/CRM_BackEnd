using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xend.CRM.Core.DataAccessLayer.Repository
{
    public interface IReadRepository<T> where T : class
    {

        IQueryable<T> Query(string sql, params object[] parameters);

        //T Search(params object[] keyValues);
        Task<T> SearchAsync(params object[] keyValues);

        T Single(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);


        Task<T> SingleAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);


        //T Last(Expression<Func<T, bool>> predicate = null,
        // Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        // Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        // bool disableTracking = true);

        Task<T> LastAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        bool disableTracking = true);

        T First(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);

        Task<T> FirstAsync(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool disableTracking = true);





        //IEnumerable<T> GetList(
        //    Expression<Func<T, bool>> predicate = null,
        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        //    bool disableTracking = true);

        Task<IEnumerable<T>> GetListAsync(
           Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool disableTracking = true);

        IQueryable<T> GetQueryableList(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);
    }
}
