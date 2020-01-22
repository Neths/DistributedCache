using Microsoft.Extensions.Caching.Distributed.DynamoDb.Constants;

namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDistributedCacheDynamoDbStartUpSettings
    {
        /// <summary>
        /// Create dynamodb on startup
        /// </summary>
        bool CreateDbOnStartUp { get; }

        /// <summary>
        /// Read capacity limit (Parallel)
        /// </summary>
        int ReadCapacityUnits { get; }

        /// <summary>
        /// Write capacity limit (Parallel)
        /// </summary>
        int WriteCapacityUnits { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DistributedCacheDynamoDbStartUpSettings : IDistributedCacheDynamoDbStartUpSettings
    {
        /// <summary>
        /// Create dynamodb on startup
        /// </summary>
        public bool CreateDbOnStartUp { get; set; }

        /// <summary>
        /// Read capacity limit (Parallel)
        /// </summary>
        public int ReadCapacityUnits { get; set; }

        /// <summary>
        /// Write capacity limit (Parallel)
        /// </summary>
        public int WriteCapacityUnits { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static DistributedCacheDynamoDbStartUpSettings Default = new DistributedCacheDynamoDbStartUpSettings
        {
            ReadCapacityUnits = CacheTableAttributes.ReadCapacityUnits,
            WriteCapacityUnits = CacheTableAttributes.WriteCapacityUnits,
            CreateDbOnStartUp = CacheTableAttributes.CreateTableOnStartUp
        };
    }
}