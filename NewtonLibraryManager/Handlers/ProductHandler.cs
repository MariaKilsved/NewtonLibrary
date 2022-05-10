using NewtonLibraryManager.Models;
using System.Linq;

namespace NewtonLibraryManager.Handlers;

public class ProductHandler
{
    public static bool ReturnProduct(int prodId)
    {
        var listOfDetails = EntityFramework.Read.ReadHandler.GetLendingDetails();

        using (NewtonLibraryContext db = new())
        {
            foreach (var item in listOfDetails)
             if (item.ProductId == prodId && item.UserId == AccountHandler.CurrentIdLoggedIn)
             {
                 item.ReturnDate = DateTime.Now;
                 db.SaveChanges();
                 return true;
             }
        }

        Console.WriteLine("Did not find relevant information in the database.");
        return false;
    }
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

    public static bool BorrowProduct(int userid, int productId)
    {
        try
        {
            EntityFramework.Create.CreateHandler.CreateLendingDetail(userid, DateTime.Now, DateTime.Now.AddMonths(1), false, productId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
    public static bool ReserveProduct(int userid, int productId)
    {
        try
        {
            EntityFramework.Create.CreateHandler.CreateLendingDetail(userid, DateTime.Now, DateTime.Now.AddMonths(1), true, productId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}