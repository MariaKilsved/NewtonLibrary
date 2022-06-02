using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.Handlers;

public static class StatisticHandler
{
    public static int GetMostActiveBorrower()
    {
        using (NewtonLibraryContext context = new NewtonLibraryContext())
        {
            try
            {
                var user = context.LendingDetails.GroupBy(x => x.UserId)
                    .OrderByDescending(ids => ids.Count())
                    .Select(ids => ids.Key)
                    .First();

                return user ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return 0;
    }


    public static List<User> GetCustomers()
    {
        using (NewtonLibraryContext db = new())
        {
            try
            {
                var users = db.Users.Where(x => x.IsAdmin == false)
                    .OrderByDescending(x => x.LastName)
                    .ToList();
                return users;
            }
            catch (Exception ex)
            {

                throw new Exception("Could not find users", ex);
            }
        }
    }

    public static List<DisplayHotBook> HotProducts()
    {
        var hottestBooksToReturn = new List<DisplayHotBook>();
        using (NewtonLibraryContext db = new())
        {
            try
            {
                var hottestBooks = db.LendingDetails.GroupBy(x => x.ProductId)
                    .OrderByDescending(hb => hb.Count())
                    .Select(hb => hb.Key)
                    .ToList();

                foreach (var item in hottestBooks)
                {
                    var dp = new DisplayHotBook();
                    var product = EntityFramework.Read.ReadHandler.GetProducts(item.Value);
                    var authorDetail = EntityFramework.Read.ReadHandler.GetAuthorDetails(product.Id);
                    var authors = EntityFramework.Read.ReadHandler.GetAuthors(product.Id);
                    dp.Title = product.Title;
                    dp.AuthorName = authors.FirstName + authors.LastName;

                    hottestBooksToReturn.Add(dp);             
                }

                return hottestBooksToReturn;
            }
            catch (Exception e)
            {
                throw new Exception("We have no hot books....",e);
            }
        }
    }
}