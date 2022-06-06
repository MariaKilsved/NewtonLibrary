using NewtonLibraryManager.Models;
using System.Linq;

namespace NewtonLibraryManager.Handlers
{
    public static class SearchHandler
    {
        /// <summary>
        /// Method for the search function. Creates a DisplayProduct class from the database based on what the user
        /// searched for. Checks ISBN, Author, Title. Returns a list of these classes, ready to be displayed.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static List<DisplayProductModel> ProductSearch(string search)
        {
            List<DisplayProductModel> displayProductModels = new();

            using (var db = new NewtonLibraryContext())
            {

                var queryable = from product in db.Products
                                join ad in db.AuthorDetails on product.Id equals ad.ProductId
                                join language in db.Languages on product.LanguageId equals language.Id
                                join category in db.Categories on product.CategoryId equals category.Id
                                join author in db.Authors on ad.AuthorId equals author.Id
                                join type in db.Types on product.ProductType equals type.Id
                                where product.Isbn.Contains(search) ||
                                      author.FirstName.Contains(search) ||
                                      author.LastName.Contains(search) ||
                                      product.Title.Contains(search)
                                select new
                                {
                                    ID = product.Id,
                                    TITLE = product.Title,
                                    LANG = language.Language1,
                                    CAT = category.Category1,
                                    DEW = product.Dewey,
                                    DESC = product.Description,
                                    ISBN = product.Isbn,
                                    FIRSTNAME = author.FirstName,
                                    LASTNAME = author.LastName,
                                    PRODTYPE = type.Type1
                                };

                foreach (var item in queryable)
                {
                    var copy = false;
                    foreach (var displayItem in displayProductModels)
                        if (displayItem.Title == item.TITLE)
                            copy = true;
                    
                    if (copy)
                        continue;
                    
                    DisplayProductModel displayProductModel = new();
                    displayProductModel.Id = item.ID;
                    displayProductModel.Category = item.CAT;
                    displayProductModel.Description = item.DESC;
                    displayProductModel.Dewey = item.DEW;
                    displayProductModel.Language = item.LANG;
                    displayProductModel.Title = item.TITLE;
                    displayProductModel.FirstName = item.FIRSTNAME;
                    displayProductModel.LastName = item.LASTNAME;
                    displayProductModel.ProductType = item.PRODTYPE;
                    displayProductModel.Isbn = item.ISBN;
                    displayProductModels.Add(displayProductModel);
                }
                return displayProductModels;
            }
        }
    }
}
