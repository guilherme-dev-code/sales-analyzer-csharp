using CSVSalesPro.Entities.Exceptions;

namespace CSVSalesPro.Entities
{
    public class SaleItem
    {
        private static int NextId = 1;

        public int Id { get; private set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public SaleItem() { }
        public SaleItem(Product product, int quantity) {

            if(quantity <= 0)
            {
                throw new SaleItemException("The quantity of the product cannot be less than or equal to zero!");
            }

            if(product == null)
            {
                throw new SaleItemException("The product is required!");
            }

            Id = NextId++;
            Product = product;
            Quantity = quantity;
            UnitPrice = product.BasePrice;
        }

        public override string ToString()
        {
            return $"Product: {Product}\nQuantity: {Quantity}\nUnit price: {UnitPrice}";
        }

        public double CalculateSubtotal()
        {
            return Quantity * UnitPrice;
        }
    }
}
