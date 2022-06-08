using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.EntityFramework.Create;
/// <summary>
/// Create handler for Entity Framework
/// </summary>
public static class CreateHandler
{
    /// <summary>
    /// Creates a product and inserts it to the databse
    /// </summary>
    /// <param name="title"></param>
    /// <param name="langId"></param>
    /// <param name="catId"></param>
    /// <param name="nrOfCopies"></param>
    /// <param name="dewey"></param>
    /// <param name="desc"></param>
    /// <param name="isbn"></param>
    /// <param name="productType"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int CreateProduct(string title, int langId, int catId, 
        int nrOfCopies, decimal dewey, string desc, string isbn, int productType)
    {
        int prodId;
        try
        {
            using (NewtonLibraryContext db = new())
            {
                var product = new Product
                {
                    Title = title,
                    LanguageId = langId,
                    CategoryId = catId,
                    NrOfCopies = nrOfCopies,
                    Dewey = dewey,
                    Description = desc,
                    Isbn = isbn,
                    ProductType = productType,
                };
                db.Add(product);
                db.SaveChanges();
                prodId = product.Id;
            }
            return prodId;
        }
        catch (Exception)
        {
            throw new Exception("Could not add product");
        }
    }
    /// <summary>
    /// Creates a lending detail and inserts it to the database
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    public static bool CreateLendingDetail(int userId, DateTime from, DateTime to, int productId)
    {
        try
        {
            using (NewtonLibraryContext db = new())
            {
                var lendingDetail = new LendingDetail
                {
                    UserId = userId,
                    BorrowedFrom = from,
                    BorrowedTo = to,
                    ProductId = productId
                };
                db.Add(lendingDetail);
                db.SaveChanges();
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
    /// <summary>
    /// Creates a reservation detail and inserts it to the database.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="reservDate"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    public static bool CreateReservationDetail(int userId, DateTime reservDate , int productId)
    {
        try
        {
            using (NewtonLibraryContext db = new())
            {
                var reservationDetail = new ReservationDetail
                {
                    UserId = userId,
                    ReservationDate = DateTime.Now,
                    ProductId = productId
                };
                db.Add(reservationDetail);
                db.SaveChanges();
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
    /// <summary>
    /// Creates an author detail and inserts it to the database
    /// </summary>
    /// <param name="authorId"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    public static bool CreateAuthorDetail(int authorId, int productId)
    {
        try
        {
            using (NewtonLibraryContext db = new())
            {
                var authorDetail = new AuthorDetail
                {
                    AuthorId = authorId,
                    ProductId = productId
                };
                db.Add(authorDetail);
                db.SaveChanges();
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
    
    /// <summary>
    /// Creates an author and inserts it to the database
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int CreateAuthor(string firstName, string lastName)
    {
        int authorId;
        try
        {
            using (NewtonLibraryContext db = new())
            {
                var author = new Author
                {
                    FirstName = firstName,
                    LastName = lastName
                };
                db.Add(author);
                db.SaveChanges();
                authorId = author.Id;
            }
            return authorId;
        }
        catch (Exception)
        {
            throw new Exception("Could not add author");
        }
    }
    /// <summary>
    /// Creates a product type and inserts it to the database
    /// </summary>
    /// <param name="tp"></param>
    /// <returns></returns>
    public static bool CreateType(string tp)
    {
        try
        {
            using (NewtonLibraryContext db = new())
            {
                var type = new Models.Type
                {
                    Type1 = tp
                };
                db.Add(type);
                db.SaveChanges();
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return false;
        }
    }
    /// <summary>
    /// Creates a product language and inserts it to the database
    /// </summary>
    /// <param name="lang"></param>
    /// <returns></returns>
    public static int CreateLanguage(string lang)
    {
        using (NewtonLibraryContext db = new())
        {
            var language = new Language
            {
                Language1 = lang
            };
            db.Add(language);
            return db.SaveChanges();
        }
    }

    /// <summary>
    /// Creates a product category and inserts it to the database
    /// </summary>
    /// <param name="cat"></param>
    /// <returns></returns>
    public static bool CreateCategory(string cat)
    {
        try
        {
            using (NewtonLibraryContext db = new())
            {
                var category = new Category
                {
                    Category1 = cat
                };
                db.Add(category);
                db.SaveChanges();
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return false;
        }
    }

    /// <summary>
    /// Creates a user and inserts it to the database
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="isAdmin"></param>
    /// <returns></returns>
    public static bool CreateUser(string firstName, string lastName, string email, string password, bool isAdmin)
    {
        try
        {
            using (NewtonLibraryContext db = new())
            {
                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EMail = email,
                    Password = password,
                    IsAdmin = isAdmin
                };
                db.Add(user);
                db.SaveChanges();
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return false;
        }
    }

    /// <summary>
    /// Creates a newevent and inserts it to the database
    /// </summary>
    /// <param name="newsEvent"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool CreateNewsEvent(NewsAndEvent newsEvent)
    {
        Console.WriteLine();
        Console.WriteLine("---CreateNewsEvent---");
        Console.WriteLine("newsEvent.CategoryId: " + newsEvent.CategoryId);
        Console.WriteLine("newsEvent.Title: " + newsEvent.Title);
        Console.WriteLine("newsEvent.ContentText: " + newsEvent.ContentText);
        Console.WriteLine("newsEvent.PublishedDate: " + newsEvent.PublishedDate);
        Console.WriteLine();

        var db = new NewtonLibraryContext();
        db.Add(newsEvent);
        try
        {
            db.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            throw new Exception("Could not add the News Event to the database");
        }
    }
}