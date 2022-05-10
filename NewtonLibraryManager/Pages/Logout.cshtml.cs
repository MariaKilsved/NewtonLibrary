using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            Response.Cookies.Delete("LibraryCookie");
            Response.Cookies.Delete("LibraryCookie1");
        }
    }
}
