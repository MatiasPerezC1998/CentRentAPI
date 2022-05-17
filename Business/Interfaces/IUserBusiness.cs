using Microsoft.EntityFrameworkCore;
using CentRent.Models;
using CentRent.Entities;
using CentRent.Data;
using CentRent.Services;

namespace CentRent.Interfaces;

public interface IUserBusiness{
    LoginResponse Login(UserRequest.LoginRequest model);
    IEnumerable<UserResponse> GetAll();
    UserResponse GetById(string email);
    UserResponse? Get(string email);
    UserResponse Register(UserRequest.RegisterRequest newUser);
    void Delete(string email);
}