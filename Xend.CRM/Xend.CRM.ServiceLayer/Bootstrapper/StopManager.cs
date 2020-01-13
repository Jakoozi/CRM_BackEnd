using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.Core.ServiceLayer.Bootstrapper;

namespace Xend.CRM.ServiceLayer.Bootstrapper
{
    public class StopManager : IStopManager
    {
        public void Stop()
        {

        }

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }
    }
}
