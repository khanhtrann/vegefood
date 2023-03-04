using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Vegefood.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter the name.")]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The number must be greater than zero.")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The number must be greater than zero.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Please enter the description.")]
        public string Description { get; set; }

        public string Image { get; set; }
        [Required]
        public bool IsFeatured { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}
