namespace NewtonLibraryManager.Controllers;

public static class AccountController
{
    private static bool _loggedIn;
    public static bool LoggedIn => _loggedIn;

    public static bool LogIn(string email, string password)
    {
        var listOfUsers = EntityFramework.Read.ReadHandler.GetUsers();
        
        foreach (var user in listOfUsers)
            if (email == user.EMail && password == user.Password)
            {
                _loggedIn = true;
                return true;
            }
        return false;
    }

    public static void LogOut() => _loggedIn = false;
}