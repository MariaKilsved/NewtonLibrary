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

        [BindProperty ]
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
        public string SelectedAuthor { get; set; }                //The chosen option of the dropdown

        [BindProperty]
        public string AuthorFirstName { get; set; }

        [BindProperty]
        public string AuthorLastName { get; set; }

        public List<Models.Type> ProductTypes { get; set; }

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

            ProdTypes.ForEach(x => Console.WriteLine(Int32.Parse(x.Value)));

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

            #region Validation Checks
            /*if (SelectedProdType == null)
            {
                Console.WriteLine("TYPE NULL");
            } else
            {
                Console.WriteLine("TYPE INTE NULL - " + SelectedProdType);
            }

            if (SelectedProdType == null)
            {
                Console.WriteLine("CAT NULL");
            }
            else
            {
                Console.WriteLine("CAT INTE NULL - " + SelectedCategory);
            }

            Console.WriteLine(IsSwedish);

            Console.WriteLine("Author " + SelectedAuthor);
            Console.WriteLine("First " + AuthorFirstName);
            Console.WriteLine("Last " + AuthorLastName);
            */
            #endregion

            //Remove hyphens from ISBN
            Isbn = Isbn.Replace("-", "");
            
            //Create product and set some values
            var product = new Models.Product() {
                Title = Title,
                LanguageId = IsSwedish ? 1 : 2,
                CategoryId = Int32.Parse(SelectedCategory),
                NrOfCopies = NrOfCopies,
                Dewey = Dewey,
                Description = Description,
                Isbn = Isbn,
                ProductType = Int32.Parse(SelectedProdType)
            };

            //Create new author object
            Models.Author author = new();

            //Set properties if author fields aren't empty
            if (!String.IsNullOrWhiteSpace(AuthorFirstName) || !String.IsNullOrWhiteSpace(AuthorLastName))
            {
                author.FirstName = AuthorFirstName;
                author.LastName = AuthorLastName;
            }
            else
            {
                //Split the SelectedAuthor from frontend
                int id = Int32.Parse(SelectedAuthor);
                Models.Author a = EntityFramework.Read.ReadHandler.GetAuthors(id);
                //Set properties
                author.FirstName = a.FirstName;
                author.LastName = a.LastName;
            }

            //Create and populate authorlist to be passed in InsertProduct()
            List<Models.Author> authorList = new();
            authorList.Add(author);

            //Attempt to add product
            if (Handlers.ProductHandler.InsertProduct(product, authorList))
            {
                //Should redirect to specific product? Or show confirmation message on page.
                Console.WriteLine("yes");
                return RedirectToPage("/index");
            }
            else
            {
                Console.WriteLine("no");
                return RedirectToPage("/index");
            }
        }
    }
}
