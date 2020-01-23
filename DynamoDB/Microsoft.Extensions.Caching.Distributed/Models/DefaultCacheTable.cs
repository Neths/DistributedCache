using Amazon.DynamoDBv2.DataModel;

namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Models
{
    /// <summary>
    /// 
    /// </summary>
    [ToString]
    public class DefaultCacheTable : ICacheTable
    {
        /// <summary>
        /// 
        /// </summary>
        [DynamoDBHashKey]
        public string CacheId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long Ttl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CacheOptions CacheOptions { get; set; }
    }
}