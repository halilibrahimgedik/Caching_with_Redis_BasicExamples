using StackExchange.Redis;

// ConnectionMultiplexer sınıfı üzerinden Redis sunucusuna bir bağlantı oluşturuyoruz.
ConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

// bu bağlantı üzerinden bir Subscriber oluşturuyoruz.
ISubscriber subscriber = connection.GetSubscriber();

// Bu aşamadan sonra Publisher ve Consumer olmak üere ikiye ayrılacaktır.

while (true)
{
    Console.Write("Mesaj : ");
    string message = Console.ReadLine();
    await subscriber.PublishAsync("mychannel", message);
}