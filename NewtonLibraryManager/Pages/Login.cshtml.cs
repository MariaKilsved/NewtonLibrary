using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewtonLibraryManager.Handlers;
using System.ComponentModel.DataAnnotations;
using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty, MaxLength(90)]
        public string EMail { get; set; }

        [BindProperty]
        public string Password { get; set; }   //Should be hashed before put in User


        public void OnGet()
        {
            //User = new Models.User { IsAdmin = false };
        }


        //Detta kommer k�ras n�r man trycker p� submit, d.v.s. anv�ndaren har skrivit in saker
        public IActionResult OnPost()
        {
            var hashedPass = Models.SecurePasswordHasher.Hash(Password);
            //Console.WriteLine(hashedPass);
            //Om det �r n�got fel p� det som skrivits in laddas sidan bara om
            if (ModelState.IsValid == false)
                return Page();

            if (AccountHandler.LogIn(EMail, hashedPass))
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(1)
                };

                Response.Cookies.Append("LibraryCookie", Models.SecurePasswordHasher.Hash($"NewtonLibraryManager_{AccountHandler.CurrentIdLoggedIn}"), cookieOptions);
                Response.Cookies.Append("LibraryCookie1", Models.SecurePasswordHasher.Hash($"NewtonLibraryManager_{AccountHandler.AdminLoggedIn}"), cookieOptions);
                Response.Cookies.Append("LibraryCookie2", $"{AccountHandler.CurrentIdLoggedIn}", cookieOptions);

                return RedirectToPage("/Index");
            }

            return Page();
        }

    }
}