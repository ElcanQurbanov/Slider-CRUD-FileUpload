
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity_Framework_Slider.Models
{
    public class Blog:BaseEntity
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Header { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }
        public DateTime Date { get; set; }
    }
}
