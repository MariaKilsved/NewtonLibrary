using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.Handlers;

/// <summary>
/// Retrieves news and events that are published today. Returns a list of DisplayNewsModel.
/// </summary>
public class NewsHandler
{
    /// <summary>
    /// Returns a news class with the news that have been published today.
    /// </summary>
    /// <returns></returns>
    public static List<DisplayNewsModel> GetNewsForToday()
    {
        var todayNewsList = new List<DisplayNewsModel>();
        using NewtonLibraryContext db = new();
        var news = db.NewsAndEvents.ToList();

        foreach (var item in news)
        {
            var todayNewsModel = new DisplayNewsModel();
            //check if it's more than a day old.
            if (item.PublishedDate != DateTime.Today) 
                continue;
            
            todayNewsModel.Title = item.Title;
            todayNewsModel.Content = item.ContentText;
            todayNewsModel.PublishedDate = item.PublishedDate;
            todayNewsList.Add(todayNewsModel);
        }

        return todayNewsList;
    }
}