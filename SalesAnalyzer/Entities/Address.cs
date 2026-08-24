using CSVSalesPro.Entities.Exceptions;

namespace CSVSalesPro.Entities
{
    public class Address
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CEP { get; set; }

        public Address() { }

        public Address(string street, int number, string neighborhood, string city, string state, string cep)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                throw new AddressException("Street is required!");
            }
            if (string.IsNullOrWhiteSpace(neighborhood))
            {
                throw new AddressException("Neighborhood is required!");
            }
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new AddressException("City is required!");
            }
            if (string.IsNullOrWhiteSpace(state))
            {
                throw new AddressException("State is required!");
            }
            if (string.IsNullOrWhiteSpace(cep))
            {
                throw new AddressException("CEP is required!");
            }
            if(cep.Length != 8)
            {
                throw new AddressException("The postal code must have 8 digits!");
            }
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            CEP = cep;
        }

        public override string ToString()
        {
            return $"ADDRESS\n\nStreet: {Street}\nNumber: {Number}\nNeighborhood: {Neighborhood}\nCity: {City}\nState: {State}\nCEP: {CEP}";
        }
    }
}
