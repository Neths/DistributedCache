using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Manager;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Models;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Service;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Settings;
using Microsoft.Extensions.DependencyInjection;

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
        /// <param name="settings"></param>
        public static void RegisterDynamoDbCacheService(this IServiceCollection services, DistributedCacheDynamoDbSettings settings)
        {
            RegisterDynamoDbCacheService<DefaultCacheTable>(services, settings, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <typeparam name="T"></typeparam>
        public static void RegisterDynamoDbCacheService<T>(this IServiceCollection services, DistributedCacheDynamoDbSettings settings) where T : ICacheTable
        {
            RegisterDynamoDbCacheService<T>(services, settings, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <param name="lifeTime"></param>
        public static void RegisterDynamoDbCacheService(this IServiceCollection services, DistributedCacheDynamoDbSettings settings, ServiceLifetime lifeTime)
        {
            RegisterDynamoDbCacheService<DefaultCacheTable>(services, settings, lifeTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <param name="lifeTime"></param>
        /// <typeparam name="T"></typeparam>
        public static void RegisterDynamoDbCacheService<T>(this IServiceCollection services, DistributedCacheDynamoDbSettings settings, ServiceLifetime lifeTime) where T : ICacheTable
        {
            services.Add(new ServiceDescriptor(typeof(ICacheTtlManager), (c) => new CacheTtlManager(settings.DefaultTtl), lifeTime));

            services.Add(new ServiceDescriptor(typeof(IDynamoDBContext), (c) => new DynamoDBContext(c.GetService<IAmazonDynamoDB>()), lifeTime));

            services.Add(new ServiceDescriptor(typeof(IDynamoDbService), typeof(DynamoDbService), lifeTime));

            services.Add(new ServiceDescriptor(typeof(IStartUpManager), (c) => new StartUpManager(c.GetService<IDynamoDbService>(), settings.StartUpSettings), lifeTime));

            services.Add(new ServiceDescriptor(typeof(IDistributedCache),
                (c) => new DistributedCacheService<T>(c.GetService<IDynamoDBContext>(), c.GetService<ICacheTtlManager>(), c.GetService<IStartUpManager>(), settings.Encoding), lifeTime));
        }
    }
}
