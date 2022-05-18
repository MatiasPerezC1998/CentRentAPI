using CentRent.Entities;

namespace CentRent.Models;

public class UserResponse
{
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }

    // UserResponse Copy Constructor
    public UserResponse (User user)
    {
        Email = user.Email;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
    }
}