using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace AgendaService.WorkerProcess
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly int _intervaloMensagemWorkerAtivo;

        public Worker(
            ILogger<Worker> logger,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _intervaloMensagemWorkerAtivo = Convert.ToInt32(configuration["IntervaloMensagemWorkerAtivo"]);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Aguardando mensagens...");

            var factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var queuename = "agenda_comunication";

            channel.QueueDeclare(
                queue: queuename,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(
                queue: queuename,
                autoAck: true,
                consumer: consumer
            );

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(
                    $"Worker ativo em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await Task.Delay(_intervaloMensagemWorkerAtivo, stoppingToken);
            }
        }

        private void Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            _logger.LogInformation(
                $"[Nova mensagem | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                Encoding.UTF8.GetString(e.Body.ToArray()));
        }
    }
}