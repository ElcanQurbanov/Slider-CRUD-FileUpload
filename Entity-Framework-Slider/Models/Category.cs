using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Slider.Models
{
    public class Category:BaseEntity
    {
        [Required(ErrorMessage = "Don't be empty")]
        [StringLength(20, ErrorMessage = "The name lenght must be max 20 characters")]
        public string? Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
