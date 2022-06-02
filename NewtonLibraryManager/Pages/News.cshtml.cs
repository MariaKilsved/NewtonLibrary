using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class NewsModel : PageModel
    {
        [BindProperty]
        public List<Models.NewsAndEvent> News { get; set; }


        public void OnGet()
        {
            //Get News
            News = EntityFramework.Read.ReadHandler.GetNewsAndEvents();

            //Reverse news to get latest first
            News.Reverse();
        }
    }
}
