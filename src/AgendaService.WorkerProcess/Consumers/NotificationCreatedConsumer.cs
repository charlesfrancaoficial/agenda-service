using CSF.AssyncMessaging.Domain;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AgendaService.WorkerProcess.Consumers
{
    public class NotificationCreatedConsumer : IConsumer<INotificationCreated>
    {
        public async Task Consume(ConsumeContext<INotificationCreated> context)
        {
            var serializedMessage = JsonSerializer.Serialize(context.Message, new JsonSerializerOptions { });

            Console.WriteLine($"NotificationCreated event consumed. Message: {serializedMessage}");
        }
    }
}
