using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.Core.ServiceLayer.Bootstrapper;

namespace Xend.CRM.ServiceLayer.Bootstrapper
{
    public class StartManager : IStartManager
    {
        public void Start()
        {

        }

        public Task StartAsync()
        {
            return Task.CompletedTask;
        }
    }
}
