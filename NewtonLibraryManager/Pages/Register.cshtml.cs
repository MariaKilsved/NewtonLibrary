using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class RegisterModel : PageModel
    {
        /*
        //Har inget Id fr�n frontend
        [BindProperty]
        public Models.User User { get; set; }
        */

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string EMail { get; set; }

        [BindProperty]
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

            EntityFramework.Create.CreateHandler.CreateUser(FirstName, LastName, EMail, Password, false);
            
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
            return RedirectToPage("/Index");
        }
    }
}
