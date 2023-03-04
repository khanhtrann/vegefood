using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegefood.Models;
using Vegefood.ViewModels;

namespace Vegefood.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public IConfiguration Configuration { get; }


        [BindProperty]
        public Login Model { get; set; }

        public LoginModel(SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            Configuration = configuration;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                var identityResult =
                    await _signInManager.PasswordSignInAsync(Model.Email, Model.Password, Model.RememberMe, false);

                if (identityResult.Succeeded)
                {
                    if (returnUrl == null || returnUrl == "/")
                    {
                        string adminEmail =
                         Configuration["ADMIN_EMAIL"];


                        if (Model.Email == adminEmail)
                        {
                            return Redirect("/Admin");
                        }
                        else
                        {
                            return Redirect("/Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                        return RedirectToPage(returnUrl);
                    }
                }

                ModelState.AddModelError("", "Incorrect username or password");


            }
            return Page();
        }
    }
}
