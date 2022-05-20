using System.ComponentModel.DataAnnotations;

namespace CentRent.Models;

public class CarImageRequest
{
    [Required]
    public IFormFile File { get; set; }
}