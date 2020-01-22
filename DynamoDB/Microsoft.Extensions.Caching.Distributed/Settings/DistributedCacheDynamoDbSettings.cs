using System.Text;
using Amazon;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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

    /// <summary>
    /// 
    /// </summary>
    public class DistributedCacheDynamoDbSettings : IDistributedCacheDynamoDbSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string TablePrefix { get; set; }

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
        public IDistributedCacheDynamoDbStartUpSettings StartUpSettings { get; set; }

        /// <summary>
        /// Settings for Dynamo db cache provider
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <param name="distributedCacheDynamoDbStartUpSettings"></param>
        public DistributedCacheDynamoDbSettings(IConfiguration configuration, 
            ILogger<DistributedCacheDynamoDbSettings> logger,
            IDistributedCacheDynamoDbStartUpSettings distributedCacheDynamoDbStartUpSettings)
        {
            Encoding = Encoding.UTF8;
            DefaultTtl = CacheTableAttributes.Ttl;

            var section = configuration.GetSection("Aws.DynamoDb:DistributedCaching");
            if (section.Exists())
            {
                if (string.IsNullOrEmpty(section["region"]))
                {
                    RegionEndpoint = RegionEndpoint.GetBySystemName(section["region"]);
                    logger.LogDebug($"dynamoDb region are overidded by {section["region"]}");
                }

                if (string.IsNullOrEmpty(section["encoding"]))
                {
                    Encoding = Encoding.GetEncoding(section["encoding"]);
                    logger.LogDebug($"encoding are overidded by {section["encoding"]}");
                }

                if (string.IsNullOrEmpty(section["ttl"]))
                {
                    DefaultTtl = long.Parse(section["ttl"]);
                    logger.LogDebug($"DefaultTtl are overidded by {section["ttl"]}");
                }
            }

            StartUpSettings = distributedCacheDynamoDbStartUpSettings;
        }

    }
}
