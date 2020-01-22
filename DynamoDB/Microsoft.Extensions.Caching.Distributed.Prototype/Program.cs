using System;
using System.IO;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Prototype.Repository;
using Microsoft.Extensions.Caching.Distributed.DynamoDb.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Caching.Distributed.DynamoDb.Prototype
{
    class Program
    {
        #region Private variables

        private static IServiceProvider _serviceProvider;

        #endregion

        static void Main(string[] args)
        {
            //Run startup.
            StartUp();

            //Sample data to be stored in cache.
            var someObject = new SampleDataModel
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Tom",
                LastName = "Hardy",
                CompanyName = "Microsoft corporation"
            };

            //Get instance of sample repository from IOC
            var repositoryInstance = _serviceProvider.GetService<ISampleRepository>();

            //Call save method. (Saves to cache)
            repositoryInstance.Save(someObject);

            //Call get method. (Gets from cache)
            var result = repositoryInstance.Get(someObject.Id);

            Console.WriteLine($"{result.FirstName}, {result.LastName}");

            Console.ReadLine();
        }

        #region Set up IOC

        static void StartUp()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            //Set up the IOC
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IDistributedCacheDynamoDbSettings, DistributedCacheDynamoDbSettings>();

            //Register caching
            serviceCollection.RegisterDynamoDbCacheService<CustomCacheTable>();

            //Register repository
            serviceCollection.AddScoped<IAmazonDynamoDB, AmazonDynamoDBClient>();
            serviceCollection.AddTransient<ISampleRepository, SampleRepository>();

            //Initialize the IOC
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        #endregion
    }
}
