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

    public async Task<LoginResponse> Login(UserRequest.LoginRequest model)
    {
        // var login = new UserRequest.LoginRequest(model);
        // var login = new UserRequest.LoginRequest(model);
        var user = await _userRepository.Login(model);

        // return null if user not found
        if (user == null)
        {
            return null;
        }

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new LoginResponse(user, token);
    }

    public async Task<IEnumerable<UserResponse>> GetAll()
    {
        var allUsers = await _userRepository.GetAll();
        return new List<UserResponse>(allUsers.Select(p => new UserResponse(p)));
    }

    public async Task<UserResponse> GetByEmail(string email)
    {
        return new UserResponse(await _userRepository.GetByEmail(email));
    }

    public async Task<User> Get(string email)
    {
        return await _userRepository.Get(email);
    }

    public async Task<UserResponse> Register(UserRequest.RegisterRequest newUser)
    {
        var registerToAdd = new User(newUser);
        return new UserResponse(await _userRepository.Register(registerToAdd));
    }

    public async Task Delete(string email)
    {
        var userToDelete = await _userRepository.Get(email);
        if (userToDelete is not null)
        {
            await _userRepository.Delete(userToDelete);
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
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.Audience,
            Subject = new ClaimsIdentity(new[] { new Claim("email", user.Email) }),
            Expires = DateTime.UtcNow.AddDays(time),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}