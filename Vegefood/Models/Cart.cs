using System.ComponentModel.DataAnnotations;

namespace Vegefood.Models
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
    }
}
