using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Services.Radis
{
    public class CacheOptions
    {
        public TimeSpan? AbsoluteExpiration { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public DistributedCacheEntryOptions ToDistributedCacheEntryOptions()
        {
            var options = new DistributedCacheEntryOptions();

            if (AbsoluteExpiration.HasValue)
                options.SetAbsoluteExpiration(AbsoluteExpiration.Value);

            if (SlidingExpiration.HasValue)
                options.SetSlidingExpiration(SlidingExpiration.Value);

            return options;
        }
    }

    public static class CacheProfiles
    {
        public static readonly CacheOptions ShortLived = new()
        {
            SlidingExpiration = TimeSpan.FromMinutes(5),
            AbsoluteExpiration = TimeSpan.FromMinutes(15)
        };

        public static readonly CacheOptions LongLived = new()
        {
            SlidingExpiration = TimeSpan.FromHours(1),
            AbsoluteExpiration = TimeSpan.FromDays(1)
        };

        public static readonly CacheOptions NeverExpires = new()
        {
            // Только абсолютное expiration на очень долгий срок
            AbsoluteExpiration = TimeSpan.FromDays(365)
        };
    }
}
