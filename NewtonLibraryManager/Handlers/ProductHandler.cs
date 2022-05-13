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
    public static bool DeleteProduct(int id)
    {
        if (AccountHandler.AdminLoggedIn)
        {
            EntityFramework.Delete.DeleteHandler.DeleteProduct(id);
            return true;
        }
        Console.WriteLine("Admin not logged in");
        return false;
    }

    public static bool BorrowProduct(int userid, int productId)
    {
        try
        {
            EntityFramework.Create.CreateHandler.CreateLendingDetail(userid, DateTime.Now, DateTime.Now.AddMonths(1), productId);
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
            EntityFramework.Create.CreateHandler.CreateReservationDetail(userid, DateTime.Now, productId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    public static List<DisplayProductModel> ShowProduct(string UrlId)
    {
        using (var db = new NewtonLibraryContext())
        {
            int id = Int32.Parse(UrlId);
            var queryable = from product in db.Products
                            join ad in db.AuthorDetails on product.Id equals ad.ProductId
                            join language in db.Languages on product.LanguageId equals language.Id
                            join category in db.Categories on product.CategoryId equals category.Id
                            join author in db.Authors on ad.AuthorId equals author.Id
                            join type in db.Types on product.ProductType equals type.Id
                            where product.Id == id
                            select new DisplayProductModel
                            {
                                Id = id,
                                Title = product.Title,
                                FirstName = author.FirstName,
                                LastName = author.LastName,
                                Language = language.Language1,
                                Category = category.Category1,
                                NrOfCopies = product.NrOfCopies,
                                Dewey = product.Dewey,
                                Description = product.Description,
                                Isbn = product.Isbn,
                                ProductType = type.Type1

                            };
            
            return queryable.ToList();
        }
    }
}