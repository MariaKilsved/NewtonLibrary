using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace NewtonLibraryManager.Pages
{
    public class ProductSearchModel : PageModel
    {
        [BindProperty]
        public string? Search { get; set; }

        [BindProperty]
        public bool IncludeBooks { get; set; }

        [BindProperty]
        public bool IncludeEbooks { get; set; }

        [BindProperty]
        public bool IncludeAudio { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            //Om det �r n�got fel p� det som skrivits in laddas sidan bara om
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            Search = Search?.Replace("-", "");

            //List of the ids of all selected products

            /*
            string query = "?";
            foreach(var id in selectedIdsList)
            {
                query += id.ToString() + "&";
            }
            query = query.Remove(query.Length - 1);
            */
            return RedirectToPage("/ProductSearch");

        }
    }
}
