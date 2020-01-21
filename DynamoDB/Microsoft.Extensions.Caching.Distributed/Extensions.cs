using Newtonsoft.Json;

namespace Microsoft.Extensions.Caching.Distributed.DynamoDb
{
    /// <summary>
    /// s
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetObject<T>(this IDistributedCache cache, string key)
        {
            var result = cache.GetString(key);

            return result != null ? JsonConvert.DeserializeObject<T>(result) : default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public static void SetObject<T>(this IDistributedCache cache, string key, T value)
        {
            cache.SetString(key, JsonConvert.SerializeObject(value));
        }

    }
}
