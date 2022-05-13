using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class ProfileModel : PageModel
    {

        [BindProperty]
        public Models.User User1 { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public void OnGet(string id)
        {
            //Uses the user Id determined in the Url to set everything
            Id = id;

            //Needs to be User1 instead of User to avoid hiding PageModel.User
            User1 = Handlers.UserHandler.GetUsers(Int32.Parse(id));
        }

        public IActionResult OnPostDelete(int id)
        {
            if(id != 1 && Handlers.AccountHandler.DeleteUser(id))
            {
                int cookieId = Int32.Parse(Request.Cookies["LibraryCookie"]);

                if (cookieId == id)
                {
                    Response.Cookies.Delete("LibraryCookie");
                    Response.Cookies.Delete("LibraryCookie1");
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
