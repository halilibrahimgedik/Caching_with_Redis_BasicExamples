using StackExchange.Redis;

namespace StackExchange.RedisAPI.Web.Services
{
    public class RedisService
    {
        private readonly string _redisHost;
        private readonly string _redisPort;
        private ConnectionMultiplexer _multiplexer;
        private IDatabase Db { get; set; }

        public RedisService(IConfiguration configuration)
        {
            _redisHost = configuration["Redis:Host"];
            _redisPort = configuration["Redis:Port"];
        }


        // Redis server ile haberleşeceğimiz metot
        public void Connect()
        {
            var configString = $"{_redisHost}:{_redisPort}";

            _multiplexer = ConnectionMultiplexer.Connect(configString);
        }

        public IDatabase GetDatabase(int db)
        {
            return _multiplexer.GetDatabase(db);
        }

    }
}
