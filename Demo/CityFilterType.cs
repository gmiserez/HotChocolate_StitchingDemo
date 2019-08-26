using HotChocolate.Types.Filters;

namespace Demo
{
    public class CityFilterType : FilterInputType<City>
    {
        protected override void Configure(
            IFilterInputTypeDescriptor<City> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Filter(c => c.ConcertDate);
        }
    }
}
