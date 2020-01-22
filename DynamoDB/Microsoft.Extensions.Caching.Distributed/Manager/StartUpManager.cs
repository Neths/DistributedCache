using Microsoft.Extensions.Caching.Distributed.DynamoDb.Service;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Settings;

namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Manager
{
    /// <summary>
    /// 
    /// </summary>
    public class StartUpManager: IStartUpManager
    {
        private readonly IDynamoDbService _dynamoDb;
        private readonly IDistributedCacheDynamoDbSettings _distributedCacheDynamoDbSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dynamoDb"></param>
        /// <param name="distributedCacheDynamoDbSettings"></param>
        public StartUpManager(IDynamoDbService dynamoDb, IDistributedCacheDynamoDbSettings distributedCacheDynamoDbSettings)
        {
            _dynamoDb = dynamoDb;
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
                _dynamoDb.CreateDb(tableName, _distributedCacheDynamoDbSettings.StartUpSettings.ReadCapacityUnits, _distributedCacheDynamoDbSettings.StartUpSettings.WriteCapacityUnits);
            }
        }
    }
}
