using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegefood.Models.Interfaces;
using Vegefood.Models;
using Vegefood.ViewModels;

namespace Vegefood.Pages
{
    public class SingleProductModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInventory _inventory;
        private readonly IShop _shop;

        public SingleProductModel(IInventory inventory, IShop shop, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _inventory = inventory;
            _shop = shop;
        }

        /// <summary>
        /// A property to be available on the Model property in the Razor Page
        /// </summary>
        public Product ProductDetail { get; set; }

        [BindProperty]
        public ProductInput Input { get; set; }

        public async Task OnGetAsync(int id)
        {
            ProductDetail = await _inventory.GetInventoryByIdAsync(id);
        }

        /// <summary>
        /// Create cart items by pulling data information from Product and Cart tables
        /// If the product already exists then call update operation instead of get operation
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Redirect to /Shop/Cart page</returns>
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = await _shop.GetCartByUserIdAsync(user.Id);
            if (ModelState.IsValid)
            {
                CartItems cartItem = new CartItems
                {
                    CartID = cart.ID,
                    ProductID = id,
                    Quantity = Input.Quantity
                };
                if (await _shop.GetCartItemByProductIdForUserAsync(user.Id, id) != null)
                {
                    CartItems existingCartItem = await _shop.GetCartItemByProductIdForUserAsync(user.Id, id);
                    existingCartItem.Quantity += Input.Quantity;
                    await _shop.UpdateCartItemsAsync(existingCartItem);
                }
                else
                {
                    await _shop.CreateCartItemAsync(cartItem);
                }
            }
            return Redirect("/Cart");
        }
    }
}
