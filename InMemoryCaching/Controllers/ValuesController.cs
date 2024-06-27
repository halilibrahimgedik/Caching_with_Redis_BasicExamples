using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("setname")]
        public void SetName(string name)
        {
            _memoryCache.Set("name", name);
        }

        [HttpGet]
        public string GetName()
        {
            //var name = _memoryCache.Get<string>("name");
            //return name.Substring(2);

            // eğer yukarıdaki gibi, bir veri eklemeden name adlı key'e karşılık veri'yi okumaya çalışırsak,runtime'da Object referance null hatası alacağız
            // böyle bir durumun oluşmaması TryGetValue ile güvenli bir erişim yapabiliriz.

            if (_memoryCache.TryGetValue<string>("name", out var name))
            {
                return name;
            }

            return "there is no any data";
        }

        [HttpGet("setdate")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(45), // ne olursa olsun 45 saniye sonra cache'den veri kaldırılacak
                SlidingExpiration = TimeSpan.FromSeconds(10) // 10 saniyede bir cache'den veriye erişmezsen veri cache'den Kaldırılacak. Absolute time biterse'de kaldırılacak
            });
        }

        [HttpGet("getdate")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }
}
