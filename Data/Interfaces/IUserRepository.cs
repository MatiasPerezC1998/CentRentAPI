using CentRent.Entities;
using CentRent.Models;

namespace CentRent.Data;

public interface IUserRepository{
    Task<User> Login(UserRequest.LoginRequest model);
    Task<IEnumerable<User>> GetAll();
    Task<User> GetByEmail(string email);
    Task<User> Get(string email);
    Task<User> Register(User newUser);
    Task Delete(User user);
}