using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegefood.Models;
using Vegefood.Models.Interfaces;

namespace Vegefood.Pages
{
    public class OrderDetailModel : PageModel
    {
        private readonly IOrder _order;

        public OrderDetailModel(IOrder order)
        {
            _order = order;
        }
        // Get the Order Items from the database and store in OrderItems with a data type of IEnumerable<OrderItems>
        public IEnumerable<OrderItems> OrderItems { get; set; }

        /// <summary>
        /// Get all the order items details by the order id that is selected
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task OnGetAsync(int id)
        {
            OrderItems = await _order.GetOrderItemsByOrderIdAsync(id);
        }
    }
}
