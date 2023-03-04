using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Vegefood.Models
{
    public class Order
    {
        public int ID { get; set; }

        public string UserID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Zip { get; set; }

        public string CreditCard { get; set; }

        public string Timestamp { get; set; }
    }
}
