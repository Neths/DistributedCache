using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Manager;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Models;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Service;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.Caching.Distributed.DynamoDb
{
    /// <summary>
    /// 
    /// </summary>
    public static class Registration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterDynamoDbCacheService(this IServiceCollection services)
        {
            RegisterDynamoDbCacheService<DefaultCacheTable>(services, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="T"></typeparam>
        public static void RegisterDynamoDbCacheService<T>(this IServiceCollection services) where T : ICacheTable
        {
            RegisterDynamoDbCacheService<T>(services, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="lifeTime"></param>
        public static void RegisterDynamoDbCacheService(this IServiceCollection services, ServiceLifetime lifeTime)
        {
            RegisterDynamoDbCacheService<DefaultCacheTable>(services, lifeTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="lifeTime"></param>
        /// <typeparam name="T"></typeparam>
        public static void RegisterDynamoDbCacheService<T>(this IServiceCollection services, ServiceLifetime lifeTime) where T : ICacheTable
        {
            services.Add(new ServiceDescriptor(typeof(ICacheTtlManager), (c) => new CacheTtlManager(c.GetService<IDistributedCacheDynamoDbSettings>()), lifeTime));

            services.Add(new ServiceDescriptor(typeof(IDynamoDBContext), (c) => new DynamoDBContext(c.GetService<IAmazonDynamoDB>()), lifeTime));

            services.Add(new ServiceDescriptor(typeof(IDynamoDbService), (c) => new DynamoDbService(c.GetService<IAmazonDynamoDB>()), lifeTime));

            services.Add(new ServiceDescriptor(typeof(IStartUpManager), (c) => new StartUpManager(c.GetService<IDynamoDbService>(),
                c.GetService<ILoggerFactory>().CreateLogger<StartUpManager>(),
                c.GetService<IDistributedCacheDynamoDbSettings>())
                , lifeTime));

            services.Add(new ServiceDescriptor(typeof(IDistributedCache),
                (c) => new DistributedCacheService<T>(c.GetService<IDynamoDBContext>(),
                    c.GetService<ICacheTtlManager>(),
                    c.GetService<IStartUpManager>(),
                    c.GetService<IDistributedCacheDynamoDbSettings>(),
                    c.GetService<ILoggerFactory>().CreateLogger<DistributedCacheService<T>>())
                , lifeTime));
        }
    }
}
