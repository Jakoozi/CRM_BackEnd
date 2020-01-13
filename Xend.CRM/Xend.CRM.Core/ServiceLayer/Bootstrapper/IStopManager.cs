using System.Threading.Tasks;

namespace Xend.CRM.Core.ServiceLayer.Bootstrapper
{
    public interface IStopManager
    {
        Task StopAsync();
        void Stop();
    }
}
