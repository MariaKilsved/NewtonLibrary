using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class LogoutModel : PageModel
    {
        /// <summary>
        /// When page is loaded
        /// </summary>
        public void OnGet()
        {
            //Log out backend
            Handlers.AccountHandler.LogOut();

            //Delete cookies
            Response.Cookies.Delete("LibraryCookie");
            Response.Cookies.Delete("LibraryCookie1");
            Response.Cookies.Delete("LibraryCookie2");


        }
    }
}
