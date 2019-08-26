using System;

namespace Demo
{
    public class City
    {
        public City(
            int id, 
            string name, 
            string countryCode, 
            bool isCapitalCity,
            DateTime concertDate)
        {
            Id = id;
            Name = name;
            CountryCode = countryCode;
            IsCapitalCity = isCapitalCity;
            ConcertDate = concertDate;
        }

        public int Id { get; }
        public string Name { get; }
        public string CountryCode { get; }
        public bool IsCapitalCity { get; }
        public DateTime ConcertDate { get; }
    }
}
