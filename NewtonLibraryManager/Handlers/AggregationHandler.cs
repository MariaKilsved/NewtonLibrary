using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.Handlers;

public class AggregationHandler
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
}