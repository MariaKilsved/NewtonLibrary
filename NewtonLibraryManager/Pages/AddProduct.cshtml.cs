using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NewtonLibraryManager.Pages
{
    public class AddProductModel : PageModel
    {

        [BindProperty, Required, MinLength(4), MaxLength(100)]
        public string Title { get; set; }

        [BindProperty]
        public bool IsSwedish { get; set; }

        [BindProperty]
        public bool IsEnglish { get; set; }

        [BindProperty, Required]
        public decimal Dewey { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty, Required]
        public int Isbn { get; set; }

        [BindProperty]
        public List<SelectListItem> ProdTypes { get; set; }         //Used to make dropdown (select)

        [BindProperty]
        public List<SelectListItem> Categories { get; set; }        ////Used to make dropdown (select)

        [BindProperty]
        public string SelectedCategory { get; set; }                //The chosen option of the dropdown

        [BindProperty]
        public string SelectedProdType { get; set; }                //The chosen option of the dropdown

        public void OnGet()
        {

            List<Models.Type> prodTypeList = EntityFramework.Read.ReadHandler.GetTypes();
            List <Models.Category> catList = EntityFramework.Read.ReadHandler.GetCategories();

            ProdTypes = prodTypeList.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Type1
            }).ToList();

            Categories = catList.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Category1
            }).ToList();
            
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
