using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.ResponseModel;

namespace Xend.CRM.Core.ServiceLayer
{
    public interface IPaginatedResultService
    {
        PaginatedResult<T> PaginateRecords<T, W>(int page, int pageSize, IEnumerable<W> records, Func<IEnumerable<W>, IEnumerable<T>> modelProcessorDelegate)
           where T : class;
        Task<PaginatedResult<T>> PaginateRecordsAsync<T, W>(int page, int pageSize, IEnumerable<W> records, Func<IEnumerable<W>, Task<IEnumerable<T>>> modelProcessorDelegate)
           where T : class;

        PaginatedResult<T> PaginateRecords<T, W>(int page, int pageSize, IEnumerable<W> records)
           where T : class
           where W : class;
    }
}
