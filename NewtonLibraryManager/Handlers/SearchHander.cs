using NewtonLibraryManager.Models;
using System.Linq;

namespace NewtonLibraryManager.Handlers
{
    public class SearchHander
    {
        public static void BookSearch(string search)
        {
            List<int> selectedIdsList;

            using (var context = new Models.NewtonLibraryContext())
            {

                var selectedIds = from product in context.Products
                                  join ad in context.AuthorDetails on product.Id equals ad.ProductId
                                  join author in context.Authors on ad.AuthorId equals author.Id
                                  join type in context.Types on product.ProductType equals type.Id
                                  where product.Isbn.Contains(search) ||
                                  author.FirstName.Contains(search) ||
                                  author.LastName.Contains(search) ||
                                  product.Title.Contains(search)
                                  select product.Id;
                selectedIdsList = selectedIds.ToList();
            }
        }
    }
}
