using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xend.CRM.Common
{
	public class PaginationTest
	{
		//public async Task<PaginatedResult<REQUEST_TO_FUND>> FundingHistory(Guid customerId, int page, int pageSize)
		//{
		//	IEnumerable<REQUEST_TO_FUND> funds = UnitOfWork.GetRepository<REQUEST_TO_FUND>().GetQueryableList(p => p.CustomerId == customerId).OrderBy(p => p.CreatedAt);
		//	if (!funds.Any())
		//	{
		//		return new PaginatedResult<REQUEST_TO_FUND>();
		//	}
		//	PaginatedResult<REQUEST_TO_FUND> result = _paginatedResultService.PaginateRecords<REQUEST_TO_FUND, REQUEST_TO_FUND>(page, pageSize, funds);
		//	return result;
		//}
	}
}
