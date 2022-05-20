namespace CentRent.Models
{
    public class CarTypeRequest
    {
        public class CreateRequest
        {
            public string? Brand { get; set; }
            public string? Model { get; set; }
            public string? Type { get; set; }
            public IFormFile? File { get; set; }
        }

        public class UpdateRequest
        {
            public int Id { get; set; }
            public string? Brand { get; set; }
            public string? Model { get; set; }
            public string? Type { get; set; }   
            public IFormFile? File { get; set; }
        }
    }
}
