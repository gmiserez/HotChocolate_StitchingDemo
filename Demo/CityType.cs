using HotChocolate.Types;

namespace Demo
{
    public class CityType: ObjectType<City>
    {
        protected override void Configure(IObjectTypeDescriptor<City> descriptor)
        {
            descriptor.Field(c => c.ConcertDate).Type<NonNullType<DateType>>();
        }
    }
}
