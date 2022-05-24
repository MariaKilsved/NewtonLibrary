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


        /// <summary>
        /// When page is loaded
        /// </summary>
        public void OnGet()
        {
            //User = new Models.User { IsAdmin = false };
        }

        /// <summary>
        /// When a new user is submitted
        /// </summary>
        /// <returns>Redirect to a the same page</returns>
        public IActionResult OnPost()
        {
            var hashedPass = Models.SecurePasswordHasher.Hash(Password);
            //Console.WriteLine(hashedPass);
            //If any frontend field is incorrect, the page will reload
            if (ModelState.IsValid == false)
                return Page();

            //Attempt to log in
            if (AccountHandler.LogIn(EMail, hashedPass))
            {
                //Set cookie expiration time
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(1)
                };

                //Append cookies
                Response.Cookies.Append("LibraryCookie", Models.SecurePasswordHasher.Hash($"NewtonLibraryManager_{AccountHandler.CurrentIdLoggedIn}"), cookieOptions);
                Response.Cookies.Append("LibraryCookie1", Models.SecurePasswordHasher.Hash($"NewtonLibraryManager_{AccountHandler.AdminLoggedIn}"), cookieOptions);
                Response.Cookies.Append("LibraryCookie2", $"{AccountHandler.CurrentIdLoggedIn}", cookieOptions);

                return RedirectToPage("/Index");
            }

            //Reload page if login failed 
            return Page();
        }

    }
}