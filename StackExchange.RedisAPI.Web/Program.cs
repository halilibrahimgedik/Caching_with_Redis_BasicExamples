using StackExchange.RedisAPI.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();




builder.Services.AddSingleton<RedisService>();




var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();




using (var serviceScope = app.Services.CreateScope())
{

    var services = serviceScope.ServiceProvider;

    var redisService = services.GetRequiredService<RedisService>();

    redisService.Connect();

}



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
