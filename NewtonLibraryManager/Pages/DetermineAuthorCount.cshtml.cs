using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace NewtonLibraryManager.Pages
{
    public class DetermineAuthorCountModel : PageModel
    {
        [BindProperty]
        public List<SelectListItem> NrOfAuthors { get; set; }           //Used to make the number of authors dropdown (select)


        [BindProperty]
        public string SelectedNrOfAuthors { get; set; }                 //The chosen option of the number of authors dropdown


        /// <summary>
        /// When page is loaded
        /// </summary>
        public void OnGet()
        {
            //Set default value
            SelectedNrOfAuthors = "1";

            //Initialize NrOfAuthors to display dropdown menu
            NrOfAuthors = new List<SelectListItem>();
            for (int i = 1; i <= 5; i++)
            {
                NrOfAuthors.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
        }

        /// <summary>
        /// When the button is pressed
        /// </summary>
        /// <returns>Redirect to AddProduct with the SelectedNrOfAuthors as parameter</returns>
        public IActionResult OnPost()
        {
            return RedirectToPage("/AddProduct", new { nr = SelectedNrOfAuthors });
        }

    }
}
