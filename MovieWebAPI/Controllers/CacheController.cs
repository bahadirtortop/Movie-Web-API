using System;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MovieWebAPI.Infrastructure.Caching;

namespace MovieWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CacheController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        public CacheController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        [HttpGet("Clear")]
        public void Clear()
        {
            Type type = typeof(CacheKeys);
            foreach (FieldInfo property in type.GetFields())
            {
                var v = property.GetValue(null);
                _memoryCache.Remove(v);
            }
        }
    }
}
