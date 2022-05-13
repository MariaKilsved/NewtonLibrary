using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class AdminManagementModel : PageModel
    {
        [BindProperty]
        public List<Models.User> Admins { get; set; }

        public void OnGet()
        {
            //Get all librarians
            Admins = Handlers.UserHandler.GetAdmins();
        }
    }
}
