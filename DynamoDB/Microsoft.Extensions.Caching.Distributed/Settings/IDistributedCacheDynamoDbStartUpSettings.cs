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
}