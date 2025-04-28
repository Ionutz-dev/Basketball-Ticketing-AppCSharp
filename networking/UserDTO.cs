using System;

namespace networking
{
    [Serializable]
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserDTO(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }

        public override string ToString()
        {
            return $"UserDTO{{id={Id}, username='{Username}', password='{Password}'}}";
        }
    }
}