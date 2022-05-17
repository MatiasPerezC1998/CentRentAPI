using CentRent.Models;

namespace CentRent.Data;

public interface IUserRepository{
    LoginResponse Login(UserRequest.LoginRequest model);
    IEnumerable<UserResponse> GetAll();
    UserResponse GetById(string email);
    UserResponse? Get(string email);
    UserResponse Register(UserRequest.RegisterRequest newUser);
    void Delete(string email);
}