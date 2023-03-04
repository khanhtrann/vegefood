using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegefood.Models.Interfaces;
using Vegefood.Models;

namespace Vegefood.Pages
{
	public class CartModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IShop _shop;

		public CartModel(UserManager<ApplicationUser> userManager, IShop shopcontext)
		{
			_shop = shopcontext;
			_userManager = userManager;
		}

		public IEnumerable<CartItems> CartItem { get; set; }
		public decimal Total { get; set; }

		public async Task OnGetAsync()
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);
			CartItem = await _shop.GetCartItemsByUserIdAsync(user.Id);
			foreach (var item in CartItem)
			{
				Total += item.Product.Price * item.Quantity;
			}
		}

		public async Task<IActionResult> OnPostUpdateAsync(int id)
		{
			int updatedQuantity = Convert.ToInt32(Request.Form["Quantity"]);
			ApplicationUser user = await _userManager.GetUserAsync(User);
			CartItems cartItem = await _shop.GetCartItemByProductIdForUserAsync(user.Id, id);

			cartItem.Quantity = updatedQuantity;
			await _shop.UpdateCartItemsAsync(cartItem);

			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);
			await _shop.RemoveCartItemsAsync(user.Id, id);

			return RedirectToPage();
		}
	}
}
