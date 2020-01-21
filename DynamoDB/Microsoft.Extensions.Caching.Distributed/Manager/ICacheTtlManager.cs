using System;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Models;

namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Manager
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICacheTtlManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        long ToUnixTime(DateTime date);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        CacheOptions ToCacheOptions(DistributedCacheEntryOptions options);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheOptions"></param>
        /// <returns></returns>
        long ToTtl(CacheOptions cacheOptions);
    }
}
