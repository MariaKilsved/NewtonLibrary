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
                                select new DisplayProductModel
                                {
                                    Id = product.Id,
                                    Title = product.Title,
                                    Language = language.Language1,
                                    Category = category.Category1,
                                    Dewey = product.Dewey,
                                    Description = product.Description,
                                    Isbn = product.Isbn,
                                    FirstName = author.FirstName,
                                    LastName = author.LastName,
                                    ProductType = type.Type1
                                };

                var testList = queryable.ToList();
                Console.WriteLine("testList: " + testList[0].Title);

                //GroupBy to account for multiple elements in queryable having the same ID
                var groupedList = queryable.ToList().GroupBy(x => x.Id, x => x);

                foreach (var i in groupedList)
                {
                    var item = i.ToList();

                    DisplayProductModel displayProductModel = item[0];

                    Console.WriteLine("displayProductModel: " + displayProductModel.Title);

                    displayProductModel.AuthorsList = new List<string>();

                    foreach (var itemCopy in item)
                    {
                        if (itemCopy.LastName != null && itemCopy.FirstName != null)
                        {
                            displayProductModel.AuthorsList.Add($"{itemCopy.LastName}, {itemCopy.FirstName}");
                        }
                        else if (itemCopy.LastName != null)
                        {
                            displayProductModel.AuthorsList.Add($"{itemCopy.LastName}");
                        }
                        else if (itemCopy.FirstName != null)
                        {
                            displayProductModel.AuthorsList.Add($"{itemCopy.FirstName}");
                        }
                        else
                        {
                            displayProductModel.AuthorsList.Add("");
                        }
                    }

                    displayProductModels.Add(displayProductModel);
                }
                return displayProductModels;
            }
        }
    }
}
