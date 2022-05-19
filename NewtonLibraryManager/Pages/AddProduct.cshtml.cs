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

        [BindProperty, Required, MinLength(10), MaxLength(13)]
        public string Isbn { get; set; }

        [BindProperty]
        public int NrOfCopies { get; set; }

        [BindProperty]
        public List<SelectListItem> ProdTypes { get; set; }         //Used to make dropdown (select)

        [BindProperty]
        public List<SelectListItem> Categories { get; set; }        //Used to make dropdown (select)

        [BindProperty]
        public List<SelectListItem> Authors { get; set; }           //Used to make dropdown (select)

        [BindProperty]
        public string SelectedCategory { get; set; }                //The chosen option of the dropdown

        [BindProperty]
        public string SelectedProdType { get; set; }                //The chosen option of the dropdown

        [BindProperty]
        public string SelectedAuthor { get; set; }                   //The chosen option of the dropdown

        private List<Models.Type> ProductTypes { get; set; }

        private List<Models.Category> ProductCategories { get; set; }

        private List<Models.Author> AuthorList { get; set; }

        public void OnGet()
        {
            //Get all product types and categories to display dropdown menus
            ProductTypes = EntityFramework.Read.ReadHandler.GetTypes();
            ProductCategories = EntityFramework.Read.ReadHandler.GetCategories();
            AuthorList = EntityFramework.Read.ReadHandler.GetAuthors().ToList();

            ProdTypes = ProductTypes.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Type1
            }).ToList();

            Categories = ProductCategories.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Category1
            }).ToList();

            Authors = AuthorList.OrderBy(author => author.LastName).Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.LastName + ", " + a.FirstName
            }).ToList();

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            int languageId = 1;
            int categoryId = 1;
            int productTypeId = 1;

            Isbn = Isbn.Replace("-", "");

            foreach (var prod in ProductTypes)
            {
                if(prod.Type1 == SelectedProdType)
                {
                    productTypeId = prod.Id;
                    break;
                }
            }

            foreach(var cat in ProductCategories)
            {
                if(cat.Category1 == SelectedCategory)
                {
                    categoryId = cat.Id;
                    break;
                }
            }

            if(IsSwedish)
            {
                languageId = 1;
            }
            if(IsEnglish)
            {
                languageId = 2;
            }

            if(Handlers.ProductHandler.AddProduct(Title, languageId, categoryId, NrOfCopies, Dewey, Description, Isbn, productTypeId))
            {
                //Should redirect to specific product? Or show confirmation message on page.
                return RedirectToPage("/ProductSearch");
            }
            else
            {
                return RedirectToPage("/ProductSearch");
            }

        }
    }
}
