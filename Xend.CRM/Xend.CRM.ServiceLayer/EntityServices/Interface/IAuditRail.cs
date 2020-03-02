using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface IAuditRail
    {
        void LogCreationService();
        void LogDeleteService();
        void GetLogByIdService();
        void GetAllLogsService();
        void UpdateLogService();

    }
}
