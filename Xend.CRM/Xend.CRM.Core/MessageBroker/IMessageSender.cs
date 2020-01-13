using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xend.CRM.Core.MessageBroker
{
    public interface IMessageSender
    {
        Task SendEvent<T>(T payload) where T : class;
        Task SendMessage<T>(string endPoint, T payload) where T : class;
    }
}
