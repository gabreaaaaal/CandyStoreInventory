namespace CandyStoreInventory.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public int LowStockThreshold { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }

        public Product(int id, string name, string description, double price, int stock, int categoryId, int supplierId, int lowStockThreshold = 10)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
            SupplierId = supplierId;
            LowStockThreshold = lowStockThreshold;
        }

        public bool IsLowStock() => Stock <= LowStockThreshold;

        public double TotalValue() => Price * Stock;

        public override string ToString() =>
            $"[{Id}] {Name} | Price: ₱{Price:F2} | Stock: {Stock} | Category ID: {CategoryId} | Supplier ID: {SupplierId}";
    }
}
