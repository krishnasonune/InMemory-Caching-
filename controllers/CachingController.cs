using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace controllers.CachingController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CachingController : ControllerBase
    {
        public ICacheService _cache;

        public CachingController(ICacheService cacheService)
        {
           _cache = cacheService;
        }

        [HttpGet]
        [Route("students")]
        public IActionResult Get()
        {
            var data = _cache.Get<Students[]>("students");
            if (data == null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "mockdata.json");
                var fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                var sr = new StreamReader(fs);
                var json = sr.ReadToEnd();
                Students[] students = JsonSerializer.Deserialize<Students[]>(json);
                _cache.SetData<Students[]>("students", students, DateTime.UtcNow.AddMinutes(15));
                return Ok(students);
            }
            return Ok(data);
        }
    }
}
