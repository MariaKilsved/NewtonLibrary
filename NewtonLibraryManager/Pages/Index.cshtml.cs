using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewtonLibraryManager.Handlers;

namespace NewtonLibraryManager.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    [BindProperty]
    public string Search { get; set; }

    [BindProperty]
    public List<Models.DisplayNewsModel> News { get; set; }

    /*
    [BindProperty]
    public bool IncludeBooks { get; set; }

    [BindProperty]
    public bool IncludeEbooks { get; set; }

    [BindProperty]
    public bool IncludeAudio { get; set; }
    */

    [BindProperty]
    public List<Models.DisplayProductModel> SearchResults { get; set; }

    [BindProperty]
    public bool SearchCompleted { get; set; }

/// <summary>
/// When page is loaded
/// </summary>
    public void OnGet()
    {
        //Set boolean for search not being completed yet
        SearchCompleted = false;

        //Obtain latest news
        News = NewsHandler.GetNewsForToday();
    }

    /// <summary>
    /// On submit to search
    /// </summary>
    public void OnPostSearch()
    {
        //Remove all hyphens
        Search = Search?.Replace("-", "");

        //Retrieve search results
        SearchResults = SearchHandler.ProductSearch(Search);

        //Set boolean for completed search
        SearchCompleted = true;
    }

}