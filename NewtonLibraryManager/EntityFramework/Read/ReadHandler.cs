using NewtonLibraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace NewtonLibraryManager.EntityFramework.Read
{
    /// <summary>
    /// Get methods for Entity Framework
    /// </summary>
    public static class ReadHandler
    {
        // READ METHODS
        public static List<User> GetUsers()
        {
            using NewtonLibraryContext db = new();
            var users = db.Users.ToList();
            return users;
        }

        public static List<Product> GetProducts()
        {
            using NewtonLibraryContext db = new();
            var products = db.Products.ToList();
            return products;
        }

        public static List<Category> GetCategories()
        {
            using NewtonLibraryContext db = new();
            var categories = db.Categories.ToList();
            return categories;
        }

        public static List<Language> GetLanguages()
        {
            using NewtonLibraryContext db = new();
            var languages = db.Languages.ToList();
            return languages;
        }

        public static List<AuthorDetail> GetAuthorDetails()
        {
            using NewtonLibraryContext db = new();
            var authorDetails = db.AuthorDetails.ToList();
            return authorDetails;
        }

        public static List<Models.Type> GetTypes()
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                var types = db.Types.ToList();
                return types;
            }
        }

        public static List<LendingDetail> GetLendingDetails()
        {
            using NewtonLibraryContext db = new();
            var lendingDetails = db.LendingDetails.ToList();
            return lendingDetails;
        }
        public static List<ReservationDetail> GetReservationDetails()
        {
            using NewtonLibraryContext db = new();
            var reservDetails = db.ReservationDetails.ToList();
            return reservDetails;
        }

        public static List<Author> GetAuthors()
        {
            using NewtonLibraryContext db = new();
            var authors = db.Authors.ToList();
            return authors;
        }

        public static List<NECategory> GetNECategories()
        {
            using NewtonLibraryContext db = new();
            var neCategories = db.NECategories.ToList();
            return neCategories;
        }

        public static List<NewsAndEvent> GetNewsAndEvents()
        {
            using NewtonLibraryContext db = new();
            var newsAndEvents = db.NewsAndEvents.ToList();
            return newsAndEvents;
        }

        //-----------//
        //Overloads//
        //---------//

        public static Author GetAuthors(int authorId)
        {
            using NewtonLibraryContext db = new();
            Author author = db.Authors.Find(authorId);

            if (author == null)
            {
                throw new Exception("Could not find author with that ID");
            }
            return author;
        }

        public static List<AuthorDetail> GetAuthorDetails(int productId)
        {
            using NewtonLibraryContext db = new();
            List<AuthorDetail> authordetails = db.AuthorDetails.Where(x => x.ProductId == productId).ToList();

            if (authordetails == null)
            {
                throw new Exception("Could not retrieve Authordetail with provided product ID");
            }

            return authordetails;
        }

        public static Category GetCategories(int categoryId)
        {
            using NewtonLibraryContext db = new();
            Category category = db.Categories.Find(categoryId);

            if (category == null)
            {
                throw new Exception("Could not find category with that ID");
            }
            return category;
        }

        public static Product GetProducts(int productId)
        {
            using NewtonLibraryContext db = new();
            Product product = db.Products.Find(productId);

            if (product == null)
            {
                throw new Exception("Could not find product with that ID");
            }
            return product;
        }

        public static User GetUsers(int userId)
        {
            using NewtonLibraryContext db = new();
            User user = db.Users.Find(userId);

            if (user == null)
            {
                throw new Exception("Could not find user with that ID");
            }
            return user;
        }

        public static LendingDetail GetLendingDetails(int lendingDetailsId)
        {
            using NewtonLibraryContext db = new();
            LendingDetail lendingDetail = db.LendingDetails.Find(lendingDetailsId);

            if (lendingDetail == null)
            {
                throw new Exception("Could not find user with that ID");
            }
            return lendingDetail;
        }

        public static ReservationDetail GetReservationDetails(int reservationDetailId)
        {
            using NewtonLibraryContext db = new();
            ReservationDetail reservationDetail = db.ReservationDetails.Find(reservationDetailId);

            if (reservationDetail == null)
            {
                throw new Exception("Could not find reservationdetail with that ID");
            }
            return reservationDetail;
        }

        public static NewsAndEvent GetNewsAndEvents(int newsAndEventId)
        {
            using NewtonLibraryContext db = new();
            NewsAndEvent newsAndEvent = db.NewsAndEvents.Find(newsAndEventId);
            if (newsAndEvent == null)
            {
                throw new Exception("Could not find news with that ID");
            }
            return newsAndEvent;
        }
    }
}
