var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//options.Configuration = burada hangi redis sunucusuna ba�lan�caksak onu yazaca��z
// redis'i dockerize etti�imizde default redis portu olan 6379'u biz loalhost'da 1453 portuna atam��t�k.
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
