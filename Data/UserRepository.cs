using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CentRent.Helpers;
using CentRent.Models;
using CentRent.Entities;

namespace CentRent.Data;

public class UserRepository : IUserRepository
{
    public readonly CentRentContext _context;

    public UserRepository(CentRentContext context, IOptions<AppSettings> appSettings)
    {
        _context = context;
    }

    public User Login(UserRequest.LoginRequest model)
    {
        var user = _context.Users
            .SingleOrDefault(x =>
                x.Username == model.Username &&
                x.Password == model.Password
            );

        return user;
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

    public User Get(string email)
    {
        var user = _context.Users
            .AsNoTracking()
            .SingleOrDefault(p => p.Email == email);

        return user;
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

    public void Delete(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();     
    }
}