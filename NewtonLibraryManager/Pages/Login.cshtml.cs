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


        //Detta kommer k�ras n�r man trycker p� submit, d.v.s. anv�ndaren har skrivit in saker
        public IActionResult OnPost()
        {
            //Om det �r n�got fel p� det som skrivits in laddas sidan bara om
            if (ModelState.IsValid == false)
                return Page();

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
            //Kontrollera vad som h�mtats fr�n frontend.
            //Frontend g�r antingen register eller login.
            //Login: User.EMail, Password
            //Register: User.FirstName, User.LastName, User.EMail, Password

            //B�r hash:a Password innan det läggs i User.Password

            //B�r testa om n�got av f�lten p� fronten var IsNullOrWhitespace
            //Om n�got var null b�r sidan ocks� laddas om d.v.s. Return Page();

            //G� till annan sida
            //Kommer anv�nda webbsession senare
        }

    }
}