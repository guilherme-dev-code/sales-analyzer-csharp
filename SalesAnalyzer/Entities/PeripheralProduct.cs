using CSVSalesPro.Entities.Enums;
using CSVSalesPro.Entities.Exceptions;

namespace CSVSalesPro.Entities
{
    internal class PeripheralProduct : Product
    {
        public ConnectionType ConnectionType { get; set; }

        public PeripheralProduct(string name, double basePrice, ConnectionType connectionType):base(name, basePrice)
        {
            ProductCategory = ProductCategory.PERIPHERAL;
            ConnectionType = connectionType;
            CommissionRate = 0.03;
            CategoryTax = 0.05;
            ShippingTax = 15.00;
        }

        public override double CalculateShipping()
        {
            return ShippingTax;
        }

        public override string ToString()
        {
            return $"{base.ToString()}\nConnection type: {ConnectionType}";
        }
    }
}
