using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.EntityFramework.Delete
{
    public static class DeleteHandler
    {

        public static bool DeleteAuthor(int authorId)
        {
            var listOfAuthors = Read.ReadHandler.GetAuthors();

            using (NewtonLibraryContext db = new())
            {
                foreach (var item in listOfAuthors)
                    if (item.Id == authorId)
                    {
                        db.Remove(item);
                        db.SaveChanges();
                        return true;
                    }
            }
            Console.WriteLine("DeleteAuthor: ID not found.");
            return false;
        }

        public static bool DeleteAuthorDetail(int authordetailId)
        {
            var listOfAuthorDetails = Read.ReadHandler.GetAuthorDetails();

            using (NewtonLibraryContext db = new())
            {
                foreach (var item in listOfAuthorDetails)
                    if (item.Id == authordetailId)
                    {
                        db.Remove(item);
                        db.SaveChanges();
                        return true;
                    }
            }
            Console.WriteLine("DeleteAuthorDetail: ID not found.");
            return false;
        }

        public static bool DeleteCategory(int categoryId)
        {
            var listOfCategories = Read.ReadHandler.GetCategories();

            using (NewtonLibraryContext db = new())
            {
                foreach (var item in listOfCategories)
                    if (item.Id == categoryId)
                    {
                        db.Remove(item);
                        db.SaveChanges();
                        return true;
                    }
            }
            Console.WriteLine("DeleteCategory: ID not found.");
            return false;
        }

        public static bool DeleteLanguage(int languageId)
        {
            var listOfLanguages = Read.ReadHandler.GetLanguages();

            using (NewtonLibraryContext db = new())
            {
                foreach (var item in listOfLanguages)
                    if (item.Id == languageId)
                    {
                        db.Remove(item);
                        db.SaveChanges();
                        return true;
                    }
            }
            Console.WriteLine("DeleteLanguage: ID not found.");
            return false;
        }

        public static bool DeleteLendingDetail(int detailId)
        {
            var listOfDetails = Read.ReadHandler.GetLendingDetails();

            using (NewtonLibraryContext db = new())
            {
                foreach (var item in listOfDetails)
                    if (item.Id == detailId)
                    {
                        db.Remove(item);
                        db.SaveChanges();
                        return true;
                    }
            }
            Console.WriteLine("DeleteLendingDetail: ID not found.");
            return false;
        }
        public static bool DeleteReservationDetail(int detailId)
        {
            var listOfDetails = Read.ReadHandler.GetReservationDetails();

            using (NewtonLibraryContext db = new())
            {
                foreach (var item in listOfDetails)
                    if (item.Id == detailId)
                    {
                        db.Remove(item);
                        db.SaveChanges();
                        return true;
                    }
            }
            Console.WriteLine("DeleteReservationDetail: ID not found.");
            return false;
        }

        public static bool DeleteProduct(int productId)
        {
            var listOfProducts = Read.ReadHandler.GetProducts();

            using (NewtonLibraryContext db = new())
            {
                foreach (var item in listOfProducts)
                    if (item.Id == productId)
                    {
                        db.Remove(item);
                        db.SaveChanges();
                        return true;
                    }
            }
            Console.WriteLine("DeleteProduct: ID not found.");
            return false;
        }

        public static bool DeleteType(int typeId)
        {
            var listOfTypes = Read.ReadHandler.GetTypes();

            using (NewtonLibraryContext db = new())
            {
                foreach (var item in listOfTypes)
                    if (item.Id == typeId)
                    {
                        db.Remove(item);
                        db.SaveChanges();
                        return true;
                    }
            }
            Console.WriteLine("DeleteType: ID not found.");
            return false;
        }

        public static bool DeleteUser(int userId)
        {
            var listOfUsers = Read.ReadHandler.GetUsers();

            using (NewtonLibraryContext db = new())
            {
                foreach (var item in listOfUsers)
                    if (item.Id == userId)
                    {
                        db.Remove(item);
                        db.SaveChanges();
                        return true;
                    }
            }
            Console.WriteLine("DeleteUser: ID not found.");
            return false;
        }
    }
}