using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

    public async Task<User> Login(UserRequest.LoginRequest model)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(x =>
                x.Username == model.Username &&
                x.Password == model.Password
            );

        return user;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users
            .Select(p => p)
            .ToListAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        return user;
    }

    public async Task<User> Get(string email)
    {
        var user = await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Email == email);

        return user;
    }

    public async Task<User> Register(User newUser)
    {
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

    public async Task Delete(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();     
    }
}