using CSVSalesPro.Entities.Enums;
using CSVSalesPro.Entities.Exceptions;

namespace CSVSalesPro.Entities
{
    internal class FurnitureProduct : Product
    {
        public string Material { get; set; }

        public FurnitureProduct(string name, double basePrice, string material) : base(name, basePrice)
        {
            if (string.IsNullOrWhiteSpace(material))
            {
                throw new FurnitureProductException("Material is required!");
            }

            ProductCategory = ProductCategory.FURNITURE;
            Material = material;
            CommissionRate = 0.05;
            CategoryTax = 0.08;
            ShippingTax = 0.10;
        }

        public override double CalculateShipping()
        {
            double taxShipping = base.CalculateShipping();

            if(taxShipping < 80.00)
            {
                return 80.00;
            }
            else
            {
                return taxShipping;
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}\nMaterial: {Material}";
        }
    }
}
