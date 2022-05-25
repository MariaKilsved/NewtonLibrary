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

        [BindProperty]
        public string AuthorFirstName { get; set; }

        [BindProperty]
        public string AuthorLastName { get; set; }

        private List<Models.Type> ProductTypes { get; set; }

        private List<Models.Category> ProductCategories { get; set; }

        private List<Models.Author> AuthorList { get; set; }

        /// <summary>
        /// When page is loaded
        /// </summary>
        public void OnGet()
        {
            #region TESTDATA FOR CREATING A PRODUCT/AUTHOR/AUTHORDETAIL WITH MULTIPLE AUTHORS
            //var product1 = new Models.Product() { Title = "jajamen", LanguageId = 1, CategoryId = 1, NrOfCopies = 1, Dewey = 0.6M, Description = "jajamen",
            //Isbn = "123456791", ProductType = 1};

            //var Author1 = new Models.Author() { FirstName = "Fredrik", LastName = "Wiman" };
            //var Author2 = new Models.Author() { FirstName = "Pontus", LastName = "Hedman" };
            
            //List<Models.Author> authors = new();

            //authors.Add(Author1);
            //authors.Add(Author2);

            //Handlers.ProductHandler.InsertProduct(product1, authors);
            #endregion

            //Get all product types and categories to display dropdown menus
            ProductTypes = EntityFramework.Read.ReadHandler.GetTypes();
            ProductCategories = EntityFramework.Read.ReadHandler.GetCategories();
            AuthorList = EntityFramework.Read.ReadHandler.GetAuthors().ToList();

            //Create new list of SelectListItem from product types; necessary for dropdown menu
            ProdTypes = ProductTypes.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Type1
            }).ToList();

            //Create new list of SelectListItem from product categories; necessary for dropdown menu
            Categories = ProductCategories.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Category1
            }).ToList();

            //Create new list of SelectListItem from authors; necessary for dropdown menu
            Authors = AuthorList.OrderBy(author => author.LastName).Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.LastName + ", " + a.FirstName
            }).ToList();

        }

        /// <summary>
        /// Submitting the new product
        /// </summary>
        /// <returns>Redirect to page</returns>
        public IActionResult OnPost()
        {
            //Reload page if invalid inputs
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            //Remove hyphens from ISBN
            Isbn = Isbn.Replace("-", "");

            //Create product and set some values
            var product = new Models.Product() { Title = Title, Isbn = Isbn, Description = Description, Dewey = Dewey, NrOfCopies = NrOfCopies };           

            //Set selected product type, chosen from dropdown
            foreach (var pt in ProductTypes)
            {
                if(pt.Type1 == SelectedProdType)
                {
                    product.ProductType = pt.Id;
                    break;
                }
            }

            //Set selected product category, chosen from dropdown
            foreach(var cat in ProductCategories)
            {
                if(cat.Category1 == SelectedCategory)
                {
                    product.CategoryId = cat.Id;
                    break;
                }
            }

            //Set language as Swedish if checkbox is checked
            if(IsSwedish)
            {
                product.LanguageId = 1;
            }
            //Set language as English if checkbox is checked
            if(IsEnglish)
            {
                product.LanguageId = 2;
            }

            //Create new author object
            var author = new Models.Author();

            //Set properties if author fields aren't empty
            if (!String.IsNullOrWhiteSpace(AuthorFirstName) || !String.IsNullOrWhiteSpace(AuthorLastName))
            {
                author.FirstName = AuthorFirstName;
                author.LastName = AuthorLastName;
            }
            else
            {
                //Split the SelectedAuthor from frontend
                string[] subs = SelectedAuthor.Split(", ");

                //Set properties
                author.FirstName = subs[1];
                author.LastName = subs[0];
            }

            //Attempt to add product
            /*
            if (Handlers.ProductHandler.AddProduct(Title, languageId, categoryId, NrOfCopies, Dewey, Description, Isbn, productTypeId))
            {
                //Should redirect to specific product? Or show confirmation message on page.
                return RedirectToPage("/ProductSearch");
            }
            else
            {
                return RedirectToPage("/ProductSearch");
            }
            */
            return RedirectToPage("/ProductSearch");


        }
    }
}
