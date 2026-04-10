using CandyStoreInventory.Models;

namespace CandyStoreInventory
{
    public class InventoryManager
    {
        private List<Product> _products = new List<Product>();
        private List<Category> _categories = new List<Category>();
        private List<Supplier> _suppliers = new List<Supplier>();
        private List<TransactionRecord> _transactions = new List<TransactionRecord>();

        private int _nextProductId = 1;
        private int _nextCategoryId = 1;
        private int _nextSupplierId = 1;
        private int _nextTransactionId = 1;

        private User _currentUser;

        public InventoryManager(User user)
        {
            _currentUser = user;
            SeedData();
        }

        // ─── Seed Data ─────────────────────────────────────────────────────────
        private void SeedData()
        {
            // Categories
            _categories.Add(new Category(_nextCategoryId++, "Chocolate", "Chocolate-based candies"));
            _categories.Add(new Category(_nextCategoryId++, "Candy Bar", "Nut and caramel bars"));
            _categories.Add(new Category(_nextCategoryId++, "Gummy & Chewy", "Chewy and fruity candies"));

            // Suppliers
            _suppliers.Add(new Supplier(_nextSupplierId++, "Mars Inc.", "John Mars", "09171234567"));
            _suppliers.Add(new Supplier(_nextSupplierId++, "Hershey Co.", "Jane Hershey", "09187654321"));
            _suppliers.Add(new Supplier(_nextSupplierId++, "Wrigley Co.", "Bob Wrigley", "09199876543"));

            // Products (8 candies)
            AddProductInternal("M&M's",                  "Colorful chocolate-coated candies",    45.00, 100, 1, 1);
            AddProductInternal("Reese's Peanut Butter Cups", "Chocolate cups with peanut butter", 55.00,  80, 1, 2);
            AddProductInternal("Hershey's Bar",          "Classic milk chocolate bar",            40.00,  90, 1, 2);
            AddProductInternal("Snickers",               "Nougat, caramel, and peanuts bar",      50.00,  75, 2, 1);
            AddProductInternal("Kit Kat",                "Crispy wafer chocolate bar",            48.00,  60, 2, 2);
            AddProductInternal("Twix",                   "Caramel and biscuit chocolate bar",     50.00,  70, 2, 1);
            AddProductInternal("Twizzlers",              "Red licorice twists",                   35.00,  50, 3, 3);
            AddProductInternal("Skittles",               "Fruity rainbow-colored candies",        40.00,  85, 3, 3);
        }

        private void AddProductInternal(string name, string desc, double price, int stock, int catId, int supId)
        {
            _products.Add(new Product(_nextProductId++, name, desc, price, stock, catId, supId));
            LogTransaction("ADD", _nextProductId - 1, name, stock, "System", "Initial seed");
        }

        // ─── Categories ────────────────────────────────────────────────────────
        public void AddCategory(string name, string description)
        {
            _categories.Add(new Category(_nextCategoryId++, name, description));
            Console.WriteLine($"\n  ✔ Category '{name}' added successfully.");
        }

        public void ViewCategories()
        {
            if (_categories.Count == 0) { Console.WriteLine("\n  No categories found."); return; }
            PrintLine();
            Console.WriteLine("  CATEGORIES");
            PrintLine();
            foreach (var c in _categories) Console.WriteLine("  " + c);
            PrintLine();
        }

        public Category? GetCategory(int id) => _categories.Find(c => c.Id == id);

        // ─── Suppliers ─────────────────────────────────────────────────────────
        public void AddSupplier(string name, string contact, string phone)
        {
            _suppliers.Add(new Supplier(_nextSupplierId++, name, contact, phone));
            Console.WriteLine($"\n  ✔ Supplier '{name}' added successfully.");
        }

        public void ViewSuppliers()
        {
            if (_suppliers.Count == 0) { Console.WriteLine("\n  No suppliers found."); return; }
            PrintLine();
            Console.WriteLine("  SUPPLIERS");
            PrintLine();
            foreach (var s in _suppliers) Console.WriteLine("  " + s);
            PrintLine();
        }

        public Supplier? GetSupplier(int id) => _suppliers.Find(s => s.Id == id);

        // ─── Products ──────────────────────────────────────────────────────────
        public void AddProduct(string name, string desc, double price, int stock, int catId, int supId)
        {
            if (GetCategory(catId) == null) throw new Exception("Category ID not found.");
            if (GetSupplier(supId) == null) throw new Exception("Supplier ID not found.");

            var product = new Product(_nextProductId++, name, desc, price, stock, catId, supId);
            _products.Add(product);
            LogTransaction("ADD", product.Id, name, stock, _currentUser.Username);
            Console.WriteLine($"\n  ✔ Product '{name}' added successfully (ID: {product.Id}).");
        }

        public void ViewProducts()
        {
            if (_products.Count == 0) { Console.WriteLine("\n  No products found."); return; }
            PrintLine();
            Console.WriteLine($"  {"ID",-4} {"Name",-30} {"Price",8} {"Stock",6} {"Category",-15} {"Supplier",-15}");
            PrintLine();
            foreach (var p in _products)
            {
                var cat = GetCategory(p.CategoryId)?.Name ?? "N/A";
                var sup = GetSupplier(p.SupplierId)?.Name ?? "N/A";
                string lowTag = p.IsLowStock() ? " ⚠" : "";
                Console.WriteLine($"  {p.Id,-4} {p.Name,-30} ₱{p.Price,7:F2} {p.Stock,6} {cat,-15} {sup,-15}{lowTag}");
            }
            PrintLine();
        }

