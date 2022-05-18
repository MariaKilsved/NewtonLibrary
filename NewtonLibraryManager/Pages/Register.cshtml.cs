using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace NewtonLibraryManager.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty, MaxLength(30), Required]
        public string FirstName { get; set; }

        [BindProperty, MaxLength(30), Required]
        public string LastName { get; set; }

        [BindProperty, MaxLength(90), Required]
        public string EMail { get; set; }

        [BindProperty, Required]
        public string Password { get; set; }

        [BindProperty]
        public bool IsAdmin { get; set; }

        public string PasswordError { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            //Regex
            if(!Handlers.AccountHandler.ValidatePassword(Password, out string passwordError))
            {
                PasswordError = passwordError;
                return Page();
            }


            Password = Models.SecurePasswordHasher.Hash(Password);
            //Console.WriteLine(Password);

            if (ModelState.IsValid == false)
                return Page();

            if(IsAdmin)
                if (Handlers.AccountHandler.CreateAdmin(FirstName, LastName, EMail, Password))
                    return RedirectToPage("/Login");
                else
                    Console.WriteLine("Admin failed");
            
            if(Handlers.AccountHandler.CreateUser(FirstName,LastName, EMail,Password))
                    return RedirectToPage("/Login");
            
            Console.WriteLine("Failed to register");
            return Page();
        }
    }
}
