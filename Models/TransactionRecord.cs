namespace CandyStoreInventory.Models
{
    public class TransactionRecord
    {
        public int Id { get; set; }
        public string Type { get; set; }       // "ADD", "RESTOCK", "DEDUCT", "UPDATE", "DELETE"
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string PerformedBy { get; set; }
        public DateTime Timestamp { get; set; }
        public string Notes { get; set; }

        public TransactionRecord(int id, string type, int productId, string productName, int quantity, string performedBy, string notes = "")
        {
            Id = id;
            Type = type;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            PerformedBy = performedBy;
            Timestamp = DateTime.Now;
            Notes = notes;
        }

        public override string ToString() =>
            $"[{Id}] {Timestamp:yyyy-MM-dd HH:mm:ss} | {Type,-8} | Product: {ProductName} | Qty: {Quantity,5} | By: {PerformedBy} | {Notes}";
    }
}
