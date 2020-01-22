using Microsoft.Extensions.Caching.Distributed.DynamoDb.Service;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Settings;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Manager
{
    /// <summary>
    /// 
    /// </summary>
    public class StartUpManager: IStartUpManager
    {
        private readonly IDynamoDbService _dynamoDb;
        private readonly ILogger<StartUpManager> _logger;
        private readonly IDistributedCacheDynamoDbSettings _distributedCacheDynamoDbSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dynamoDb"></param>
        /// <param name="logger"></param>
        /// <param name="distributedCacheDynamoDbSettings"></param>
        public StartUpManager(IDynamoDbService dynamoDb, 
            ILogger<StartUpManager> logger,
            IDistributedCacheDynamoDbSettings distributedCacheDynamoDbSettings)
        {
            _dynamoDb = dynamoDb;
            _logger = logger;
            _distributedCacheDynamoDbSettings = distributedCacheDynamoDbSettings;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        public void Run(string tableName)
        {
            if(_distributedCacheDynamoDbSettings.StartUpSettings != null && _distributedCacheDynamoDbSettings.StartUpSettings.CreateDbOnStartUp)
            {
                _logger.LogDebug($"StartUpManager - Run - create cache table with name {tableName}");
                _dynamoDb.CreateDb(tableName, _distributedCacheDynamoDbSettings.StartUpSettings.ReadCapacityUnits, _distributedCacheDynamoDbSettings.StartUpSettings.WriteCapacityUnits);
            }
        }
    }
}
