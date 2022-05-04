using NewtonLibraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace NewtonLibraryManager.EntityFramework.Update
{
    public static class UpdateHandler
    {
        public static int updateAuthor(int authorId, Author updatedAuthor)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                try
                {
                    //Get author that is to be updated
                    Author author = Read.ReadHandler.GetAuthors(authorId);

                    //Update the retrieved author from DB and replace it with
                    //edited author from form.
                    author = updatedAuthor;
                }
                catch (Exception)
                {
                    throw;
                }

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new DbUpdateException();
                }
            }
        }

        public static int updateCategory(int categoryId, Category updatedCategory)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                try
                {
                    //Get category that is to be updated
                    Category category = Read.ReadHandler.GetCategories(categoryId);

                    //Update the retrieved category from DB and replace it with
                    //edited category from form.
                    category = updatedCategory;
                }
                catch (Exception)
                {
                    throw;
                }

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new DbUpdateException();
                }
            }
        }

        public static int updateProduct(int productId, Product updatedProduct)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                try
                {
                    //Get product that is to be updated
                    Product product = Read.ReadHandler.GetProducts(productId);

                    //Update the retrieved product from DB and replace it with
                    //edited product from form.
                    product = updatedProduct;
                }
                catch (Exception)
                {
                    throw;
                }

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new DbUpdateException();
                }
            }
        }

        public static int updateUsers(int userId, User updatedUser)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                try
                {
                    //Get user that is to be updated
                    User user = Read.ReadHandler.GetUsers(userId);

                    //Update the retrieved user from DB and replace it with
                    //edited user from form.
                    user = updatedUser;
                }
                catch (Exception)
                {
                    throw;
                }

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new DbUpdateException();
                }
            }
        }

        public static int updateLendingDetails(int lendingDetailsId, LendingDetail updatedLendingDetail)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                try
                {
                    //Get lendingDetail that is to be updated
                    LendingDetail lendingDetail = Read.ReadHandler.GetLendingDetails(lendingDetailsId);

                    //Update the retrieved lendingDetail from DB and replace it with
                    //edited lendingDetail from form.
                    lendingDetail = updatedLendingDetail;
                }
                catch (Exception)
                {
                    throw;
                }

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new DbUpdateException();
                }
            }
        }



    }
}
