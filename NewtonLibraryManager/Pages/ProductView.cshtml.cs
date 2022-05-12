using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class ProductViewModel : PageModel
    {
        [BindProperty]
        public Models.DisplayProductModel Product { get; set; }

        [BindProperty]
        public List<string> AuthorNames { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }


        public void OnGet(string id)
        {
            //Uses the product Id determined in the Url to set everything
            Id = id;

            List<Models.DisplayProductModel> productList = Handlers.ProductHandler.ShowProduct(Id);
            Product = productList[0];

            AuthorNames = new List<string>();

            foreach(var prod in productList)
            {
                AuthorNames.Add(prod?.FirstName + " " + prod?.LastName);
            }

        }

        public IActionResult OnPostReserve()
        {
            string cookieValue = Request.Cookies["LibraryCookie"];
            int userId = Int32.Parse(cookieValue);
            int prodId = Int32.Parse(Id);

            if (Handlers.ProductHandler.ReserveProduct(userId, prodId))
            {
                return RedirectToPage("/Index");

            }
            else
            {
                return Page();
            }
        }
        
        /*
        public IActionResult OnPostBorrow()
        {
            string cookieValue = Request.Cookies["LibraryCookie"];
            int userId = Int32.Parse(cookieValue);
            int prodId = Int32.Parse(Id);

            if(Handlers.ProductHandler.BorrowProduct(userId, prodId))
            {
                return RedirectToPage("/Index");

            }
            else
            {
                return Page();
            }

        }
        */
    }
}
