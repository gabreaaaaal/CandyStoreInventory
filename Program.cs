using CandyStoreInventory;
using CandyStoreInventory.Models;

// ─── Default User ──────────────────────────────────────────────────────────────
var currentUser = new User(1, "admin", "admin123", "Admin");

Console.Clear();
PrintBanner();
Console.WriteLine("  Welcome to the Polangui Candy Corner Inventory System");
Console.WriteLine();
Thread.Sleep(800);

// ─── Main Menu ─────────────────────────────────────────────────────────────────
var manager = new InventoryManager(currentUser);
bool running = true;

while (running)
{
    Console.Clear();
    PrintBanner();
    InventoryManager.PrintLine();
    Console.WriteLine("  MAIN MENU");
    InventoryManager.PrintLine();
    Console.WriteLine("   [1] Manage Categories");
    Console.WriteLine("   [2] Manage Suppliers");
    Console.WriteLine("   [3] Manage Products");
    Console.WriteLine("   [4] Stock Operations");
    Console.WriteLine("   [5] Reports & History");
    Console.WriteLine("   [0] Exit");
    InventoryManager.PrintLine();
    Console.Write("  Choose: ");

    string choice = Console.ReadLine()?.Trim() ?? "";

    switch (choice)
    {
        case "1": CategoryMenu(manager); break;
        case "2": SupplierMenu(manager); break;
        case "3": ProductMenu(manager); break;
        case "4": StockMenu(manager); break;
        case "5": ReportsMenu(manager); break;
        case "0": running = false; break;
        default: ShowError("Invalid option. Please try again."); break;
    }
}

Console.Clear();
PrintBanner();
Console.WriteLine("  Thank you for using Polangui Candy Corner Inventory! Goodbye.");
Console.WriteLine();

// ─── Sub-Menus ─────────────────────────────────────────────────────────────────

void CategoryMenu(InventoryManager mgr)
{
    bool back = false;
    while (!back)
    {
        Console.Clear();
        PrintBanner();
        InventoryManager.PrintLine();
        Console.WriteLine("  CATEGORY MANAGEMENT");
        InventoryManager.PrintLine();
        Console.WriteLine("   [1] Add Category");
        Console.WriteLine("   [2] View All Categories");
        Console.WriteLine("   [0] Back");
        InventoryManager.PrintLine();
        Console.Write("  Choose: ");
        string opt = Console.ReadLine()?.Trim() ?? "";

        switch (opt)
        {
            case "1":
                try
                {
                    Console.Write("\n  Category Name: ");
                    string name = RequireInput("Category name");
                    Console.Write("  Description  : ");
                    string desc = RequireInput("Description");
                    mgr.AddCategory(name, desc);
                }
                catch (Exception ex) { ShowError(ex.Message); }
                Pause();
                break;
            case "2":
                mgr.ViewCategories();
                Pause();
                break;
            case "0": back = true; break;
            default: ShowError("Invalid option."); break;
        }
    }
}

void SupplierMenu(InventoryManager mgr)
{
    bool back = false;
    while (!back)
    {
        Console.Clear();
        PrintBanner();
        InventoryManager.PrintLine();
        Console.WriteLine("  SUPPLIER MANAGEMENT");
        InventoryManager.PrintLine();
        Console.WriteLine("   [1] Add Supplier");
        Console.WriteLine("   [2] View All Suppliers");
        Console.WriteLine("   [0] Back");
        InventoryManager.PrintLine();
        Console.Write("  Choose: ");
        string opt = Console.ReadLine()?.Trim() ?? "";

        switch (opt)
        {
            case "1":
                try
                {
                    Console.Write("\n  Supplier Name   : ");
                    string name = RequireInput("Supplier name");
                    Console.Write("  Contact Person  : ");
                    string contact = RequireInput("Contact person");
                    Console.Write("  Phone Number    : ");
                    string phone = RequireInput("Phone number");
                    mgr.AddSupplier(name, contact, phone);
                }
                catch (Exception ex) { ShowError(ex.Message); }
                Pause();
                break;
            case "2":
                mgr.ViewSuppliers();
                Pause();
                break;
            case "0": back = true; break;
            default: ShowError("Invalid option."); break;
        }
    }
}

