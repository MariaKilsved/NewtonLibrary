using NewtonLibraryManager.Models;

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

    //public static void EditProduct()

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

        var ProductList = EntityFramework.Read.ReadHandler.GetProducts().Where(x => x.Isbn == product.Isbn).ToList();
            if (ProductList.Count > 1)
                return false;

            //If everything is ok, proceed with create!
        EntityFramework.Create.CreateHandler.CreateProduct(product.Title, product.LanguageId, product.CategoryId, product.NrOfCopies,
            product.Dewey, product.Description, product.Isbn, product.ProductType);


        var authorList = EntityFramework.Read.ReadHandler.GetAuthors().Where(x => x.LastName == author.LastName && x.FirstName == author.FirstName).ToList();
        if (authorList.Count > 0)
        {
            author.FirstName = authorList.FirstOrDefault().FirstName;
            author.LastName = authorList.FirstOrDefault().LastName;
        }
        else
        {
            EntityFramework.Create.CreateHandler.CreateAuthor(author.FirstName, author.LastName);
            EntityFramework.Create.CreateHandler.CreateAuthorDetail(authorDetail.AuthorId, authorDetail.ProductId);
        }
        return true;
    }
}