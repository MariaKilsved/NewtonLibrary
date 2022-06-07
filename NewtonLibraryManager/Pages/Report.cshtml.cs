using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class ReportModel : PageModel
    {
        [BindProperty]
        public List<Models.DisplayHotBook> HotBooks { get; set; }

        public void OnGet()
        {
            HotBooks = Handlers.StatisticHandler.HotProducts();
        }
    }
}
