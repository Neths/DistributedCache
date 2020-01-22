using System;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Constants;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Models;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Settings;

namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Manager
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheTtlManager : ICacheTtlManager
    {
        private readonly IDistributedCacheDynamoDbSettings _settings;

        /// <summary>
        /// 
        /// </summary>
        public CacheTtlManager(IDistributedCacheDynamoDbSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public CacheOptions ToCacheOptions(DistributedCacheEntryOptions options)
        {
            if (options.AbsoluteExpiration != null)
            {
                return new CacheOptions
                {
                    Type = CacheExpiryType.Absolute,
                    Span = (long)(options.AbsoluteExpiration.Value.ToUniversalTime().DateTime - DateTimeOffset.UtcNow.DateTime).TotalMinutes
                };
            }

            if (options.AbsoluteExpirationRelativeToNow != null)
            {
                return new CacheOptions
                {
                    Type = CacheExpiryType.Absolute,
                    Span = (long)options.AbsoluteExpirationRelativeToNow.Value.TotalMinutes
                };
            }

            if (options.SlidingExpiration != null)
            {
                return new CacheOptions
                {
                    Type = CacheExpiryType.Sliding,
                    Span = (long)options.SlidingExpiration.Value.TotalMinutes
                };
            }

            return new CacheOptions
            {
                Type = CacheExpiryType.Absolute,
                Span = (long)(DateTimeOffset.UtcNow.AddMinutes(_settings.DefaultTtl).DateTime - DateTimeOffset.UtcNow.DateTime).TotalMinutes
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheOptions"></param>
        /// <returns></returns>
        public long ToTtl(CacheOptions cacheOptions)
        {
            return ToUnixTime(DateTime.UtcNow.AddMinutes(cacheOptions.Span));
        }
    }
}