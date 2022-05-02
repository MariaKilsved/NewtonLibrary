using NewtonLibraryManager.Models;

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
    }
}
