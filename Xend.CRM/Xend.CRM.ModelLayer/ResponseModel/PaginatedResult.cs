using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.ModelLayer.ResponseModel
{
    public class PaginatedResult<T> where T : class
    {
        public IEnumerable<T> Records { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }


    }
}
