using Microsoft.EntityFrameworkCore;
using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.Handlers;

public static class ProductHandler
{

    /// <summary>
    /// Should be called when a user returns a product. It sets the return date in the lending details database.
    /// </summary>
    /// <param name="prodId">Product Id</param>
    /// <param name="userId">User Id, optional.</param>
    /// <returns>Returns true if everything went ok. False with an error otherwise.</returns>
    public static bool ReturnProduct(int prodId, int userId = 0)
    {
        userId = (userId == 0) ? AccountHandler.CurrentIdLoggedIn : userId;
        var listOfDetails = EntityFramework.Read.ReadHandler.GetLendingDetails();

        using (NewtonLibraryContext db = new())
        {
            foreach (var item in listOfDetails)
                if (item.ProductId == prodId && item.UserId == userId)
                {
                    item.ReturnDate = DateTime.Now;
                    db.LendingDetails.Attach(item);
                    db.Entry(item).State = EntityState.Modified;
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
    /// <summary>
    /// Deletes a product from the database, based on the product ID. Returns true if successful, false otherwise.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Should be called when a product is borrowed. Creates a lending detail based on user and product ID, with the
    /// current date and time. Returns true if all is ok, false otherwise.
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Creates a reservation detail based on user and product ID. Returns true if all is ok, throws exception and
    /// returns false otherwise.
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Gets all products in the database, along with their languages, categories, authors, and types.
    /// </summary>
    /// <returns>Returns a List<DisplayProductModel> with all products in the database.</returns>

    public static List<DisplayProductModel> ListAllProducts()
    {
        using (var db = new NewtonLibraryContext())
        {
            var queryable = from product in db.Products
                            join ad in db.AuthorDetails on product.Id equals ad.ProductId
                            join language in db.Languages on product.LanguageId equals language.Id
                            join category in db.Categories on product.CategoryId equals category.Id
                            join author in db.Authors on ad.AuthorId equals author.Id
                            join type in db.Types on product.ProductType equals type.Id
                            select new DisplayProductModel
                            {
                                Id = product.Id,
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
            queryable = queryable.OrderBy(p => p.Category).ThenBy(p => p.Language).ThenBy(p => p.LastName).ThenBy(p => p.FirstName);

            return queryable.ToList();
        }
    }

    /// <summary>
    /// Creates a model for a display product on the front end. The model can be used as a normal class.
    /// </summary>
    /// <param name="urlId"></param>
    /// <returns></returns>
    public static List<DisplayProductModel> ShowProduct(string urlId)
    {
        using (var db = new NewtonLibraryContext())
        {
            var id = int.Parse(urlId);
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

    /// <summary>
    /// Returns a product ID based off of a product ISBN. Checks for nulls before returning.
    /// </summary>
    /// <param name="isbn"></param>
    /// <returns>Id of the product. Returns 0 if the product doesn't exist.</returns>
    public static int GetProductIdFromIsbn(string isbn)
    {
        var list = EntityFramework.Read.ReadHandler.GetProducts()
            .Where(x => x.Isbn == isbn).ToList();
        var prod = list.FirstOrDefault();

        if (prod != null)
            return prod.Id;

        Console.WriteLine("Did not find that isbn in the database.");
        return 0;
    }

    /// <summary>
    /// Check if a LendingDetail exist between a specific user and a specific product
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="productId"></param>
    /// <returns>True if a LendingDetail exists, False if not.</returns>
    public static bool HasLendingDetail(int userId, int productId)
    {
        var list = EntityFramework.Read.ReadHandler.GetLendingDetails()
            .Where(ld => (ld.UserId == userId && ld.ProductId == productId))
            .ToList();

        return (list.Count > 0);
    }

    /// <summary>
    /// Adds a Product/Author/AuthorDetail if it doesnt exist already.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="author"></param>
    /// <param name="authorDetail"></param>
    /// <returns></returns>
    public static bool InsertProduct(Product product, Author author, AuthorDetail authorDetail)
    {
        if (!AccountHandler.AdminLoggedIn)
            return false;

        var productList = EntityFramework.Read.ReadHandler.GetProducts().Where(x => x.Isbn == product.Isbn).ToList();
            if (productList.Count > 1)
                return false;

            //If everything is ok, proceed with create!
        EntityFramework.Create.CreateHandler.CreateProduct(product.Title, product.LanguageId, product.CategoryId, product.NrOfCopies,
            product.Dewey, product.Description, product.Isbn, product.ProductType);


        var authorList = EntityFramework.Read.ReadHandler.GetAuthors().Where(x => x.LastName == author.LastName && x.FirstName == author.FirstName).ToList();
        if (authorList.Count > 0)
        {
            author.FirstName = authorList.FirstOrDefault()?.FirstName;
            author.LastName = authorList.FirstOrDefault()?.LastName;
        }
        else
        {
            EntityFramework.Create.CreateHandler.CreateAuthor(author.FirstName, author.LastName);
            EntityFramework.Create.CreateHandler.CreateAuthorDetail(authorDetail.AuthorId, authorDetail.ProductId);
        }
        return true;
    }
}