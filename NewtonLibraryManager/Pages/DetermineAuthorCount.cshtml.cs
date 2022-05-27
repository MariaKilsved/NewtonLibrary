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


        public void OnGet()
        {
            //Initialize NrOfAuthors to display dropdown menu
            NrOfAuthors = new List<SelectListItem>();
            for (int i = 1; i <= 5; i++)
            {
                NrOfAuthors.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
        }
    }
}
