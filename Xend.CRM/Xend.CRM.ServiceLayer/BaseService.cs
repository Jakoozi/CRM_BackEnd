using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.Core.DataAccessLayer.Repository;
using Xend.CRM.ModelLayer.DbContexts;

namespace Xend.CRM.ServiceLayer
{
    public abstract class BaseService
    {
        protected IUnitOfWork<XendDbContext> UnitOfWork { get; set; }


        protected IMapper Mapper { get; set; }

        public BaseService(IUnitOfWork<XendDbContext> unitOfWork, IMapper mapper)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }
    }
}
