using NewtonLibraryManager.Models;
using System.Linq;

namespace NewtonLibraryManager.Handlers
{
    public class SearchHandler
    {
        
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
                        TITLE = product.Title,
                        ISBN = product.Isbn,
                        FIRSTNAME = author.FirstName,
                        LASTNAME = author.LastName,
                    };

                foreach (var item in queryable)
                {
                    DisplayProductModel displayProductModel = new();
                    displayProductModel.Title = item.TITLE;
                    displayProductModel.FirstName = item.FIRSTNAME;
                    displayProductModel.LastName = item.LASTNAME;
                    displayProductModel.ISBN = item.ISBN;
                    displayProductModels.Add(displayProductModel);
                }

                return displayProductModels;
            }
        }
    }
    }
