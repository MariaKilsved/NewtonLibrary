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

        [BindProperty] 
        public int LendedOut { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }


        public void OnGet(string id)
        {
            //Uses the product Id determined in the Url to set everything
            Id = id;

            //Get product
            List<Models.DisplayProductModel> productList = Handlers.ProductHandler.ShowProduct(Id);
            Product = productList[0];

            //Merge additional author names
            AuthorNames = new List<string>();

            foreach(var prod in productList)
            {
                AuthorNames.Add(prod?.FirstName + " " + prod?.LastName);
            }

            //Obtain inventory status
            LendedOut = Handlers.InventoryHandler.GetNrOfBorrowedFromProductId(Int32.Parse(id));

        }

        public IActionResult OnPostReserve()
        {
            //Compare cookies
            string cookieValue1 = Request.Cookies["LibraryCookie"];
            string cookieValue2 = Request.Cookies["LibraryCookie2"];

            if (cookieValue1 != null && cookieValue2 != null && Models.SecurePasswordHasher.Hash(cookieValue2) == cookieValue1)
            {
                int userId = Int32.Parse(cookieValue2);
                int prodId = Int32.Parse(Id);

                //Attempt to reserve product
                if (Handlers.ProductHandler.ReserveProduct(userId, prodId))
                {
                    return RedirectToPage("/Index");

                }
            }
            
            return Page();
        }
    }
}
