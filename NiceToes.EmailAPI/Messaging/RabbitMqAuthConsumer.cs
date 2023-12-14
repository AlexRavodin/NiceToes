using System.Text;
using Newtonsoft.Json;
using NiceToes.EmailAPI.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NiceToes.EmailAPI.Messaging
{
    public class RabbitMqAuthConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;
        private IConnection _connection;
        private IModel _channel;
        public RabbitMqAuthConsumer(IConfiguration configuration, EmailService emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest",
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue")
                , false, false, false, null);

        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                String email = JsonConvert.DeserializeObject<string>(content);
                HandleMessage(email).GetAwaiter().GetResult();

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue"), false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(string email)
        {
            _emailService.RegisterUserEmailAndLog(email).GetAwaiter().GetResult();
        }
    }
}
