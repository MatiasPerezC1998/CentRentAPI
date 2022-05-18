using CentRent.Entities;

namespace CentRent.Models;

public class LoginResponse {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Email { get; }
    public string? Token { get; set; }

    // LoginResponse Copy Constructor
    public LoginResponse(User user, string token)
    {
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        Email = user.Email;
        Token = token;
    }
}