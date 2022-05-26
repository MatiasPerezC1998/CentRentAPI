using CentRent.Models;

namespace CentRent.Entities
{
    public partial class User
    {
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public User (UserRequest.RegisterRequest user)
        {
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Password = user.Password;
        }

        public User () {

        }
    }
}
