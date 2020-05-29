using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.Core.ServiceLayer;
using Xend.CRM.ModelLayer.ResponseModel;

namespace Xend.CRM.ServiceLayer.PaginationService
{
    public class PaginatedResultService : IPaginatedResultService
    {
        IMapper Mapper { get; }
        public PaginatedResultService(IMapper mapper)
        {
            Mapper = mapper;
        }
        public async Task<PaginatedResult<T>> PaginateRecordsAsync<T, W>(int page, int pageSize, IEnumerable<W> records, Func<IEnumerable<W>, Task<IEnumerable<T>>> modelProcessorDelegate)
            where T : class
        {
            int recordsToSkip = (page - 1) * pageSize;
            int totalRecords = records.Count();
            double totalPages = (double)totalRecords / (double)pageSize;

            int pageCount = int.Parse(Math.Ceiling(totalPages).ToString());

            IEnumerable<W> paginatedRecords = records.Skip(recordsToSkip).Take(pageSize);

            IEnumerable<T> paginatedRecordsDTO = await modelProcessorDelegate(paginatedRecords.ToList());
            PaginatedResult<T> paginatedResult = new PaginatedResult<T>
            {
                Records = paginatedRecordsDTO,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = pageCount,
                TotalRecords = totalRecords
            };
            return paginatedResult;
        }



        public PaginatedResult<T> PaginateRecords<T, W>(int page, int pageSize, IEnumerable<W> records, Func<IEnumerable<W>, IEnumerable<T>> modelProcessorDelegate)
            where T : class
        {
            int recordsToSkip = (page - 1) * pageSize;
            int totalRecords = records.Count();
            double totalPages = (double)totalRecords / (double)pageSize;

            int pageCount = int.Parse(Math.Ceiling(totalPages).ToString());

            IEnumerable<W> paginatedRecords = records.Skip(recordsToSkip).Take(pageSize);

            IEnumerable<T> paginatedRecordsDTO = modelProcessorDelegate(paginatedRecords.ToList());
            PaginatedResult<T> paginatedResult = new PaginatedResult<T>
            {
                Records = paginatedRecordsDTO,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = pageCount,
                TotalRecords = totalRecords
            };
            return paginatedResult;
        }



        public PaginatedResult<T> PaginateRecords<T, W>(int page, int pageSize, IEnumerable<W> records)
            where T : class
            where W : class
        {
            int recordsToSkip = (page - 1) * pageSize;
            int totalRecords = records.Count();
            double totalPages = (double)totalRecords / (double)pageSize;

            int pageCount = int.Parse(Math.Ceiling(totalPages).ToString());

            IEnumerable<W> paginatedRecords = records.Skip(recordsToSkip).Take(pageSize);
            IEnumerable<T> paginatedRecordsDTO = Mapper.Map<IEnumerable<T>>(paginatedRecords);

            PaginatedResult<T> paginatedResult = new PaginatedResult<T>
            {
                Records = paginatedRecordsDTO,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = pageCount,
                TotalRecords = totalRecords
            };
            return paginatedResult;
        }
    }
}
