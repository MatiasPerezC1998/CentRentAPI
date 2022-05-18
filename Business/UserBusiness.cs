using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CentRent.Helpers;
using CentRent.Models;
using CentRent.Entities;
using CentRent.Data;
using CentRent.Interfaces;

namespace CentRent.Business;

public class UserBusiness : IUserBusiness
{
    private readonly IUserRepository _userRepository;
    private readonly AppSettings _appSettings;

    public UserBusiness(IUserRepository userRepository, IOptions<AppSettings> appSettings)
    {
        _userRepository = userRepository;
        _appSettings = appSettings.Value;
    }

    public LoginResponse Login(UserRequest.LoginRequest model)
    {
        var user = _userRepository.Login(model);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new LoginResponse(user, token);
    }

    public IEnumerable<UserResponse> GetAll()
    {
        return _userRepository.GetAll();
    }

    public UserResponse GetByEmail(string email)
    {
        return _userRepository.GetByEmail(email);
    }

    public User Get(string email)
    {
        return _userRepository.Get(email);
    }

    public UserResponse Register(UserRequest.RegisterRequest newUser)
    {
        return _userRepository.Register(newUser);
    }

    public void Delete(string email)
    {
        var userToDelete = _userRepository.Get(email);
        if (userToDelete is not null)
        {
            _userRepository.Delete(userToDelete);
        }        
    }

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var time = _appSettings.Time;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("email", user.Email) }),
            Expires = DateTime.UtcNow.AddDays(time),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}