using CentRent.Models;
using CentRent.Entities;

namespace CentRent.Interfaces;

public interface IUserBusiness{
    LoginResponse Login(UserRequest.LoginRequest model);
    IEnumerable<UserResponse> GetAll();
    UserResponse GetByEmail(string email);
    User Get(string email);
    UserResponse Register(UserRequest.RegisterRequest newUser);
    void Delete(string email);
}