namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public class DistributedCacheDynamoDbStartUpSettings
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
    }
}