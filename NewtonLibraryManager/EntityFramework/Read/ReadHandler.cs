using NewtonLibraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace NewtonLibraryManager.EntityFramework.Read
{
    public static class ReadHandler
    {
        public static void Get(string command)
        {

            switch (command)
            {
                case "users":
                    GetUsers();
                    break;
                case "products":
                    GetProducts();
                    break;
                case "categories":
                    GetCategories();
                    break;
                case "languages":
                    GetLanguages();
                    break;
                case "authordetails":
                    GetAuthorDetails();
                    break;
                case "types":
                    GetTypes();
                    break;
                case "lendingdetails":
                    GetLendingDetails();
                    break;
                case "authors":
                    GetAuthors();
                    break;
                    
                default:
                    throw new ArgumentException("Wrong input");
                    

            }
        }


        // READ METHODS
        public static List<User> GetUsers()
        {
            using NewtonLibraryContext db = new();
            var Users = db.Users.ToList();
            return Users;
        }

        public static List<Product> GetProducts()
        {
            using NewtonLibraryContext db = new();
            var Products = db.Products.ToList();
            return Products;
        }

        public static List<Category> GetCategories()
        {
            using NewtonLibraryContext db = new();
            var Categories = db.Categories.ToList();
            return Categories;
        }

        public static List<Language> GetLanguages()
        {
            using NewtonLibraryContext db = new();
            var Languages = db.Languages.ToList();
            return Languages;
        }

        public static List<AuthorDetail> GetAuthorDetails()
        {
            using NewtonLibraryContext db = new();
            var AuthorDetails = db.AuthorDetails.ToList();
            return AuthorDetails;
        }

        public static List<Models.Type> GetTypes()
        {
            using NewtonLibraryContext db = new();
            var Types = db.Types.ToList();
            return Types;
        }

        public static List<LendingDetail> GetLendingDetails()
        {
            using NewtonLibraryContext db = new();
            var LendingDetails = db.LendingDetails.ToList();
            return LendingDetails;
        }

        public static List<Author> GetAuthors()
        {
            using NewtonLibraryContext db = new();
            var Authors = db.Authors.ToList();
            return Authors;
        }

        //-----------//
        //Overloads//
        //---------//

        public static Author GetAuthors(int authorId)
        {
            using NewtonLibraryContext db = new();
            Author? author = db.Authors.Find(authorId);

            if (author == null)
            {
                throw new Exception("Could not find author with that ID");
            }
            return author;
        }

        public static Category GetCategories(int categoryId)
        {
            using NewtonLibraryContext db = new();
            Category? category = db.Categories.Find(categoryId);

            if (category == null)
            {
                throw new Exception("Could not find category with that ID");
            }
            return category;
        }

        public static Product GetProducts(int productId)
        {
            using NewtonLibraryContext db = new();
            Product? product = db.Products.Find(productId);

            if (product == null)
            {
                throw new Exception("Could not find product with that ID");
            }
            return product;
        }

        public static User GetUsers(int userId)
        {
            using NewtonLibraryContext db = new();
            User? user = db.Users.Find(userId);

            if (user == null)
            {
                throw new Exception("Could not find user with that ID");
            }
            return user;
        }

        public static LendingDetail GetLendingDetails(int lendingDetailsId)
        {
            using NewtonLibraryContext db = new();
            LendingDetail? lendingDetail = db.LendingDetails.Find(lendingDetailsId);

            if (lendingDetail == null)
            {
                throw new Exception("Could not find user with that ID");
            }
            return lendingDetail;
        }
    }
}
