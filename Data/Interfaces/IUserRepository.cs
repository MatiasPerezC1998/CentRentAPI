using CentRent.Entities;
using CentRent.Models;

namespace CentRent.Data;

public interface IUserRepository{
    User Login(UserRequest.LoginRequest model);
    IEnumerable<UserResponse> GetAll();
    UserResponse GetById(string email);
    User Get(string email);
    UserResponse Register(UserRequest.RegisterRequest newUser);
    void Delete(User user);
}