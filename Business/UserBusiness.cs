using Microsoft.EntityFrameworkCore;
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

namespace CentRent.Services;

public class UserBusiness : IUserBusiness
{
    public readonly CentRentContext _context;
    private readonly AppSettings _appSettings;

    public UserBusiness(CentRentContext context, IOptions<AppSettings> appSettings)
    {
        _context = context;
        _appSettings = appSettings.Value;
    }

    public LoginResponse Login(UserRequest.LoginRequest model)
    {
        var user = _context.Users
            .SingleOrDefault(x =>
                x.Username == model.Username &&
                x.Password == model.Password
            );

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new LoginResponse(user, token);
    }

    public IEnumerable<UserResponse> GetAll()
    {
        return _context.Users
            .Select(p => new UserResponse(p))
            .AsNoTracking()
            .ToList();
    }

    public UserResponse GetById(string email)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == email);

        return new UserResponse(user);
    }

    public UserResponse? Get(string email)
    {
        var user = _context.Users
            .AsNoTracking()
            .SingleOrDefault(p => p.Email == email);

        return new UserResponse(user);

    }

    public UserResponse Register(UserRequest.RegisterRequest newUser)
    {
        var user = new User
        {
            Email = newUser.Email,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Password = newUser.Password,
            Username =  newUser.Username
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return new UserResponse(user);
    }

    public void Delete(string email)
    {
        var userToDelete = _context.Users.Find(email);
        if (userToDelete is not null)
        {
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
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