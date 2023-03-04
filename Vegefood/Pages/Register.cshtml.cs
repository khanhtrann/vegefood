using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Vegefood.Models;
using Vegefood.Models.Interfaces;
using Vegefood.ViewModels;

namespace Vegefood.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private IShop _shop;


        public IConfiguration Configuration { get; }


        [BindProperty]
        public Register Model { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IShop shop)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            Configuration = configuration;
            _shop = shop;

        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Model.Email,
                    Email = Model.Email,
                    FirstName = Model.FirstName,
                    LastName = Model.LastName,
                    Address = Model.Address,
                    City = Model.City,
                    Province = Model.Province,
                    Zip = Model.Zip
                };

                var result = await _userManager.CreateAsync(user, Model.Password);
                if (result.Succeeded)
                {
                    Claim name = new Claim("FullName", $"{Model.FirstName} {Model.LastName}");
                    Claim email = new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email);

                    List<Claim> claims = new List<Claim> { name, email };
                    await _userManager.AddClaimsAsync(user, claims);

                    string adminEmail = Configuration["ADMIN_EMAIL"];

                    if (Model.Email == adminEmail)
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
                    }

                    await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);

                    var cart = new Cart
                    {
                        UserID = user.Id
                    };

                    await _shop.CreateCartAsync(cart);

                    await _signInManager.SignInAsync(user, false);

                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
