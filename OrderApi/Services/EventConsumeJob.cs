using Confluent.Kafka;

namespace OrderApi.Services
{
    public class EventConsumeJob : BackgroundService
    {
        private readonly ILogger<EventConsumeJob> logger;
        private readonly IConfiguration configuration;
        public EventConsumeJob(ILogger<EventConsumeJob> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                GroupId = configuration["Kafka:GroupId"],
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(configuration["Kafka:Topic"]);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(stoppingToken);
                    logger.LogInformation($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'");
                    //var order = JsonConvert.DeserializeObject<Order>(consumeResult.Message.Value);
                    //await orderService.Create(order);
                }
                catch (OperationCanceledException ex)
                {

                    throw;
                }
            }

        }
    }
}
