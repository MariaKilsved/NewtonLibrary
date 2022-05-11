using NewtonLibraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace NewtonLibraryManager.EntityFramework.Update
{
    public static class UpdateHandler
    {
        public static int updateUser(int userId, User updatedUser)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                try
                {
                    //Get user that is to be updated
                    User user = Read.ReadHandler.GetUsers(userId);

                    //Update the retrieved user from DB and replace it with
                    //edited user from form.
                    user.FirstName = updatedUser.FirstName;
                    user.LastName = updatedUser.LastName;
                    user.EMail = updatedUser.EMail;
                    user.IsAdmin = updatedUser.IsAdmin;
                    user.Password = updatedUser.Password;
                }
                catch (Exception)
                {
                    throw new Exception("Could not set object properties");
                }

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("Could not update database");
                }
            }
        }
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
                    author.FirstName = updatedAuthor.FirstName;
                    author.LastName = updatedAuthor.LastName;
                    author.AuthorDetails = updatedAuthor.AuthorDetails;
                }
                catch (Exception)
                {
                    throw new Exception("Could not set object properties");
                }

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("Could not update database");
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
                    category.Category1 = updatedCategory.Category1;
                }
                catch (Exception)
                {
                    throw new Exception("Could not set object properties");
                }

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("Could not update database");
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
                    product.Title = updatedProduct.Title;
                    product.LanguageId = updatedProduct.LanguageId;
                    product.CategoryId = updatedProduct.CategoryId;
                    product.NrOfCopies = updatedProduct.NrOfCopies;
                    product.Dewey = updatedProduct.Dewey;
                    product.Description = updatedProduct.Description;
                    product.Isbn = updatedProduct.Isbn;
                    product.ProductType = updatedProduct.ProductType;
                }
                catch (Exception)
                {
                    throw new Exception("Could not set object properties");
                }

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("Could not update database");

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
                    lendingDetail.UserId = updatedLendingDetail.UserId;
                    lendingDetail.BorrowedFrom = updatedLendingDetail.BorrowedFrom;
                    lendingDetail.BorrowedTo = updatedLendingDetail.BorrowedTo;
                    lendingDetail.ReturnDate = updatedLendingDetail.ReturnDate;
                    lendingDetail.ProductId = updatedLendingDetail.ProductId;

                    lendingDetail.Product = updatedLendingDetail.Product;
                    lendingDetail.User = updatedLendingDetail.User;
                }
                catch (Exception)
                {
                    throw new Exception("Could not set object properties");
                }

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("Could not update database");
                }
            }
        }
    }
}
