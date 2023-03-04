using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegefood.Models;
using Vegefood.Models.Interfaces;

namespace Vegefood.Pages
{
    public class ShopModel : PageModel
    {
        private readonly IInventory _context;
        public ShopModel(IInventory context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _context.GetAllInventoriesAsync();
        }
    }
}
