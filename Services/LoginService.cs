// using Microsoft.EntityFrameworkCore;

// using CentRent.Models;
// using CentRent.Entities;
// using CentRent.Data;

// namespace CentRent.Services;

// public class LoginService {
//     // static List<Login> Logins { get; }
//     // static int nextId = 4;
//     // static LoginService() {
//     // }

//     private readonly CentRentContext _context;

//     public LoginService(CentRentContext context) {
//         _context = context;
//     }

//     public IEnumerable<Log> GetAll() {
//         return _context.Logs;
//     }

//     public Log? Get(string email) {
//         return _context.Logs
//             .AsNoTracking()
//             .SingleOrDefault(p => p.Email == email);
//     }

//     public Log Add(Log newLog) { // Cambiamos login por newLog
//         _context.Logs.Add(newLog);
//         _context.SaveChanges();

//         return newLog;

//         // var newLogins = Logins.Select(c => c.Name).ToList();
//         // if (!newLogins.Contains(login.Name) && login.Name != "" && login.Password != "") {
            
//         //     login.Id = nextId++;
//         //     Logins.Add(login);
//         //     Console.WriteLine(login);

//         // } else if (login.Name == "" || login.Password == "") {

//         //     Console.WriteLine("Error, usuario o contraseña no introducidos");
        
//         // } else if (newLogins.Contains(login.Name)) {

//         //     Console.WriteLine("Error, usuario existente, introduzca uno nuevo");
        
//         // } else {

//         //     Console.WriteLine("Error genérico");
            
//         // }
//     }

// }