namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICacheTable
    { 
        /// <summary>
        /// 
        /// </summary>
        string CacheId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        byte[] Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        long Ttl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        CacheOptions CacheOptions { get; set; }
    }
}