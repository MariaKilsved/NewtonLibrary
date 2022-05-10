using NewtonLibraryManager.Models;
using System.Linq;

namespace NewtonLibraryManager.Handlers;

public class ProductHandler
{
    /*
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
    */
    public static bool AddProduct(string title, int languageId, int categoryId, int nrOfCopies,
        decimal dewey, string description, string isbn, int productType)
    {
        if (AccountHandler.AdminLoggedIn)
        {
            EntityFramework.Create.CreateHandler.CreateProduct(title, languageId, categoryId, nrOfCopies, dewey,
                description, isbn, productType);
            return true;
        }
        Console.WriteLine("Admin not logged in");
        return false;
    }

    public static bool BorrowProduct(int userid, bool reserved, int productId)
    {
        try
        {
            EntityFramework.Create.CreateHandler.CreateLendingDetail(userid, DateTime.Now, DateTime.Now.AddMonths(1), reserved, productId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}