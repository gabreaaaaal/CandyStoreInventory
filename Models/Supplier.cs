namespace CandyStoreInventory.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }

        public Supplier(int id, string name, string contactPerson, string phone)
        {
            Id = id;
            Name = name;
            ContactPerson = contactPerson;
            Phone = phone;
        }

        public override string ToString() =>
            $"[{Id}] {Name} | Contact: {ContactPerson} | Phone: {Phone}";
    }
}
