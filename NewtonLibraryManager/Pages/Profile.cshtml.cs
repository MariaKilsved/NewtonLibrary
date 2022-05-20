using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class ProfileModel : PageModel
    {

        [BindProperty]
        public Models.User User1 { get; set; }

        [BindProperty]
        public Models.User EditedUser { get; set; }

        [BindProperty]
        public string PasswordError { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }


        public void OnGet(string id)
        {
            //Uses the user Id determined in the Url to set everything
            Id = id;

            //Needs to be User1 instead of User to avoid hiding PageModel.User
            User1 = Handlers.UserHandler.GetUsers(Int32.Parse(id));
            EditedUser = new Models.User() { Id = User1.Id, EMail = User1.EMail, FirstName = User1.FirstName, LastName = User1.LastName, IsAdmin = User1.IsAdmin, Password = User1.Password };
        }

        public IActionResult OnPostEdit()
        {
            if (ModelState.IsValid == false || String.IsNullOrWhiteSpace(EditedUser.Password))
            {
                PasswordError = "Ange lösenord";
                return Page();
            }
            else
            {
                //Regex
                if (!Handlers.AccountHandler.ValidatePassword(EditedUser.Password, out string passwordError))
                {
                    PasswordError = passwordError;
                    return Page();
                }
                //Hashing
                else
                {
                    EditedUser.Password = Models.SecurePasswordHasher.Hash(EditedUser.Password);
                }
            }

            if (EditedUser.IsAdmin == null)
            {
                EditedUser.IsAdmin = false;
            }

            int rows = EntityFramework.Update.UpdateHandler.UpdateUser(EditedUser.Id, EditedUser);
            if (rows > 0)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            string selectedIdHash = Models.SecurePasswordHasher.Hash("NewtonLibraryManager_" + id.ToString());

            if (selectedIdHash != Models.SecurePasswordHasher.Hash("NewtonLibraryManager_1") && Handlers.AccountHandler.DeleteUser(id))
            {

                if (Request.Cookies["LibraryCookie"] == selectedIdHash)
                {
                    Response.Cookies.Delete("LibraryCookie");
                    Response.Cookies.Delete("LibraryCookie1");
                    Response.Cookies.Delete("LibraryCookie2");
                    return RedirectToPage("/Logout");
                }
                return RedirectToPage("/Index");
            }
            else
            {
                //Maybe return error page?
                return Page();
            }
        }
    }
}
