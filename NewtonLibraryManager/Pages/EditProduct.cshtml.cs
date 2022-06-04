using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace NewtonLibraryManager.Pages
{
    public class EditProductModel : PageModel
    {
        [BindProperty]
        public Models.DisplayProductModel Product { get; set; }

        [BindProperty]
        public List<Models.DisplayProductModel> ProductList { get; set; }

        //To properly check for input lengths, separate properties are used

        [BindProperty, Required, MinLength(4), MaxLength(100)]
        public string Title { get; set; }

        [BindProperty, Required]
        public string Dewey { get; set; }                               //Due to issues with how numbers input behave, this is from a text input

        [BindProperty]
        public string DeweyError { get; set; }                          //Will print out an error if Dewey has the wrong format

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
        public List<SelectListItem> Authors { get; set; }               //Used to make the author dropdown(s) (select)

        [BindProperty, Required]
        public string SelectedCategory { get; set; }                    //The chosen option of the category dropdown

        [BindProperty, Required]
        public string SelectedProdType { get; set; }                    //The chosen option of the product type dropdown

        [BindProperty]
        public List<string> AuthorFirstNames { get; set; }              //The chosen author first names from all the author text input(s)

        [BindProperty]
        public List<string> AuthorLastNames { get; set; }               //The chosen author last names from all the author text input(s)

        [BindProperty]
        public List<string> SelectedAuthorNames { get; set; }           //The chosen authors from all the author dropdown(s)

        [BindProperty]
        public List<Models.Language> Languages { get; set; }            //Used to make checkboxes from available languages

        [BindProperty, Required]
        public int LanguageId { get; set; }                             //Chosen language Id from checkboxes

        private List<Models.Type> ProductTypes { get; set; }            //All product types in database, later converted to SelectListItem for dropdown

        private List<Models.Category> ProductCategories { get; set; }   //All product categories in database, later converted to SelectListItem for dropdown

        private List<Models.Author> AllAuthorsList { get; set; }        //All authors in database, later converted to SelectListItem for dropdown

        [BindProperty]
        public int AuthorCount { get; set; }

        [BindProperty]
        public bool ShowModal { get; set; }

        [BindProperty]
        public string ModalBody { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        /// <summary>
        /// When the page is loaded
        /// </summary>
        /// <param name="id">The id of the specific product being displayed</param>
        /// <param name="showModal">Whether to show a modal or not</param>
        /// <param name="modalBody">What to put in the body of the modal</param>

        public void OnGet(string id, bool showModal = false, string modalBody = "")
        {
            //Uses the product Id determined in the Url to set everything
            Id = id;

            //Get product
            ProductList = Handlers.ProductHandler.ShowProduct(Id);
            Product = ProductList[0] ?? new Models.DisplayProductModel();

            //Set AuthorsList in Product
            foreach (var i in ProductList)
            {
                Product.AuthorsList = new List<string>();

                if (Product.LastName != null && Product.FirstName != null)
                {
                    Product.AuthorsList.Add($"{Product.LastName}, {Product.FirstName}");
                }
                else if (Product.LastName != null)
                {
                    Product.AuthorsList.Add($"{Product.LastName}");
                }
                else if (Product.FirstName != null)
                {
                    Product.AuthorsList.Add($"{Product.FirstName}");
                }
                else
                {
                    Product.AuthorsList.Add("");
                }
            }

            //Set AuthorCount
            AuthorCount = Product.AuthorsList.Count();

            //Create the Lists to receive new author input
            AuthorFirstNames = new List<string>();
            AuthorLastNames = new List<string>();
            SelectedAuthorNames = new List<string>();


            //Add to the lists which will take input from frontend
            for (int i = 0; i < AuthorCount; i++)
            {
                AuthorFirstNames.Add("");
                AuthorLastNames.Add("");
                SelectedAuthorNames.Add("");
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
            Categories = ProductCategories.OrderBy(pc => pc.Category1).Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Category1
            }).ToList();

            //Create new list of SelectListItem from authors; necessary for dropdown menu
            Authors = AllAuthorsList.OrderBy(author => author.LastName).Select(a =>
            new SelectListItem
            {
                Value = a.FirstName + "*" + a.LastName,
                Text = a.LastName + ", " + a.FirstName
            }).ToList();

            //Show modal if necessary
            ShowModal = showModal;
            ModalBody = modalBody;
        }

        public IActionResult OnPost()
        {
            //Remove hyphens from ISBN
            Isbn = Isbn.Replace("-", "");

            //Convert Dewey from string to decimal (can't use input type=number due to bug in Razor Pages):
            Dewey = Dewey.Replace('.', ',');
            bool s = Decimal.TryParse(Dewey, out decimal DeweyDecimal);
            if (!s)
            {
                DeweyError = "M" + '\x00E5' + "ste vara ett nummer mellan 0 och 999.999";
                return Page();
            }

            //Round Dewey
            DeweyDecimal = Math.Round(DeweyDecimal, 3, MidpointRounding.ToZero);

            //Create the product and set the values
            var product = new Models.Product()
            {
                Title = Title,
                LanguageId = LanguageId,
                CategoryId = Int32.Parse(SelectedCategory),
                NrOfCopies = NrOfCopies,
                Dewey = DeweyDecimal,
                Description = Description,
                Isbn = Isbn,
                ProductType = Int32.Parse(SelectedProdType)
            };

            //Create list of authors
            var newAuthors = new List<Models.Author>();

            //Loop through authors list. Using SelectedAuthorNames instead of AuthorCount in case of page reloads
            for (int i = 0; i < SelectedAuthorNames.Count; i++)
            {
                //If there is something in the text input fields, use those instead of the select
                if (!String.IsNullOrWhiteSpace(AuthorFirstNames[i]) && !String.IsNullOrWhiteSpace(AuthorLastNames[i]))
                {
                    //Remove commas just in case
                    AuthorFirstNames[i].Replace(",", "");
                    AuthorLastNames[i].Replace(",", "");

                    //Add new author to list
                    newAuthors.Add(new Models.Author { FirstName = AuthorFirstNames[i], LastName = AuthorLastNames[i] });
                }
                else
                {
                    var subs = SelectedAuthorNames[i].Split("*");

                    newAuthors.Add(new Models.Author() { FirstName = subs[0], LastName = subs[1] });
                }
            }

            try
            {
                Handlers.ProductHandler.UpdateProduct(product, newAuthors);
                return RedirectToPage("/ProductView", new { id = Id, showModal = true, modalBody = "Produkt uppdaterad" });
            }
            catch (Exception)
            {
                return RedirectToPage("/EditProduct", new { id = Id, showModal = true, modalBody = "Misslyckades med att uppdatera produkt" });
            }
        }
    }
}
