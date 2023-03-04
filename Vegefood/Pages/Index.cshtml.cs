using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegefood.Models;
using Vegefood.Models.Interfaces;

namespace Vegefood.Pages
{
	public class IndexModel : PageModel
	{
		private readonly IInventory _context;
		public IndexModel(IInventory context)
		{
			_context = context;
		}
		public IList<Product> FeaturedProducts { get; set; }

		public async Task OnGetAsync()
		{
			FeaturedProducts = await _context.GetFeaturedInventoriesAsync();
		}
	}
}