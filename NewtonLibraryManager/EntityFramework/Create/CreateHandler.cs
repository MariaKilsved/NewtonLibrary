using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.EntityFramework.Create;


public static class CreateHandler
{
    public static int CreateProduct(string title, int langId, int catId, 
        int nrOfCopies, decimal dewey, string desc, string isbn, int productType)
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
            return db.SaveChanges();
        }
    }
    public static int CreateLendingDetail(int userId, DateTime from, DateTime to, bool isReservation, int productId)
    {
        using (NewtonLibraryContext db = new())
        {
            var lendingDetail = new LendingDetail
            {
                UserId = userId,
                BorrowedFrom = from,
                BorrowedTo = to,
                IsReservation = isReservation,
                ProductId = productId
            };
            db.Add(lendingDetail);
            return db.SaveChanges();
        }
    }
    public static int CreateAuthorDetail(int authorId, int productId)
    {
        using (NewtonLibraryContext db = new())
        {
            var authorDetail = new AuthorDetail
            {
                AuthorId = authorId,
                ProductId = productId
            };
            db.Add(authorDetail);
            return db.SaveChanges();
        }
    }
    
    public static int CreateAuthor(string firstName, string lastName)
    {
        using (NewtonLibraryContext db = new())
        {
            var author = new Author
            {
                FirstName = firstName,
                LastName = lastName
            };
            db.Add(author);
            return db.SaveChanges();
        }
    }
    public static int CreateType(string tp)
    {
        using (NewtonLibraryContext db = new())
        {
            var type = new Models.Type
            {
                Type1 = tp
            };
            db.Add(type);
            return db.SaveChanges();
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

    public static int CreateCategory(string cat)
    {
        using (NewtonLibraryContext db = new())
        {
            var category = new Category
            {
                Category1 = cat
            };
            db.Add(category);
            return db.SaveChanges();
        }
    }

    public static int CreateUser(string firstName, string lastName, string email, string password, bool isAdmin)
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
            return db.SaveChanges();
        }
    }
}