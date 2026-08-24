using CSVSalesPro.Entities.Enums;
using CSVSalesPro.Entities.Exceptions;

namespace CSVSalesPro.Entities
{
    internal class ElectronicProduct : Product
    {
        public int WarrantyMonths { get; set; }

        public ElectronicProduct(string name, double basePrice, int warrantyMonths) : base(name, basePrice)
        {
            if(warrantyMonths <= 0 || warrantyMonths > 48)
            {
                throw new ElectronicProductException("The warranty period must be between 1 and 12 months!");
            }

            ProductCategory = ProductCategory.ELECTRONIC;
            WarrantyMonths = warrantyMonths;
            CommissionRate = 0.08;
            CategoryTax = 0.12;
            ShippingTax = 0.03;
        }

        public override double CalculateShipping()
        {
            double taxShipping = base.CalculateShipping();

            if(taxShipping < 30.00)
            {
                return 30.00;
            }
            else
            {
                return taxShipping;
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}\nWarranty Months: {WarrantyMonths}";
        }
    }
}
