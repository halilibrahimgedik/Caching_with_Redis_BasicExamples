var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//options.Configuration = burada hangi redis sunucusuna bağlanıcaksak onu yazacağız
// redis'i dockerize ettiğimizde default redis portu olan 6379'u biz loalhost'da 1453 portuna atamıştık.
builder.Services.AddStackExchangeRedisCache(options => options.Configuration = "localhost:1453");



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
