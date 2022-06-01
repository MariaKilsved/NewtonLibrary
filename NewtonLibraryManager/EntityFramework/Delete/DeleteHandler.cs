using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.EntityFramework.Delete
{
    /// <summary>
    /// Delete handler for Entity Framework
    /// </summary>
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

        public static int DeleteAuthorDetail(int productId)
        {
            List<AuthorDetail> authorDetails = Read.ReadHandler.GetAuthorDetails(productId);

            using (NewtonLibraryContext db = new())
            {
                authorDetails.ForEach(authorDetail =>
                {
                    db.Remove(authorDetail);
                });

                try
                {
                    return db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not remove authordetails from database", ex);
                }
            }
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

        public static int DeleteProduct(int productId)
        {
            Product prod = Read.ReadHandler.GetProducts(productId);

            using (NewtonLibraryContext db = new())
            {
                try
                {
                    db.Remove(prod);
                    return db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not remove item from database", ex);
                }
            }
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

        public static bool DeleteNewsEvent(int NewsEventId)
        {
            var listOfNewsEvents = Read.ReadHandler.GetNewsAndEvents();

            using (NewtonLibraryContext db = new())
            {
                foreach (var item in listOfNewsEvents)
                    if (item.Id == NewsEventId)
                    {
                        db.Remove(item);
                        db.SaveChanges();
                        return true;
                    }
            }
            return false;

        }
    }
}