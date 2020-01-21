namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Models
{
    /// <summary>
    /// 
    /// </summary>
    [ToString]
    public class CacheOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long Span { get; set; }
    }
}