void ProductMenu(InventoryManager mgr)
{
    bool back = false;
    while (!back)
    {
        Console.Clear();
        PrintBanner();
        InventoryManager.PrintLine();
        Console.WriteLine("  PRODUCT MANAGEMENT");
        InventoryManager.PrintLine();
        Console.WriteLine("   [1] Add Product");
        Console.WriteLine("   [2] View All Products");
        Console.WriteLine("   [3] Search Product");
        Console.WriteLine("   [4] Update Product");
        Console.WriteLine("   [5] Delete Product");
        Console.WriteLine("   [0] Back");
        InventoryManager.PrintLine();
        Console.Write("  Choose: ");
        string opt = Console.ReadLine()?.Trim() ?? "";

        switch (opt)
        {
            case "1":
                try
                {
                    mgr.ViewCategories();
                    mgr.ViewSuppliers();
                    Console.Write("  Product Name : ");
                    string name = RequireInput("Product name");
                    Console.Write("  Description  : ");
                    string desc = RequireInput("Description");
                    Console.Write("  Price (₱)    : ");
                    double price = ParsePositiveDouble("Price");
                    Console.Write("  Initial Stock: ");
                    int stock = ParsePositiveInt("Stock");
                    Console.Write("  Category ID  : ");
                    int catId = ParsePositiveInt("Category ID");
                    Console.Write("  Supplier ID  : ");
                    int supId = ParsePositiveInt("Supplier ID");
                    mgr.AddProduct(name, desc, price, stock, catId, supId);
                }
                catch (Exception ex) { ShowError(ex.Message); }
                Pause();
                break;

            case "2":
                mgr.ViewProducts();
                Pause();
                break;

            case "3":
                try
                {
                    Console.Write("\n  Search keyword: ");
                    string kw = RequireInput("Keyword");
                    mgr.SearchProduct(kw);
                }
                catch (Exception ex) { ShowError(ex.Message); }
                Pause();
                break;

            case "4":
                try
                {
                    mgr.ViewProducts();
                    Console.Write("  Product ID to update: ");
                    int id = ParsePositiveInt("Product ID");
                    Console.Write("  New Name        : ");
                    string name = RequireInput("Name");
                    Console.Write("  New Description : ");
                    string desc = RequireInput("Description");
                    Console.Write("  New Price (₱)   : ");
                    double price = ParsePositiveDouble("Price");
                    mgr.ViewCategories();
                    Console.Write("  New Category ID : ");
                    int catId = ParsePositiveInt("Category ID");
                    mgr.ViewSuppliers();
                    Console.Write("  New Supplier ID : ");
                    int supId = ParsePositiveInt("Supplier ID");
                    mgr.UpdateProduct(id, name, desc, price, catId, supId);
                }
                catch (Exception ex) { ShowError(ex.Message); }
                Pause();
                break;

            case "5":
                try
                {
                    mgr.ViewProducts();
                    Console.Write("  Product ID to delete: ");
                    int id = ParsePositiveInt("Product ID");
                    Console.Write($"  Are you sure? (yes/no): ");
                    string confirm = Console.ReadLine()?.Trim().ToLower() ?? "";
                    if (confirm == "yes") mgr.DeleteProduct(id);
                    else Console.WriteLine("\n  Deletion cancelled.");
                }
                catch (Exception ex) { ShowError(ex.Message); }
                Pause();
                break;

            case "0": back = true; break;
            default: ShowError("Invalid option."); break;
        }
    }
}

void StockMenu(InventoryManager mgr)
{
    bool back = false;
    while (!back)
    {
        Console.Clear();
        PrintBanner();
        InventoryManager.PrintLine();
        Console.WriteLine("  STOCK OPERATIONS");
        InventoryManager.PrintLine();
        Console.WriteLine("   [1] Restock Product");
        Console.WriteLine("   [2] Deduct Stock");
        Console.WriteLine("   [0] Back");
        InventoryManager.PrintLine();
        Console.Write("  Choose: ");
        string opt = Console.ReadLine()?.Trim() ?? "";

        switch (opt)
        {
            case "1":
                try
                {
                    mgr.ViewProducts();
                    Console.Write("  Product ID : ");
                    int id = ParsePositiveInt("Product ID");
                    Console.Write("  Quantity   : ");
                    int qty = ParsePositiveInt("Quantity");
                    mgr.RestockProduct(id, qty);
                }
                catch (Exception ex) { ShowError(ex.Message); }
                Pause();
                break;

            case "2":
                try
                {
                    mgr.ViewProducts();
                    Console.Write("  Product ID : ");
                    int id = ParsePositiveInt("Product ID");
                    Console.Write("  Quantity   : ");
                    int qty = ParsePositiveInt("Quantity");
                    mgr.DeductStock(id, qty);
                }
                catch (Exception ex) { ShowError(ex.Message); }
                Pause();
                break;

            case "0": back = true; break;
            default: ShowError("Invalid option."); break;
        }
    }
}

void ReportsMenu(InventoryManager mgr)
{
    bool back = false;
    while (!back)
    {
        Console.Clear();
        PrintBanner();
        InventoryManager.PrintLine();
        Console.WriteLine("  REPORTS & HISTORY");
        InventoryManager.PrintLine();
        Console.WriteLine("   [1] Transaction History");
        Console.WriteLine("   [2] Low Stock Items");
        Console.WriteLine("   [3] Total Inventory Value");
        Console.WriteLine("   [0] Back");
        InventoryManager.PrintLine();
        Console.Write("  Choose: ");
        string opt = Console.ReadLine()?.Trim() ?? "";

        switch (opt)
        {
            case "1": mgr.ViewTransactions(); Pause(); break;
            case "2": mgr.ShowLowStock();     Pause(); break;
            case "3": mgr.ShowInventoryValue(); Pause(); break;
            case "0": back = true; break;
            default: ShowError("Invalid option."); break;
        }
    }
}

// ─── Utility Methods ───────────────────────────────────────────────────────────

void PrintBanner()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine();
    Console.WriteLine("  ╔══════════════════════════════════════════════════════════════════════════╗");
    Console.WriteLine("  ║              POLANGUI CANDY CORNER  --  Inventory System               ║");
    Console.WriteLine("  ╚══════════════════════════════════════════════════════════════════════════╝");
    Console.ResetColor();
    Console.WriteLine();
}

void ShowError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n  ✖ Error: {message}");
    Console.ResetColor();
}

void Pause()
{
    Console.Write("\n  Press Enter to continue...");
    Console.ReadLine();
}

string RequireInput(string fieldName)
{
    string value = Console.ReadLine()?.Trim() ?? "";
    if (string.IsNullOrWhiteSpace(value))
        throw new Exception($"{fieldName} cannot be empty.");
    return value;
}

double ParsePositiveDouble(string fieldName)
{
    string raw = Console.ReadLine()?.Trim() ?? "";
    if (!double.TryParse(raw, out double result) || result <= 0)
        throw new Exception($"{fieldName} must be a positive number.");
    return result;
}

int ParsePositiveInt(string fieldName)
{
    string raw = Console.ReadLine()?.Trim() ?? "";
    if (!int.TryParse(raw, out int result) || result <= 0)
        throw new Exception($"{fieldName} must be a positive whole number.");
    return result;
}

