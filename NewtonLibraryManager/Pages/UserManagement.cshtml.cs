using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class UserManagementModel : PageModel
    {
        [BindProperty]
        public List<Models.User> Users { get; set; }

        public void OnGet()
        {
            Users = Handlers.UserHandler.GetUsers().OrderByDescending(x => x.IsAdmin).ToList();
        }
    }
}
