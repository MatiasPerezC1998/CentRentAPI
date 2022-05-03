using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CentRent.Helpers;
using CentRent.Models;
using CentRent.Data;

namespace CentRent.Services;

public interface ILogService {
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<Log> GetAll();
    Log GetById(string email);
    Log? Get(string email);
    Log Add(Log newLog);
}

public class LogService : ILogService {

    public readonly CentRentContext _context;
    private readonly AppSettings _appSettings;

    public LogService(CentRentContext context, IOptions<AppSettings> appSettings) {
        _context = context;
        _appSettings = appSettings.Value;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var log = _context.Logs.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        // return null if user not found
        if (log == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(log);

        return new AuthenticateResponse(log, token);
    }

    public IEnumerable<Log> GetAll()
    {
        return _context.Logs;
    }

    public Log GetById(string email)
    {
        return _context.Logs.FirstOrDefault(x => x.Email == email);
    }

    public Log? Get(string email) {
        return _context.Logs
            .AsNoTracking()
            .SingleOrDefault(p => p.Email == email);
    }

    public Log Add(Log newLog) {
        _context.Logs.Add(newLog);
        _context.SaveChanges();

        return newLog;
    }

    // helper methods

    private string generateJwtToken(Log log)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("email", log.Email) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}