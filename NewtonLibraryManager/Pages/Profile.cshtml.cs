using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class ProfileModel : PageModel
    {

        [BindProperty]
        public Models.User User { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public void OnGet(string id)
        {
            //Uses the user Id determined in the Url to set everything
            Id = id;

            /*
            List<Models.DisplayProductModel> productList = Handlers.ProductHandler.ShowProduct(Id);
            Product = productList[0];

            AuthorNames = new List<string>();

            foreach (var prod in productList)
            {
                AuthorNames.Add(prod?.FirstName + " " + prod?.LastName);
            }
            */
        }
    }
}
