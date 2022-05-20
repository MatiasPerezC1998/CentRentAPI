namespace CentRent.Models
{
    public class CarRequest
    {
        public class CreateRequest
        {
            public string? Registration { get; set; }
            public int CarTypeId {get; set;}
        }

        public class UpdateRequest
        {
            public int Id { get; set; }          
            public string? Registration { get; set; }
            public int CarTypeId {get; set;}
        }
    }
}
