using System;
using MongoDB.Driver;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Demo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var client = new MongoClient();
                IMongoDatabase database = client.GetDatabase(
                    "db_" + Guid.NewGuid().ToString("N"));

                IMongoCollection<City> collection
                    = database.GetCollection<City>("col");
                collection.InsertMany(new[]
                {
                    new City(
                        1, 
                        "Amsterdam", 
                        "nl", 
                        true, 
                        new DateTime(2019,6,1,1,1,1,DateTimeKind.Utc)),
                    new City(
                        2, 
                        "Berlin", 
                        "de", 
                        true,
                        new DateTime(2019,7,10,1,1,1,DateTimeKind.Utc)),
                    new City(
                        3, 
                        "Paris", 
                        "fr", 
                        true,
                        new DateTime(2019,6,18,1,1,1,DateTimeKind.Utc)),
                    new City(
                        4, 
                        "Zürich", 
                        "ch", 
                        false,
                        new DateTime(2019,3,15,1,1,1,DateTimeKind.Utc))
                });

                return collection;
            });

            services.AddGraphQL(s =>
            {
                ISchemaBuilder builder = SchemaBuilder.New()
                    .AddType<CityType>()
                    .AddQueryType<QueryType>()
                    .AddServices(s);

                return builder.Create();
            }, QueryOptions);

            services.AddDiagnosticObserver<HotChocolateLogger>();
            services.AddLogging();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseGraphQL();

        }

        protected QueryExecutionOptions QueryOptions =>
            new QueryExecutionOptions
            {
                TracingPreference = TracingPreference.Always
            };
    }
}
