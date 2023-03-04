using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegefood.Models.Interfaces;
using Vegefood.Models;

namespace Vegefood.Pages
{
    public class MyOrderDetailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IOrder _order;

        /// <summary>
        /// A constructor that brings in UserManager<ApplicationUser> and IOrder interface to enable the implementation of show the order details that contains only the user's orders
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="order"></param>
        public MyOrderDetailModel(UserManager<ApplicationUser> userManager, IOrder order)
        {
            _userManager = userManager;
            _order = order;
        }

        /// <summary>
        /// A property to be available on the Model property in the Razor Page
        /// </summary>
        public IEnumerable<OrderItems> OrderItems { get; set; }

        /// <summary>
        /// Get all the order items by sending over the order id that is requested
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task OnGetAsync(int id)
        {
            OrderItems = await _order.GetOrderItemsByOrderIdAsync(id);
        }
    }
}
