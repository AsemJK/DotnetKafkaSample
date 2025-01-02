using Confluent.Kafka;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> logger;

        public ProductService(ILogger<ProductService> logger)
        {
            this.logger = logger;
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
            var deliveryResult = await producer.ProduceAsync("test-topic", new Message<Null, string> { Value = "test" });
            logger.LogInformation($"Delivered '{deliveryResult.Value}' to '{deliveryResult.TopicPartitionOffset}'");

            await Task.CompletedTask;
        }
    }

    public interface IProductService
    {
        Task Test();
    }
}
