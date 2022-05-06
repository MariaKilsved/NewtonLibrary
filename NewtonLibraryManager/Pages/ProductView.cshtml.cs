using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class ProductViewModel : PageModel
    {

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Author { get; set; }

        [BindProperty]
        public string Language { get; set; }

        [BindProperty]
        public string Category { get; set; }

        [BindProperty]
        public int NrOfCopies { get; set; }

        [BindProperty]
        public string Dewey { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public string Isbn { get; set; }

        [BindProperty]
        public string ProductType { get; set; }

        public void OnGet()
        {
            string path = Request.Path;
            string[] subPaths = path.Split('/');
            string id = subPaths[1];    // ProductView/id?query

            //Should get whatever product info there is
            //Uses the product Id determined in the Url to set everything
        }
    }
}
