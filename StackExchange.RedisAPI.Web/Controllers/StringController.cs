using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using StackExchange.RedisAPI.Web.Services;
using System.Text;

namespace StackExchange.RedisAPI.Web.Controllers
{
    public class StringController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;

        public StringController(RedisService redisService)
        {
            _redisService = redisService;
            _db = _redisService.GetDatabase(0); 
        }


        public IActionResult Index()
        {
            //redis sunucusunda 0-15'e kadar 16 tane veri tabanı vardır.
            _db.StringSet("name", "halil ibrahim gedik");
            _db.StringSet("occupation", "computer engineer");
            _db.StringSet("viewer", "0");

            var directory = Directory.GetCurrentDirectory();
            string imagePath = directory+"/wwwroot/images/redis.jpg";

            byte[] imageByte = System.IO.File.ReadAllBytes(imagePath);
            _db.StringSet("redisImage",imageByte);

            return View();
        }

        public IActionResult Show()
        {
            ViewBag.Name = _db.StringGet("name").ToString() ?? RedisValue.Null;
            ViewBag.Occupation = _db.StringGet("occupation").ToString() ?? RedisValue.Null;
            ViewBag.Viewer = _db.StringIncrement("viewer", 1);

            byte[]? imageBytes = _db.StringGet("redisImage");

            if (imageBytes != null && imageBytes.Length > 0)
            {
                string base64String = Convert.ToBase64String(imageBytes);
                string imgSrc = $"data:image/jpeg;base64,{base64String}";
                ViewData["ImageSrc"] = imgSrc ?? null;
            }

            return View();  
        }
    }
}
