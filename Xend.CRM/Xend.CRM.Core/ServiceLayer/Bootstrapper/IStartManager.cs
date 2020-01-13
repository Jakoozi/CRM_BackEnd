using System.Threading.Tasks;

namespace Xend.CRM.Core.ServiceLayer.Bootstrapper
{
    public interface IStartManager
    {
        Task StartAsync();
        void Start();
    }
}
