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
        return RedirectToPage("/ProductView/" + id.ToString());
    }

    //public async Task<IActionResult> OnPostAsync()
    public IActionResult OnPost()
    {
        //Om det �r n�got fel p� det som skrivits in laddas sidan bara om
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        Search = Search?.Replace("-", "");

        if (String.IsNullOrWhiteSpace(Search))
        {
            return Page();
        }

        SearchResults = Handlers.SearchHandler.ProductSearch(Search);

        SearchCompleted = true;

        //Should instead set the property SearchResults!
        //SearchResults.Author should be a string of authors separated by ,

        return RedirectToPage("/Index");

    }

}