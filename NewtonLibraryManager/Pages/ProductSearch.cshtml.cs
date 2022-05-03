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
        public Models.Author Author { get; set; }       //Kan bli null n�r det h�mtas fr�n frontend, eller f� null properties
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
            //Om det �r n�got fel p� det som skrivits in laddas sidan bara om
            if (ModelState.IsValid == false)
            {
                return Page();
            }


            //B�r h�mta ut produkten fr�n databasen h�r.



            //Redirect:a till viss produkts sida ist�llet, eller visa produkt p� sidan
            return RedirectToPage("/ProductSearch");
        }
    }
}
