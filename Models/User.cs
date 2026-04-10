namespace CandyStoreInventory.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        private string _password;
        public string Role { get; set; }

        public User(int id, string username, string password, string role)
        {
            Id = id;
            Username = username;
            _password = password;
            Role = role;
        }

        public bool ValidatePassword(string password) => _password == password;

        public override string ToString() =>
            $"[{Id}] {Username} | Role: {Role}";
    }
}
