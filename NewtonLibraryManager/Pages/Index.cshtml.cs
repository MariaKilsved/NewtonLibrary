using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    /*
    //Har inget Id från frontend
    [BindProperty]
    public Models.User User { get; set; }
    */

    [BindProperty]
    public string FirstName { get; set; }

    [BindProperty]
    public string LastName { get; set; }

    [BindProperty]
    public string EMail { get; set; }

    [BindProperty]
    public string Password { get; set; }   //Should be hashed before put in User


    public void OnGet()
    {
        //User = new Models.User { IsAdmin = false };
    }


    //Detta kommer köras när man trycker på submit, d.v.s. användaren har skrivit in saker
    public IActionResult OnPost()
    {
        //Om det är något fel på det som skrivits in laddas sidan bara om
        if(ModelState.IsValid == false)
        {
            return Page();
        }

        //Antagligen ska man lägga in registrering i databasen här.
        //Kontrollera vad som hämtats från frontend.
        //Frontend gör antingen register eller login.
        //Login: User.EMail, Password
        //Register: User.FirstName, User.LastName, User.EMail, Password

        //Bör hash:a Password innan det läggs i User.Password

        //Bör testa om något av fälten på fronten var IsNullOrWhitespace
        //Om något var null bör sidan också laddas om d.v.s. Return Page();

        //Gå till annan sida
        //Kommer använda webbsession senare
        return RedirectToPage("/ProductSearch");
    }

}