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
    public string EMail { get; set; }

    [BindProperty]
    public string Password { get; set; }   //Should be hashed before put in User

    public int Id { get; set; }

    public void OnGet()
    {
        //User = new Models.User { IsAdmin = false };
    }


    //Detta kommer köras när man trycker på submit, d.v.s. användaren har skrivit in saker
    public IActionResult OnPost()
    {
        //Om det är något fel på det som skrivits in laddas sidan bara om
        if(ModelState.IsValid == false)
            return Page();

        var listOfUsers = EntityFramework.Read.ReadHandler.GetUsers();

        foreach (var user in listOfUsers)
            if (EMail == user.EMail && Password == user.Password)
            {
                Id = user.Id;

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                };
                Response.Cookies.Append("LibraryCookie", user.Id.ToString(), cookieOptions);

                return RedirectToPage("/ProductSearch");
            }
        return Page();
        //Kontrollera vad som hämtats från frontend.
        //Frontend gör antingen register eller login.
        //Login: User.EMail, Password
        //Register: User.FirstName, User.LastName, User.EMail, Password

        //Bör hash:a Password innan det läggs i User.Password

        //Bör testa om något av fälten på fronten var IsNullOrWhitespace
        //Om något var null bör sidan också laddas om d.v.s. Return Page();

        //Gå till annan sida
        //Kommer använda webbsession senare
    }

}