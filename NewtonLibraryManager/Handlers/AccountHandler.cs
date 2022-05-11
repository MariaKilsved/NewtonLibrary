namespace NewtonLibraryManager.Handlers;

public static class AccountHandler
{
    private static bool _loggedIn;
    private static int _currentIdLoggedIn;
    private static bool _adminLoggedIn;
    public static bool LoggedIn => _loggedIn;
    public static int CurrentIdLoggedIn => _currentIdLoggedIn;
    public static bool AdminLoggedIn => _adminLoggedIn;

    public static bool LogIn(string email, string password)
    {
        var listOfUsers = EntityFramework.Read.ReadHandler.GetUsers();
        
        foreach (var user in listOfUsers)
            if (email == user.EMail && password == user.Password)
            {
                _loggedIn = true;
                _currentIdLoggedIn = user.Id;
                _adminLoggedIn = user.IsAdmin ?? false;
                return true;
            }
        return false;
    }

    public static void LogOut()
    {
        if (LoggedIn || AdminLoggedIn)
        {
            _loggedIn = false;
            _currentIdLoggedIn = 0;
            _adminLoggedIn = false;
        }
        else
            Console.WriteLine("You're not logged in.");
    }

    public static bool CreateUser(string firstName, string lastName, string email, string password)
    {
        try
        {
            EntityFramework.Create.CreateHandler.CreateUser(firstName, lastName, email, password, false);
            return true;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
            return false;
        }
    }
    public static bool CreateAdmin(string firstName, string lastName, string email, string password)
    {
        try
        {
            EntityFramework.Create.CreateHandler.CreateUser(firstName, lastName, email, password, true);
            return true;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
            return false;
        }
    }

    public static bool DeleteUser(int userId)
    {
        var listOfUsers = EntityFramework.Read.ReadHandler.GetUsers();

        try
        {
            EntityFramework.Delete.DeleteHandler.DeleteUser(listOfUsers, userId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
            return false;
        }
    }
}