using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using MCMicroservice.MyModels;
using MCMicroservice.Repository;
using MCMicroservice.Repository.Models;
using System.Text.Json;
using System.Text;

namespace MCMicroservice
{

    public class MessageConsumerService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageConsumerService(IConfiguration configuration)
        {
            // ... RabbitMQ connection setup ...
            var factory = new ConnectionFactory()
            {
/*                HostName = configuration["RabbitMQ:HostName"],
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"]*/
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "energy_data", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                ProcessMessage(message);
            };

            _channel.BasicConsume(queue: "energy_data", autoAck: true, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void ProcessMessage(string message)
        {
            // ... Message processing logic ...

            Console.WriteLine($"Received raw message: {message}");

            // Deserialize the JSON message
            var measurement = JsonSerializer.Deserialize<MeasurementModel>(message);
            //print the content of the message
            Console.WriteLine($"Received message: {measurement.MeasurementValue} from device {measurement.DeviceId} and timestamp {measurement.Timestamp}");

            // Save the measurement to the database
            using (var context = new DS_MeasurementDbContext())
            {
                context.Measurements.Add(new Measurement
                {
                    DeviceId = int.Parse(measurement.DeviceId),
                    MeasurementValue = measurement.MeasurementValue,
                    Timestamp = measurement.Timestamp
                });
                context.SaveChanges();
            }
        }
    }
}
