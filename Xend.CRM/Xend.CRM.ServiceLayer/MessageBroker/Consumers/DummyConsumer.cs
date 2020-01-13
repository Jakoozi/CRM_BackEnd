using AutoMapper;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.ServiceLayer.MessageBroker.Messages;

namespace Xend.CRM.ServiceLayer.MessageBroker.DummyConsumer
{

    public class DummyConsumer : IConsumer<DummyMessage>
    {
        IMapper Mapper { get; }
        public DummyConsumer(IMapper mapper)
        {
            Mapper = mapper;
        }

        public async Task Consume(ConsumeContext<DummyMessage> context)
        {


        }
    }

}
