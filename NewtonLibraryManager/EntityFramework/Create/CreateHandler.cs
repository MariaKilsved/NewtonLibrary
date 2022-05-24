using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.EntityFramework.Create;
/// <summary>
/// Create handler for Entity Framework
/// </summary>
public static class CreateHandler
{
    public static bool CreateProduct(string title, int langId, int catId, 
        int nrOfCopies, decimal dewey, string desc, string isbn, int productType)
    {
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
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return false;
        }
    }
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
    
    public static bool CreateAuthor(string firstName, string lastName)
    {
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
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
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
}