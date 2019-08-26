using HotChocolate.Types;
using MongoDB.Driver;

namespace Demo
{
    public class QueryType: ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("Query");

            descriptor.Field("blackSabbath2019Tour")
                .Resolver(ctx => ctx.Service<IMongoCollection<City>>().AsQueryable())
                .UseFiltering<CityFilterType>();
        }
    }
}
