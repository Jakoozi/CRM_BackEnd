using Microsoft.EntityFrameworkCore;
using Xend.CRM.Core.DataAccessLayer.Repository;
using Xend.CRM.DataAccessLayer;

namespace Xend.Core.DataAccessLayer.Repository
{
    public class RepositoryReadOnly<T> : BaseRepository<T>, IRepositoryReadOnly<T> where T : class
    {
        public RepositoryReadOnly(DbContext context) : base(context)
        {
        }
    }
}
