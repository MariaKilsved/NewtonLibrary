using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewtonLibraryManager.Handlers;
using System.ComponentModel.DataAnnotations;

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


        //Detta kommer köras när man trycker på submit, d.v.s. användaren har skrivit in saker
        public IActionResult OnPost()
        {
            //Om det är något fel på det som skrivits in laddas sidan bara om
            if (ModelState.IsValid == false)
                return Page();

            if (AccountHandler.LogIn(EMail, Password))
                return RedirectToPage("/Index");

            if (AccountHandler.LogIn(EMail, Password))
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(1)
                };
                Response.Cookies.Append("LibraryCookie", $"{AccountHandler.CurrentIdLoggedIn}", cookieOptions);
                return RedirectToPage("/ProductSearch");
            }

            return Page();
            //Kontrollera vad som hämtats från frontend.
            //Frontend gör antingen register eller login.
            //Login: User.EMail, Password
            //Register: User.FirstName, User.LastName, User.EMail, Password

            //Bör hash:a Password innan det läggs i User.Password

            //Bör testa om något av fälten på fronten var IsNullOrWhitespace
            //Om något var null bör sidan också laddas om d.v.s. Return Page();

            //Gå till annan sida
            //Kommer använda webbsession senare
        }

    }
}