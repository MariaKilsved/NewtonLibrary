using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class UserManagementModel : PageModel
    {
        [BindProperty]
        public List<Models.User> Users { get; set; }

        [BindProperty]
        public string MostActiveUser { get; set; }

        /// <summary>
        /// On page loading
        /// </summary>
        public void OnGet()
        {
            //Get all users, with the admins appearing first
            Users = Handlers.UserHandler.GetUsers().OrderByDescending(x => x.IsAdmin).ToList();

            //Get the most active user, who has the most entries in LendingDetails
            int activeUserId = Handlers.AggregationHandler.GetMostActiveBorrower();
            var activeUser = Handlers.UserHandler.GetUser(activeUserId);

            if(activeUser != null)
            {
                MostActiveUser = $"{activeUser.FirstName} {activeUser.LastName}";
            }
        }
    }
}
