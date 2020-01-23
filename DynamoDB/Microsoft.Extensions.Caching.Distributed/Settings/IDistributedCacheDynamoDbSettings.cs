using System.Text;
using Amazon;

namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDistributedCacheDynamoDbSettings
    {
        /// <summary>
        /// 
        /// </summary>
        string TablePrefix { get; }
        /// <summary>
        /// 
        /// </summary>
        RegionEndpoint RegionEndpoint { get; }
        /// <summary>
        /// 
        /// </summary>
        long DefaultTtl { get; }
        /// <summary>
        /// 
        /// </summary>
        Encoding Encoding { get; }
        /// <summary>
        /// 
        /// </summary>
        IDistributedCacheDynamoDbStartUpSettings StartUpSettings { get; }
    }
}