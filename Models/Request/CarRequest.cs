namespace CentRent.Models
{
    public class CarRequest
    {
        public class CreateRequest
        {
            public string? Name { get; set; }
            public string? Brand { get; set; }
            public string? Type { get; set; }
            public string? Registration { get; set; }
            public int IsRented { get; set; }
            public string? Image { get; set; }
        }

        public class UpdateRequest
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Brand { get; set; }
            public string? Type { get; set; }
            public string? Registration { get; set; }
            public int IsRented { get; set; }       
            public string? Image { get; set; }
        }
    }
}
