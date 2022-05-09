using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace NewtonLibraryManager.Pages
{
    public class RegisterModel : PageModel
    {
        /*
        //Har inget Id fr�n frontend
        [BindProperty]
        public Models.User User { get; set; }
        */

        [BindProperty, MaxLength(30), Required]
        public string FirstName { get; set; }

        [BindProperty, MaxLength(30), Required]
        public string LastName { get; set; }

        [BindProperty, MaxLength(90), Required]
        public string EMail { get; set; }

        [BindProperty, Required]
        public string Password { get; set; }   //Should be hashed before put in User

        [BindProperty]
        public bool IsAdmin { get; set; }

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

            if(IsAdmin)
                if(Handlers.AccountHandler.CreateAdmin(FirstName, LastName, EMail, Password))
                    return RedirectToPage("/Index");
                
            if(Handlers.AccountHandler.CreateUser(FirstName, LastName, EMail, Password))
                return RedirectToPage("/Index");

            return Page();

            //Antagligen ska man l�gga in registrering i databasen h�r.
            //Kontrollera vad som h�mtats fr�n frontend.
            //Frontend g�r antingen register eller login.
            //Login: User.EMail, Password
            //Register: User.FirstName, User.LastName, User.EMail, Password

            //B�r hash:a Password innan det l�ggs i User.Password

            //B�r testa om n�got av f�lten p� fronten var IsNullOrWhitespace
            //Om n�got var null b�r sidan ocks� laddas om d.v.s. Return Page();

            //G� till annan sida
            //Kommer anv�nda webbsession senare
        }
    }
}
