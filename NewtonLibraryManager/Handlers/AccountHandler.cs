using System.Security.Cryptography;
using System.Text.RegularExpressions;

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
        //Can't delete boss
        if(userId == 1)
            return false;

        try
        {
            EntityFramework.Delete.DeleteHandler.DeleteUser(userId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
            return false;
        }
    }

    public static bool ValidatePassword(string password, out string errorMessage)
    {
        errorMessage = "";

        if (string.IsNullOrWhiteSpace(password))
        {
            errorMessage = "L�senordet kan inte vara tomt";
            return false;
        }

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMinChars = new Regex(@".{8,}");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$�%^&*()_+=\[{\]};:<>|./?,-]");


        if (!hasLowerChar.IsMatch(password))
        {
            errorMessage = "L�senordet m�ste inneh�lla minst en liten bokstav";
            return false;
        }
        if (!hasUpperChar.IsMatch(password))
        {
            errorMessage = "L�senordet m�ste inneh�lla minst en stor bokstav";
            return false;
        }
        if (!hasMinChars.IsMatch(password))
        {
            errorMessage = "L�senordet m�ste ha minst 8 tecken";
            return false;
        }
        if (!(hasNumber.IsMatch(password) || hasSymbols.IsMatch(password)))
        {
            errorMessage = "L�senordet m�ste inneh�lla minst en siffra eller ett specialtecken";
            return false;
        } 
        return true;
    }

    public static void passwordHash(string pass)
    {
        byte[] salt = new byte[32];
        using (var rngCsp = new RSACryptoServiceProvider())
        {


        }
    }
}