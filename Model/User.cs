namespace model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }

        public override string ToString()
        {
            return $"User{{id={Id}, username='{Username}', password='{Password}'}}";
        }
    }
}