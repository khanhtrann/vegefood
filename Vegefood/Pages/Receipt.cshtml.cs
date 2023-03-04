using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegefood.Models.Interfaces;
using Vegefood.Models;

namespace Vegefood.Pages
{
    public class ReceiptModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrder _order;

        public ReceiptModel(UserManager<ApplicationUser> userManager, IOrder order)
        {
            _userManager = userManager;
            _order = order;
        }

        public IList<OrderItems> OrderItems { get; set; }

        /// <summary>
        /// Create a user of type ApplicationUser that gets the user that is currently signed in
        /// Get the orders for the current user by taking the user id
        /// Get all the order items by the order id
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            Order order = await _order.GetLatestOrderForUserAsync(user.Id);
            OrderItems = await _order.GetOrderItemsByOrderIdAsync(order.ID);
        }
    }
}
