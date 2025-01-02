using Confluent.Kafka;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> logger;
        private readonly IConfiguration configuration;

        public ProductService(ILogger<ProductService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
            // SD sd = new SD(); // Error: The type or namespace name 'SD' could not be found (are you missing a using directive or an assembly reference?)
        }
        public async Task Test()
        {
            //produce call using kafka
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                AllowAutoCreateTopics = true,
                Acks = Acks.All
            };
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var deliveryResult = await producer.ProduceAsync(configuration["Kafka:Topic"], new Message<Null, string> { Value = "test" });
            logger.LogInformation($"Delivered {DateTime.Now} '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");

            await Task.CompletedTask;
            producer.Flush(TimeSpan.FromSeconds(10));
            producer.Dispose();
        }
    }

    public interface IProductService
    {
        Task Test();
    }
}
