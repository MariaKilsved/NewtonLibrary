using NewtonLibraryManager.Models;
using System.Linq;

namespace NewtonLibraryManager.Handlers;

public class ProductHandler
{
    public static bool ReturnProduct(string isbn)
    {
        using (var db = new NewtonLibraryContext())
        {
            var groupedLending = from product in db.Products
                join lendingDetail in db.LendingDetails on product.Id equals lendingDetail.UserId
                where product.Isbn.Contains(isbn)
                select product.Isbn;

        }

        return false;
    }
}