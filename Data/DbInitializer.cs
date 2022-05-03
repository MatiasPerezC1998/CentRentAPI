using CentRent.Models;

namespace CentRent.Data {
    public static class DbInitializer {
        public static void Initialize(CentRentContext context) {
            if (context.Cars.Any() &&
                context.Users.Any() &&
                context.Logs.Any()) {

                return; //DB has been seeded
            }

            var cars = new Car[] {
                new Car {
                    Id = 1,
                    Name = "Yaris",
                    Brand = "Toyota",
                    Type = "Deportivo",
                    Registration = "1863JVK",
                    IsRented = 0
                },
                new Car { 
                    Id = 2, 
                    Name = "Dacia-Logan", 
                    Brand = "Renault", 
                    Type = "None", 
                    Registration = "8139RHQ", 
                    IsRented = 0
                },
                new Car { 
                    Id = 3, 
                    Name = "Q3", 
                    Brand = "Audi", 
                    Type = "Todoterreno", 
                    Registration = "1998MPC", 
                    IsRented = 0
                }
            };

            var users = new User[] {
                new User { 
                    Id = 1, 
                    Name = "Alberto", 
                    Surname = "Mohedano", 
                    Email = "am@gmail.com", 
                    Phone = 123456789,
                    Dni = "12345678A",
                    CarRentedId = -1
                },
                new User { 
                    Id = 2, 
                    Name = "Paca", 
                    Surname = "Moya", 
                    Email = "pm@gmail.com", 
                    Phone = 987654321,
                    Dni = "87654321A",
                    CarRentedId = -1
                },
                new User { 
                    Id = 3, 
                    Name = "Matias", 
                    Surname = "Perez", 
                    Email = "mp@gmail.com", 
                    Phone = 147258369,
                    Dni = "14725836Z",
                    CarRentedId = -1
                }
            };

            var logs = new Log[] {
                new Log {
                    FirstName = "Andres",
                    LastName = "Beteta",
                    Email = "ab@gmail.com",
                    Username = "andres",
                    Password = "1"
                },
                new Log {
                    FirstName = "Paco",
                    LastName = "Peña",
                    Email = "pp@gmail.com",
                    Username = "paco",
                    Password = "1"
                },
                new Log {
                    FirstName = "Matias",
                    LastName = "Perez",
                    Email = "mp@gmail.com",
                    Username = "matias",
                    Password = "1"
                }
            };

            context.Cars.AddRange(cars);
            context.Users.AddRange(users);
            context.Logs.AddRange(logs);
            context.SaveChanges();
        }
    }
}