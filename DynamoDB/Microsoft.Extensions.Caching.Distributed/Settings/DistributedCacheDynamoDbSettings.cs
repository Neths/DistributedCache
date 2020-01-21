using System.Text;
using Amazon;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Constants;

namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public class DistributedCacheDynamoDbSettings
    {
        /// <summary>
        /// AWS Region
        /// </summary>
        public RegionEndpoint RegionEndpoint { get; set; }

        /// <summary>
        /// Default time to live for cache items
        /// </summary>
        public long DefaultTtl { get; set; }

        /// <summary>
        /// Encoding of data
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Start up settings
        /// </summary>
        public DistributedCacheDynamoDbStartUpSettings StartUpSettings { get; set; }

        /// <summary>
        /// Settings for Dynamo db cache provider
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="regionEndpoint"></param>
        public DistributedCacheDynamoDbSettings(Encoding encoding, RegionEndpoint regionEndpoint)
        {
            RegionEndpoint = regionEndpoint;

            Encoding = encoding;

            DefaultTtl = CacheTableAttributes.Ttl;

            StartUpSettings = new DistributedCacheDynamoDbStartUpSettings
            {
                ReadCapacityUnits = CacheTableAttributes.ReadCapacityUnits,
                WriteCapacityUnits = CacheTableAttributes.WriteCapacityUnits,
                CreateDbOnStartUp = CacheTableAttributes.CreateTableOnStartUp
            };
        }

    }
}
