using NewtonLibraryManager.Models;
using Microsoft.EntityFrameworkCore;

namespace NewtonLibraryManager.EntityFramework.Update
{
    /// <summary>
    /// Update methods with Entity Framework
    /// </summary>
    public static class UpdateHandler
    {
        public static int UpdateUser(User updatedUser)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                db.Users.Attach(updatedUser);
                db.Entry(updatedUser).State = EntityState.Modified;

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not update database", ex);
                }
            }
        }
        public static int UpdateAuthor(Author updatedAuthor)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                db.Authors.Attach(updatedAuthor);
                db.Entry(updatedAuthor).State = EntityState.Modified;

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not update database", ex);
                }
            }
        }

        public static int UpdateCategory(Category updatedCategory)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                db.Categories.Attach(updatedCategory);
                db.Entry(updatedCategory).State = EntityState.Modified;

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not update database", ex);
                }
            }
        }

        public static int UpdateProduct(Product updatedProduct)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                db.Products.Attach(updatedProduct);
                db.Entry(updatedProduct).State = EntityState.Modified;

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not update database", ex);
                }
            }
        }


        public static int UpdateLendingDetails(LendingDetail updatedLendingDetail)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                db.LendingDetails.Attach(updatedLendingDetail);
                db.Entry(updatedLendingDetail).State = EntityState.Modified;

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not update database", ex);
                }
            }
        }

        public static int UpdateReservationDetails(ReservationDetail updatedReservationDetail)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                db.ReservationDetails.Attach(updatedReservationDetail);
                db.Entry(updatedReservationDetail).State = EntityState.Modified;

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not update database", ex);
                }
            }
        }

        public static int UpdateNewsAndEvent(NewsAndEvent updateNewsAndEvent)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                db.NewsAndEvents.Attach(updateNewsAndEvent);
                db.Entry(updateNewsAndEvent).State = EntityState.Modified;

                //Save changes, return number of rows changed or throw exception
                try
                {
                    return db.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw new Exception("Could not update database", ex);
                }
            }
        }
    }
}
