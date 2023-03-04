using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Vegefood.ViewModels
{
	public class CheckoutInput
	{
		[Display(Name = "Purchased Date:")]
		public DateTime Date { get; set; }

		[Required]
		[Display(Name = "First Name:")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last Name:")]
		public string LastName { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string City { get; set; }

		[Required]
		public string Province { get; set; }

		[Required]
		[DataType(DataType.PostalCode)]
		[Compare("Zip", ErrorMessage = "The is an invalid zip code")]
		public string Zip { get; set; }

		[Required]
		public CreditCard CreditCard { get; set; }
	}
	public enum CreditCard
	{
		Visa = 0,
		Mastercard,
		[Display(Name = "American Express")]
		AmericanExpress
	}
}


