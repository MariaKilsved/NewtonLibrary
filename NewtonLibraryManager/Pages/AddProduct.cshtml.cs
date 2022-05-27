using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NewtonLibraryManager.Pages
{
    public class AddProductModel : PageModel
    {
        //To properly check for input lengths, separate properties are used

        [BindProperty, Required, MinLength(4), MaxLength(100)]
        public string Title { get; set; }

        [BindProperty, Required]
        public decimal Dewey { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty, Required, MinLength(10), MaxLength(13)]
        public string Isbn { get; set; }

        [BindProperty, Required]
        public int NrOfCopies { get; set; }

        [BindProperty]
        public List<SelectListItem> ProdTypes { get; set; }             //Used to make the product type dropdown (select)

        [BindProperty]
        public List<SelectListItem> Categories { get; set; }            //Used to make the category dropdown (select)

        [BindProperty]
        public List<SelectListItem> Authors { get; set; }               //Used to make the author dropdown (select)

        [BindProperty]
        public string SelectedCategory { get; set; }                    //The chosen option of the category dropdown

        [BindProperty]
        public string SelectedProdType { get; set; }                    //The chosen option of the product type dropdown

        [BindProperty]
        public string SelectedAuthor { get; set; }                      //The chosen option of the author dropdown

        [BindProperty]
        public List<Models.DisplaySelectedAuthorModel> SelectedAuthors { get; set; }        //The chosen authors

        [BindProperty]
        public List<Models.Language> Languages { get; set; }            //Used to make checkboxes from available languages

        [BindProperty, Required]
        public int LanguageId { get; set; }                             //Chosen language Id from checkboxes

        private List<Models.Type> ProductTypes { get; set; }            //All product types in database

        private List<Models.Category> ProductCategories { get; set; }   //All product categories in database

        private List<Models.Author> AllAuthorsList { get; set; }        //All authors in database

        [BindProperty(SupportsGet = true)]
        public string AuthorCount { get; set; }

        /// <summary>
        /// When page is loaded
        /// </summary>
        public void OnGet(string authorCount)
        {
            AuthorCount = authorCount;


            //Create the SelectedAuthors list from chosen number of authors, but it's still empty
            SelectedAuthors = new List<Models.DisplaySelectedAuthorModel>();

            bool success = Int32.TryParse(AuthorCount, out int selectedNrOfAuthorsInt);

            if (success)
            {
                for (int i = 0; i < selectedNrOfAuthorsInt; i++)
                {
                    SelectedAuthors.Add(new Models.DisplaySelectedAuthorModel { Author = new Models.Author { FirstName = "", LastName = "" }, FormattedName = "" });
                }
            }
            else
            {
                Console.WriteLine("Failed to parse number of authors.");
                Console.WriteLine("SelectedNrOfAuthors: " + AuthorCount);
            }

            //Get all product types and categories to display dropdown menus
            ProductTypes = EntityFramework.Read.ReadHandler.GetTypes();
            ProductCategories = EntityFramework.Read.ReadHandler.GetCategories();
            AllAuthorsList = EntityFramework.Read.ReadHandler.GetAuthors().ToList();

            //Get the languages for checkboxes
            Languages = EntityFramework.Read.ReadHandler.GetLanguages();

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
            Authors = AllAuthorsList.OrderBy(author => author.LastName).Select(a =>
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
        public IActionResult OnPostAddProduct()
        {
            //Remove hyphens from ISBN
            Isbn = Isbn.Replace("-", "");

            //Create product and set some of the values
            var product = new Models.Product() { Title = Title, Isbn = Isbn, Description = Description, Dewey = Dewey, NrOfCopies = NrOfCopies };           

            //Turn the List<Models.DisplaySelectedAuthorModel> SelectedAuthors into a List<Models.Author>
            var newAuthors = new List<Models.Author>();

            foreach(var a in SelectedAuthors)
            {
                //Add author if named
                if (a.Author != null && !String.IsNullOrWhiteSpace(a.Author.FirstName) && !String.IsNullOrWhiteSpace(a.Author.LastName))
                {
                    //Remove commas just in case
                    a.Author.FirstName = a.Author.FirstName.Replace(",", "");
                    a.Author.LastName = a.Author.LastName.Replace(",", "");

                    newAuthors.Add(a.Author);

                    Console.WriteLine();
                    Console.WriteLine("Adding author from user input:");
                    Console.WriteLine("Author first name: " + a.Author.FirstName);
                    Console.WriteLine("Author last name: " + a.Author.LastName);
                    Console.WriteLine();
                }
                else
                {
                    //Create new author from the chosen dropdown (select) string 
                    string[] subs = a.FormattedName.Split(", ");

                    a.Author.FirstName = subs[1];
                    a.Author.LastName = subs[0];

                    newAuthors.Add(a.Author);

                    Console.WriteLine();
                    Console.WriteLine("Adding author from dropdown:");
                    Console.WriteLine("Author first name: " + a.Author.FirstName);
                    Console.WriteLine("Author last name: " + a.Author.LastName);
                    Console.WriteLine();

                }
            }
            Console.WriteLine();
            Console.WriteLine("Attempting to add product...");
            Console.WriteLine();

            //Attempt to add product
            if (Handlers.ProductHandler.InsertProduct(product, newAuthors))
            {
                //Should redirect to specific product? Or show confirmation message on page.

                //var addedProductList = EntityFramework.Read.ReadHandler.GetProducts().Where(x => x.Isbn == product.Isbn).ToList();

                return RedirectToPage("/Index");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Failed to add product!");
                Console.WriteLine("Product Title: " + product.Title);
                Console.WriteLine("Product LanguageId: " + product.LanguageId);
                Console.WriteLine("Product CategoryId: " + product.CategoryId);
                Console.WriteLine("Product NrOfCopies: " + product.NrOfCopies);
                Console.WriteLine("Product Dewey: " + product.Dewey);
                Console.WriteLine("Product Description: " + product.Description);
                Console.WriteLine("Product Isbn: " + product.Isbn);
                Console.WriteLine("Product ProductType: " + product.ProductType);
                Console.WriteLine();
                for(int i = 0; i < newAuthors.Count; i++)
                {
                    Console.WriteLine($"Author {i + 1} FirstName: {newAuthors[i].FirstName}");
                    Console.WriteLine($"Author {i + 1} LastName: {newAuthors[i].LastName}");
                    Console.WriteLine();
                }

                return RedirectToPage("/Error");
            }
        }
    }
}
