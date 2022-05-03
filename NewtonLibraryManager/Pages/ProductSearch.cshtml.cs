using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class ProductSearchModel : PageModel
    {
        [BindProperty]
        public string Isbn { get; set; }

        /*
        [BindProperty]
        public Models.Author Author { get; set; }       //Kan bli null när det hämtas från frontend, eller få null properties
        */

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

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


            //Bör hämta ut produkten från databasen här.



            //Redirect:a till viss produkts sida istället, eller visa produkt på sidan
            return RedirectToPage("/ProductSearch");
        }
    }
}
