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
    public bool IncludeBooks { get; set; }

    [BindProperty]
    public bool IncludeEbooks { get; set; }

    [BindProperty]
    public bool IncludeAudio { get; set; }

    [BindProperty]
    public List<Models.DisplayProductModel> SearchResults { get; set; }

    [BindProperty]
    public bool SearchCompleted { get; set; }

    public void OnGet()
    {
        SearchCompleted = false;
    }

    public IActionResult OnPostView(int id)
    {
        if (ModelState.IsValid == false)
            return Page();
        else
        {
            return RedirectToPage("/ProductView?Id=" + id.ToString());
        }

    }

    public void OnPostEdit()
    {
        Search = Search?.Replace("-", "");


        SearchResults = Handlers.SearchHandler.ProductSearch(Search);

        SearchCompleted = true;
    }

}