using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegefood.Models;
using Vegefood.Models.Interfaces;

namespace Vegefood.Pages
{
    public class OrderModel : PageModel
    {
        private readonly IOrder _order;
        public OrderModel(IOrder order)
        {
            _order = order;
        }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderItems> OrderItems { get; set; }

        /// <summary>
        /// Get all the orders from the order table
        /// Get all the order items from the each order
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            Orders = await _order.GetOrdersAsync();
            OrderItems = await _order.GetOrderItemsAsync();
        }
    }
}
