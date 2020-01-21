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
        public string CacheId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

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