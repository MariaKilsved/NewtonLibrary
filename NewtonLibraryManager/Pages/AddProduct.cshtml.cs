using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace NewtonLibraryManager.Pages
{
    public class AddProductModel : PageModel
    {

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public bool IsSwedish { get; set; }

        [BindProperty]
        public bool IsEnglish { get; set; }

        [BindProperty]
        public decimal Dewey { get; set; }

        [BindProperty]
        public string? Description { get; set; }

        [BindProperty]
        public int Isbn { get; set; }


        [BindProperty]
        public List<string?>? ProdTypes { get; set; }


        [BindProperty]
        public List<string?>? Categories { get; set; }

        [BindProperty]
        public string? ProdType { get; set; }

        [BindProperty]
        public string? Category { get; set; }

        public void OnGet()
        {
            /*
            List<Models.Type> prodTypeList = EntityFramework.Read.ReadHandler.GetTypes();
            List <Models.Category> catList = EntityFramework.Read.ReadHandler.GetCategories();

            ProdTypes = prodTypeList.Select(a => a.Type1).ToList();
            Categories = catList.Select(a => a.Category1).ToList();
            */
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            //Should redirect to specific product? Or show confirmation message on page.
            return RedirectToPage("/ProductSearch");
        }
    }
}
