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

        /// <summary>
        /// On page loading
        /// </summary>
        public void OnGet()
        {
        }

        /// <summary>
        /// When submit button to register is pressed
        /// </summary>
        /// <returns>Redirect to login or reload page</returns>
        public IActionResult OnPost()
        {

            //Validate password with regex
            if(!Handlers.AccountHandler.ValidatePassword(Password, out string passwordError))
            {
                PasswordError = passwordError;
                return Page();
            }

            //Hash password
            Password = Models.SecurePasswordHasher.Hash(Password);

            //Reload if empty/incorrect frontend fields
            if (ModelState.IsValid == false)
                return Page();

            //If IsAdmin checkbox is checked, attempt to create an admin
            if(IsAdmin)
                if (Handlers.AccountHandler.CreateAdmin(FirstName, LastName, EMail, Password))
                    return RedirectToPage("/Login");
            
            //If a normal user, create the user
            if(Handlers.AccountHandler.CreateUser(FirstName,LastName, EMail,Password))
                    return RedirectToPage("/Login");
            
            //Reload page if failed
            return Page();
        }
    }
}
