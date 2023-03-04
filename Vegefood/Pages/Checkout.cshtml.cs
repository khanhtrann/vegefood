using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegefood.Models.Interfaces;
using Vegefood.Models;
using Vegefood.ViewModels;
using AuthorizeNet.Api.Contracts.V1;

namespace Vegefood.Pages
{
	[Authorize]
	public class CheckoutModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IShop _shop;
		private readonly IPayment _payment;
		private readonly IOrder _order;

		public CheckoutModel(UserManager<ApplicationUser> userManager, IShop shop, IPayment payment, IOrder order)
		{
			_userManager = userManager;
			_shop = shop;
			_payment = payment;
			_order = order;
		}

		[BindProperty]
		public CheckoutInput Input { get; set; }
		public decimal Total { get; set; }
		public async Task OnGetAsync()
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);
			IEnumerable<CartItems> cartItems = await _shop.GetCartItemsByUserIdAsync(user.Id);

			foreach (var cartItem in cartItems)
			{
				Total += cartItem.Product.Price * cartItem.Quantity;
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = await _userManager.GetUserAsync(User);

				Order order = new Order
				{
					UserID = user.Id,
					FirstName = Input.FirstName,
					LastName = Input.LastName,
					Address = Input.Address,
					City = Input.City,
					Province = Input.Province,
					Zip = Input.Zip,
					CreditCard = Input.CreditCard.ToString(),
					Timestamp = DateTime.Now.ToString()
				};

				await _order.SaveOrderAsync(order);

				order = await _order.GetLatestOrderForUserAsync(user.Id);

				IEnumerable<CartItems> cartItems = await _shop.GetCartItemsByUserIdAsync(user.Id);
				IList<OrderItems> orderItems = new List<OrderItems>();
				decimal total = 0;

				foreach (var cartItem in cartItems)
				{
					OrderItems orderItem = new OrderItems
					{
						OrderID = order.ID,
						ProductID = cartItem.ProductID,
						Quantity = cartItem.Quantity
					};
					orderItems.Add(orderItem);
					total += cartItem.Product.Price * cartItem.Quantity;
				}

				double finalCost = decimal.ToDouble(total);
				foreach (var item in orderItems)
				{
					await _order.SaveOrderItemAsync(item);
				}

				string creditCardNumber;
				string creditCardExpiration;

				string cardType = Input.CreditCard.ToString();
				switch (cardType)
				{
					case "0":
						creditCardNumber = "4111111111111111";
						creditCardExpiration = "0723";
						break;
					case "1":
						creditCardNumber = "5424000000000015";
						creditCardExpiration = "0922";
						break;
					case "2":
						creditCardNumber = "370000000000002";
						creditCardExpiration = "1222";
						break;
					default:
						creditCardNumber = "4111111111111111";
						creditCardExpiration = "0723";
						break;
				}

				var creditCard = new creditCardType
				{
					cardNumber = creditCardNumber,
					expirationDate = creditCardExpiration
				};

				var billingAdress = new customerAddressType
				{
					firstName = Input.FirstName,
					lastName = Input.LastName,
					address = Input.Address,
					city = Input.City,
					state = Input.Province,
					zip = Input.Zip
				};

				if (_payment.Run(finalCost, creditCard, billingAdress))
				{
					await _shop.RemoveCartItemsAsync(cartItems);

					return Redirect("/Receipt");
				}
			}
			return Page();
		}
	}
}
