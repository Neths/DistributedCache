using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Constants;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Manager;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Models;

namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DistributedCacheService<T> : IDistributedCache where T : ICacheTable
    {   
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly ICacheTtlManager _cacheTtlManager;
        private readonly Encoding _encoding;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dynamoDbContext"></param>
        /// <param name="cacheTtlManager"></param>
        /// <param name="startUpManager"></param>
        /// <param name="encoding"></param>
        public DistributedCacheService(IDynamoDBContext dynamoDbContext, ICacheTtlManager cacheTtlManager, IStartUpManager startUpManager, Encoding encoding)
        {
            _dynamoDbContext = dynamoDbContext;

            _cacheTtlManager = cacheTtlManager;

            _encoding = encoding;
            
            startUpManager.Run(typeof(T).Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] Get(string key)
        {
            var cacheItem = _dynamoDbContext.LoadAsync<T>(key).GetAwaiter().GetResult();

            if (cacheItem == null)
                return null;

            if (cacheItem.Ttl >= _cacheTtlManager.ToUnixTime(DateTime.UtcNow))
            {
                return _encoding.GetBytes(_dynamoDbContext.LoadAsync<T>(key).GetAwaiter().GetResult().Value);
            }
                
            Remove(cacheItem.CacheId);
            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<byte[]> GetAsync(string key, 
            CancellationToken token = new CancellationToken())
        {
            if (token.IsCancellationRequested)
            {
                return Task.FromCanceled<byte[]>(token);
            }

            try
            {
                return Task.FromResult(Get(key));
            }
            catch (Exception exception)
            {
                return Task.FromException<byte[]>(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public void Set(string key, 
            byte[] value, 
            DistributedCacheEntryOptions options)
        {
            var cacheItem = Activator.CreateInstance<T>();

            cacheItem.CacheId = key;
            cacheItem.Value = _encoding.GetString(value);
            cacheItem.CacheOptions = _cacheTtlManager.ToCacheOptions(options); 
            cacheItem.Ttl = _cacheTtlManager.ToTtl(cacheItem.CacheOptions);

            _dynamoDbContext.SaveAsync(cacheItem).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task SetAsync(string key, 
            byte[] value, 
            DistributedCacheEntryOptions options,
            CancellationToken token = new CancellationToken())
        {
            if (token.IsCancellationRequested)
            {
                return Task.FromCanceled<byte[]>(token);
            }

            try
            {
                Set(key, value, options);

                return Task.FromResult(0);
            }
            catch (Exception exception)
            {
                return Task.FromException<byte[]>(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Refresh(string key)
        {
            var cacheItem = _dynamoDbContext.LoadAsync<T>(key).GetAwaiter().GetResult();

            if (cacheItem != null)
            {
                if (cacheItem.CacheOptions.Type == CacheExpiryType.Sliding)
                {
                    cacheItem.Ttl = _cacheTtlManager.ToTtl(cacheItem.CacheOptions);
                }

                _dynamoDbContext.SaveAsync(cacheItem).GetAwaiter().GetResult();
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task RefreshAsync(string key, 
            CancellationToken token = new CancellationToken())
        {
            if (token.IsCancellationRequested)
            {
                return Task.FromCanceled<byte[]>(token);
            }

            try
            {
                Refresh(key);

                return Task.FromResult(0);
            }
            catch (Exception exception)
            {
                return Task.FromException<byte[]>(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _dynamoDbContext.DeleteAsync<T>(key).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task RemoveAsync(string key, 
            CancellationToken token = new CancellationToken())
        {
            if (token.IsCancellationRequested)
            {
                return Task.FromCanceled<byte[]>(token);
            }

            try
            {
                Remove(key);

                return Task.FromResult(0);
            }
            catch (Exception exception)
            {
                return Task.FromException<byte[]>(exception);
            }
        }
    }
}
