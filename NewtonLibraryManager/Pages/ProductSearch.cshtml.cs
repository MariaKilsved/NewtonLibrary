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
            //Om det är något fel på det som skrivits in laddas sidan bara om
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            Search = Search?.Replace("-", "");

            if(String.IsNullOrWhiteSpace(Search))
            {
                return Page();
            }

            //List of the ids of all selected products
            List<int> selectedIdsList;


            using (var context = new Models.NewtonLibraryContext())
            {
                var selectedIds = from product in context.Products
                                       join ad in context.AuthorDetails on product.Id equals ad.ProductId
                                       join author in context.Authors on ad.AuthorId equals author.Id
                                       join type in context.Types on product.ProductType equals type.Id
                                       where product.Isbn == Search || author.FirstName == Search || author.LastName == Search || author.FirstName + " " + author.LastName == Search && ((IncludeBooks? type.Id == 1 : type.Id != 1) && (IncludeEbooks? type.Id == 2 : type.Id != 2) && (IncludeAudio? type.Id == 3 : type.Id != 3))
                                       select product.Id;
                selectedIdsList = selectedIds.ToList();
            }

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
