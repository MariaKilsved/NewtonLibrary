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

        [BindProperty]
        public string ReturnsToStock { get; set; }

        [BindProperty]
        public bool HasLendingDetail { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }


        /// <summary>
        /// When the page is loaded
        /// </summary>
        /// <param name="id">The id of the specific product being displayed</param>
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

            //Obtain information on when book returns to stock
            ReturnsToStock = Handlers.InventoryHandler.ReturnsToStock(Int32.Parse(id))?.ToString("MM/dd/yyyy") ?? "-";

            //Check cookies
            var cookieValue = Request.Cookies["LibraryCookie"];
            var cookieValue2 = Request.Cookies["LibraryCookie2"];
            var cookieComparer = Models.SecurePasswordHasher.Hash("NewtonLibraryManager_" + cookieValue2);

            if (cookieValue != null && cookieValue2 != null && cookieValue == cookieComparer)
            {
                //Set HasLendingDetail to true only if a matching LendingDetail exists
                HasLendingDetail = Handlers.ProductHandler.HasLendingDetail(Int32.Parse(cookieValue2), Int32.Parse(id));
            }
            else
            {
                HasLendingDetail = false;
            }
        }


        /// <summary>
        /// When the submit button to borrow is pressed
        /// </summary>
        /// <returns>Redirect to a index or reload same page</returns>
        public IActionResult OnPostBorrow()
        {
            //Compare cookies
            string cookieValue = Request.Cookies["LibraryCookie"];
            string cookieValue2 = Request.Cookies["LibraryCookie2"];

            //Validation using cookies. Cookies are saved as strings and must be converted to int.
            if (cookieValue != null && cookieValue2 != null && Models.SecurePasswordHasher.Hash("NewtonLibraryManager_" + cookieValue2) == cookieValue)
            {
                int userId = Int32.Parse(cookieValue2);
                int prodId = Int32.Parse(Id);

                //Attempt to reserve product
                if (Handlers.ProductHandler.BorrowProduct(userId, prodId))
                {
                    return RedirectToPage("/Index");

                }
            }
            return Page();
        }
        

        /// <summary>
        /// When the submit button to reserve is pressed
        /// </summary>
        /// <returns>Redirect to index or reload page.</returns>
        public IActionResult OnPostReserve()
        {
            //Compare cookies
            string cookieValue = Request.Cookies["LibraryCookie"];
            string cookieValue2 = Request.Cookies["LibraryCookie2"];

            //Validation using cookies. Cookies are saved as strings and must be converted to int.
            if (cookieValue != null && cookieValue2 != null && Models.SecurePasswordHasher.Hash("NewtonLibraryManager_" + cookieValue2) == cookieValue)
            {
                int userId = Int32.Parse(cookieValue2);
                int prodId = Int32.Parse(Id);

                //Attempt to reserve product
                if (Handlers.ProductHandler.ReserveProduct(userId, prodId))
                {
                    return RedirectToPage("/Index");

                }
                //Console.WriteLine("User: " + userId);
                //Console.WriteLine("Product: " + prodId);
            }
			//Console.WriteLine("Cookie: " + cookieValue);
			//Console.WriteLine("Cookie 2: " + cookieValue2);
            return Page();
        }

        /// <summary>
        /// When the submit button to return a product is pressed
        /// </summary>
        /// <returns>Redirect to index or reload page.</returns>
        public IActionResult OnPostReturn()
        {
            //Compare cookies
            string cookieValue = Request.Cookies["LibraryCookie"];
            string cookieValue2 = Request.Cookies["LibraryCookie2"];
            string cookieComparer = Models.SecurePasswordHasher.Hash("NewtonLibraryManager_" + cookieValue2);

            //Validation using cookies. Cookies are saved as strings and must be converted to int.
            if (cookieValue != null && cookieValue2 != null && cookieComparer == cookieValue)
            {
                //int userId = Int32.Parse(cookieValue2);
                int prodId = Int32.Parse(Id);

                //Attempt to return product
                if (Handlers.ProductHandler.ReturnProduct(prodId))
                {
                    return RedirectToPage("/Index");
                }
            }
            return Page();
        }

        /// <summary>
        /// When the submit button to delete a product is pressed. Only available to admins (librarians).
        /// </summary>
        /// <returns>Redirect to index or reload page.</returns>
        public IActionResult OnPostDelete()
        {
            //Compare cookies
            string cookieValue = Request.Cookies["LibraryCookie1"];
            string cookieComparerTrue = Models.SecurePasswordHasher.Hash("NewtonLibraryManager_True");

            //Validation using cookies. Cookies are saved as strings and must be converted to int.
            if (cookieValue != null && cookieComparerTrue == cookieValue)
            {
                //Attempt to delete product
                if (Handlers.ProductHandler.DeleteProduct(Int32.Parse(Id)))
                {
                    return RedirectToPage("/Index");
                }
            }
            return Page();
        }
    }
}
