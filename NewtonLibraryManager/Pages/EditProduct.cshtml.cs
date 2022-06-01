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

        [BindProperty]
        public List<SelectListItem> ProdTypes { get; set; }         //Used to make dropdown (select)

        [BindProperty]
        public List<SelectListItem> Categories { get; set; }        //Used to make dropdown (select)

        [BindProperty]
        public List<SelectListItem> Authors { get; set; }           //Used to make dropdown (select)

        [BindProperty]
        public string AuthorFirstName { get; set; }

        [BindProperty]
        public string AuthorLastName { get; set; }

        [BindProperty]
        public string SelectedCategory { get; set; }                //The chosen option of the dropdown

        [BindProperty]
        public string SelectedProdType { get; set; }                //The chosen option of the dropdown

        [BindProperty]
        public string SelectedAuthor { get; set; }                   //The chosen option of the dropdown

        [BindProperty]
        public List<Models.Language> Languages { get; set; }        //Used to make checkboxes

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        /// <summary>
        /// When the page is loaded
        /// </summary>
        /// <param name="id">The id of the specific product being displayed</param>
        public void OnGet(string id)
        {
            //Uses the product Id determined in the Url to set everything
            Id = id;

            //Get product
            ProductList = Handlers.ProductHandler.ShowProduct(Id);
            Product = ProductList[0];

            //Get the contents to fill up the select (dropdown) options
            List<Models.Type> ProductTypes = EntityFramework.Read.ReadHandler.GetTypes();
            List<Models.Category> ProductCategories = EntityFramework.Read.ReadHandler.GetCategories();
            List<Models.Author> AuthorList = EntityFramework.Read.ReadHandler.GetAuthors().ToList();

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
            Authors = AuthorList.OrderBy(author => author.LastName).Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.LastName + ", " + a.FirstName
            }).ToList();
        }

        public void OnPost()
        {
            Console.WriteLine("lang" + Product.Language);
            Int32.TryParse(Product.Language, out int lid);
            Int32.TryParse(SelectedCategory, out int cid);
            Int32.TryParse(SelectedProdType, out int ptype);

            var prod = EntityFramework.Read.ReadHandler.GetProducts(Int32.Parse(Id));

            prod.Title = Product.Title;
            prod.LanguageId = lid;
            prod.CategoryId = cid;
            prod.NrOfCopies = Product.NrOfCopies;
            prod.Dewey = Product.Dewey;
            prod.Description = Product.Description;
            prod.Isbn = Product.Isbn;
            prod.ProductType = ptype;

            List<Models.Author> authors = new();

            //If there is something in the text input fields, use those instead of the select
            if (!String.IsNullOrWhiteSpace(AuthorFirstName) && !String.IsNullOrWhiteSpace(AuthorLastName))
            {
                //Remove commas just in case
                AuthorFirstName.Replace(",", "");
                AuthorLastName.Replace(",", "");

                //Add new author to list
                authors.Add(new Models.Author { FirstName = AuthorFirstName, LastName = AuthorLastName});
            }
            else
            {
                var a = EntityFramework.Read.ReadHandler.GetAuthors(Int32.Parse(SelectedAuthor)) ;
                authors.Add(a);
            }

            try
            {
                Handlers.ProductHandler.updateProduct(prod, authors);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
