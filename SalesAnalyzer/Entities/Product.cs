using System.Globalization;
using CSVSalesPro.Entities.Enums;
using CSVSalesPro.Entities.Exceptions;

namespace CSVSalesPro.Entities
{
    public abstract class Product
    {
        private static int NextId = 1;
        public int Id { get; private set; }
        public string Name { get; set; }
        public double BasePrice { get; set; }
        public double CommissionRate { get; protected set; }
        public double CategoryTax { get; protected set; }
        public double ShippingTax { get; protected set; }
        public ProductCategory ProductCategory { get; protected set; }
        public Product(string name, double basePrice)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ProductException("Name is required!");
            }

            if(basePrice <= 0.0)
            {
                throw new ProductException("The base price can't be zero or below!");
            }

            Id = NextId++;
            Name = name;
            BasePrice = basePrice;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, ProductCategory);
        }

        public override bool Equals(object? obj)
        {
            if(!(obj is Product)){
                return false;
            }

            Product other = obj as Product;

            return Name.Equals(other.Name) && ProductCategory.Equals(other.ProductCategory);
        }

        public void AlterBasePrice(double newBasePrice)
        {
            if(newBasePrice <= 0.0)
            {
                throw new ProductException("The new base pricae can't be zero or below!");
            }
            else
            {
                BasePrice = newBasePrice;
            }
        }

        public virtual double CalculateCommission()
        {
            return BasePrice * CommissionRate;
        }

        public virtual double CalculateFinalPrice()
        {
            return BasePrice + (BasePrice * CategoryTax);
        }

        public virtual double CalculateShipping()
        {
            return BasePrice * ShippingTax;
        }

        public override string ToString()
        {
            return $"{Name}\n\nCategory: {ProductCategory}\nBase price: {BasePrice}\nCommission: {CalculateCommission().ToString("F2", CultureInfo.InvariantCulture)}\nShipping: {CalculateShipping().ToString("F2", CultureInfo.InvariantCulture)}\nFinal price: {CalculateFinalPrice().ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }
}
