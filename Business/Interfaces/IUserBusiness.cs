using CentRent.Models;
using CentRent.Entities;

namespace CentRent.Interfaces;

public interface IUserBusiness{
    Task<LoginResponse> Login(UserRequest.LoginRequest model);
    Task<IEnumerable<UserResponse>> GetAll();
    Task<UserResponse> GetByEmail(string email);
    Task<User> Get(string email);
    Task<UserResponse> Register(UserRequest.RegisterRequest newUser);
    Task Delete(string email);
}