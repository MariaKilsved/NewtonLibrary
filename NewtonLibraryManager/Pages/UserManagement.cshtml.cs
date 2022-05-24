using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class UserManagementModel : PageModel
    {
        [BindProperty]
        public List<Models.User> Users { get; set; }

        /// <summary>
        /// On page loading
        /// </summary>
        public void OnGet()
        {
            //Get all users, with the admins appearing first
            Users = Handlers.UserHandler.GetUsers().OrderByDescending(x => x.IsAdmin).ToList();
        }
    }
}
