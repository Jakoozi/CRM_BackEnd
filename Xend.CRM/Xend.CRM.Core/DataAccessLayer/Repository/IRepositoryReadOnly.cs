using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.Core.DataAccessLayer.Repository
{
    public interface IRepositoryReadOnly<T> : IReadRepository<T> where T : class
    {

    }
}
