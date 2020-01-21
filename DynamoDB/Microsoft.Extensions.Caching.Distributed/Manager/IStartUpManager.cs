namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Manager
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStartUpManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        void Run(string tableName);
    }
}
