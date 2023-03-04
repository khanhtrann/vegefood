using System.ComponentModel.DataAnnotations;

namespace Vegefood.Models
{
    public class OrderItems
    {
        [Key]
        public int ID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        /// <summary>
        /// Navigational Properties
        /// </summary>
        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
