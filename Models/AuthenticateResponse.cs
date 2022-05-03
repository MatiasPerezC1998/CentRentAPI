namespace CentRent.Models;

public class AuthenticateResponse {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Token { get; set; }


    public AuthenticateResponse(Log log, string token)
    {
        FirstName = log.FirstName;
        LastName = log.LastName;
        Username = log.Username;
        Token = token;
    }
}