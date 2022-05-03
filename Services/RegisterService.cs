// using Microsoft.EntityFrameworkCore;

// using CentRent.Entities;
// using CentRent.Helpers;
// using CentRent.Models;
// using CentRent.Data;

// namespace CentRent.Services;

// public class RegisterService {
//     private readonly CentRentContext _context;
//     public RegisterService(CentRentContext context) {
//         _context = context;
//     }

//     public IEnumerable<Log> GetAll() {
//         return _context.Logs;
//     }
//     public Log Add(Log newLog) { // Cambiamos register por newLog
//         _context.Logs.Add(newLog);
//         _context.SaveChanges();

//         return newLog;

//         // var newRegisters = LogService._logs.Select(c => c.Email).ToList();
//         // if (!newRegisters.Contains(register.Email) &&
//         //     register.FirstName != "" &&
//         //     register.LastName != "" &&
//         //     register.Username != "" &&
//         //     register.Password != "" &&
//         //     register.Email != "") {
            
//         //     LogService._logs.Add(register);
//         //     Console.WriteLine(register);

//         // } else if (register.FirstName == "" ||
//         //     register.LastName == "" ||
//         //     register.Email == "" ||
//         //     register.Username == "" ||
//         //     register.Password == "") {
            
//         //     Console.WriteLine("Error, no puede haber ningún campo vacío");

//         // } else if (newRegisters.Contains(register.Email)) {

//         //     Console.WriteLine("Error, el correo electrónico ya existe, introduzca uno diferente");
        
//         // } else {

//         //     Console.WriteLine("Error genérico");

//         // }
//     }
// }