        public void SearchProduct(string keyword)
        {
            var results = _products.FindAll(p =>
                p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase));

            if (results.Count == 0) { Console.WriteLine("\n  No products matched your search."); return; }

            PrintLine();
            Console.WriteLine($"  Search results for: \"{keyword}\"");
            PrintLine();
            foreach (var p in results)
            {
                var cat = GetCategory(p.CategoryId)?.Name ?? "N/A";
                var sup = GetSupplier(p.SupplierId)?.Name ?? "N/A";
                Console.WriteLine($"  {p.Id,-4} {p.Name,-30} ₱{p.Price,7:F2} Stock:{p.Stock,5}  [{cat}] [{sup}]");
                Console.WriteLine($"       {p.Description}");
            }
            PrintLine();
        }

        public void UpdateProduct(int id, string name, string desc, double price, int catId, int supId)
        {
            var p = _products.Find(x => x.Id == id) ?? throw new Exception("Product not found.");
            if (GetCategory(catId) == null) throw new Exception("Category ID not found.");
            if (GetSupplier(supId) == null) throw new Exception("Supplier ID not found.");

            string oldName = p.Name;
            p.Name = name;
            p.Description = desc;
            p.Price = price;
            p.CategoryId = catId;
            p.SupplierId = supId;

            LogTransaction("UPDATE", id, name, 0, _currentUser.Username, $"Updated from '{oldName}'");
            Console.WriteLine($"\n  ✔ Product updated successfully.");
        }

        public void DeleteProduct(int id)
        {
            var p = _products.Find(x => x.Id == id) ?? throw new Exception("Product not found.");
            _products.Remove(p);
            LogTransaction("DELETE", id, p.Name, p.Stock, _currentUser.Username);
            Console.WriteLine($"\n  ✔ Product '{p.Name}' deleted.");
        }

        public void RestockProduct(int id, int quantity)
        {
            if (quantity <= 0) throw new Exception("Quantity must be greater than 0.");
            var p = _products.Find(x => x.Id == id) ?? throw new Exception("Product not found.");
            p.Stock += quantity;
            LogTransaction("RESTOCK", id, p.Name, quantity, _currentUser.Username);
            Console.WriteLine($"\n  ✔ Restocked '{p.Name}' by {quantity}. New stock: {p.Stock}");
        }

        public void DeductStock(int id, int quantity)
        {
            if (quantity <= 0) throw new Exception("Quantity must be greater than 0.");
            var p = _products.Find(x => x.Id == id) ?? throw new Exception("Product not found.");
            if (p.Stock < quantity) throw new Exception($"Insufficient stock. Available: {p.Stock}");
            p.Stock -= quantity;
            LogTransaction("DEDUCT", id, p.Name, quantity, _currentUser.Username);
            Console.WriteLine($"\n  ✔ Deducted {quantity} from '{p.Name}'. Remaining stock: {p.Stock}");
            if (p.IsLowStock()) Console.WriteLine($"  ⚠  Warning: '{p.Name}' is now low on stock ({p.Stock} left)!");
        }

        public void ShowLowStock()
        {
            var low = _products.FindAll(p => p.IsLowStock());
            if (low.Count == 0) { Console.WriteLine("\n  All products have sufficient stock. ✔"); return; }
            PrintLine();
            Console.WriteLine("  ⚠  LOW STOCK ITEMS");
            PrintLine();
            foreach (var p in low)
                Console.WriteLine($"  {p.Id,-4} {p.Name,-30} Stock: {p.Stock,4}  (Threshold: {p.LowStockThreshold})");
            PrintLine();
        }

        public void ShowInventoryValue()
        {
            double total = 0;
            PrintLine();
            Console.WriteLine($"  {"Product",-30} {"Price",8} {"Stock",6} {"Value",12}");
            PrintLine();
            foreach (var p in _products)
            {
                double val = p.TotalValue();
                total += val;
                Console.WriteLine($"  {p.Name,-30} ₱{p.Price,7:F2} {p.Stock,6} ₱{val,11:F2}");
            }
            PrintLine();
            Console.WriteLine($"  {"TOTAL INVENTORY VALUE",-38} ₱{total,11:F2}");
            PrintLine();
        }

        public void ViewTransactions()
        {
            if (_transactions.Count == 0) { Console.WriteLine("\n  No transactions recorded."); return; }
            PrintLine();
            Console.WriteLine("  TRANSACTION HISTORY");
            PrintLine();
            foreach (var t in _transactions) Console.WriteLine("  " + t);
            PrintLine();
        }

        // ─── Helpers ───────────────────────────────────────────────────────────
        private void LogTransaction(string type, int productId, string productName, int qty, string user, string notes = "")
        {
            _transactions.Add(new TransactionRecord(_nextTransactionId++, type, productId, productName, qty, user, notes));
        }

        public static void PrintLine() => Console.WriteLine("  " + new string('─', 72));
    }
}
