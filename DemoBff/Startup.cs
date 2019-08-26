using System;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Subscriptions;
using HotChocolate.Stitching;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Demo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("foo",
                (ctx, cfg) =>
                {
                    cfg.BaseAddress = new Uri("http://localhost:47939/");
                });

            services.AddGraphQLSubscriptions();
            services.AddStitchedSchema(builder => builder.AddSchemaFromHttp("foo"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseGraphQL();
        }
    }
}
