using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.EntityFramework.Delete
{
    public static class DeleteHandler
    {


        public static int DeleteAuthor(List<Author> authorID, int id)
        {
            using (NewtonLibraryContext db = new())
            {
                foreach (var item in authorID)
                    if (id == item.Id)
                        db.Remove(item);
                return db.SaveChanges();
            }
        }

        public static int DeleteAuthorDetail(List<AuthorDetail> authordetailID, int id)
        {
            using (NewtonLibraryContext db = new())
            {
                foreach (var item in authordetailID)
                    if (id == item.Id)
                        db.Remove(item);
                return db.SaveChanges();
            }
        }

        public static int DeleteCategory(List<Category> categoryID, int id)
        {
            using (NewtonLibraryContext db = new())
            {
                foreach (var item in categoryID)
                    if (id == item.Id)
                        db.Remove(item);
                return db.SaveChanges();
            }
        }

        public static int DeleteLanguage(List<Language> languageID, int id)
        {
            using (NewtonLibraryContext db = new())
            {
                foreach (var item in languageID)
                    if (id == item.Id)
                        db.Remove(item);
                return db.SaveChanges();
            }
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
            Console.WriteLine("ID not found.");
            return false;
        }

        public static int DeleteProduct(List<Product> productID, int id)
        {
            using (NewtonLibraryContext db = new())
            {
                foreach (var item in productID)
                   if (id == item.Id)
                        db.Remove(item);
                return db.SaveChanges();
            }
        }

        public static int DeleteType(List<Models.Type> typeID, int id)
        {
            using (NewtonLibraryContext db = new())
            {
                foreach (var item in typeID)
                    if (id == item.Id)
                        db.Remove(item);
                return db.SaveChanges();
            }
        }

        public static int DeleteUser(List<User> userID, int id)
        {
            using (NewtonLibraryContext db = new())
            {
                foreach (var item in userID)
                    if (id == item.Id)
                        db.Remove(item);
                return db.SaveChanges();
            }
        }
    }
}