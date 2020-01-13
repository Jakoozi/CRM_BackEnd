using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.Core.MessageBroker;

namespace Xend.CRM.ServiceLayer.MessageBroker
{
    public class MessageSender : IMessageSender
    {
        IBus Bus { get; }
        public MessageSender(IBus bus)
        {
            Bus = bus;
        }

        public async Task SendMessage<T>(string endPoint, T payload)
            where T : class
        {
            var endpoint = await Bus.GetSendEndpoint(new Uri(endPoint));
            await endpoint.Send(payload);
        }

        public async Task SendEvent<T>(T payload)
             where T : class

        {
            await Bus.Publish<T>(payload);
        }

    }
}
