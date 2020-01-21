namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDynamoDbService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="readCapacityUnits"></param>
        /// <param name="writeCapacityUnits"></param>
        /// <returns></returns>
        bool CreateDb(string databaseName, int readCapacityUnits, int writeCapacityUnits);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        bool DeleteDb(string databaseName);
    }
}