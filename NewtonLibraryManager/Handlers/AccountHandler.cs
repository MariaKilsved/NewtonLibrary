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

    /// <summary>
    /// Default login method. Checks if entered e-mail and password exist in the database. If they do, the method returns true and
    /// sets the AccountHandler variables to true. Also checks if the current user is an admin, if so IsAdmin is set to true.
    /// </summary>
    /// <param name="email">User email</param>
    /// <param name="password">User password</param>
    /// <returns></returns>
    public static bool LogIn(string email, string password)
    {
        var listOfUsers = EntityFramework.Read.ReadHandler.GetUsers();
        
        // Login successful.
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

    /// <summary>
    /// Method to set the AccountHandler variables back to false when the user is logged out.
    /// </summary>
    public static void LogOut()
    {
            _loggedIn = false;
            _currentIdLoggedIn = 0;
            _adminLoggedIn = false;
    }

    /// <summary>
    /// First checks if the entered email is not already in use in the database. If not, the method creates a new
    /// user with the in-parameters and adds it to the database. Throws exception if anything goes wrong. Returns true
    /// if everything is successful.
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool CreateUser(string firstName, string lastName, string email, string password)
    {
        var listOfUsers = EntityFramework.Read.ReadHandler.GetUsers();
        var pass = true;

        //checking if email already exists.
        foreach (var item in listOfUsers)
            if (item.EMail == email)
                pass = false;
        try
        {
            //pass is false if email already detected in database.
            if (!pass)
                throw new Exception("Email already in use.");
                
            EntityFramework.Create.CreateHandler.CreateUser(firstName, lastName, email, password, false);
            return true;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
            return false;
        }
    }
    /// <summary>
    /// First checks if the entered email is not already in use in the database. If not, the method creates a new
    /// admin with the in-parameters and adds it to the database. Throws exception if anything goes wrong. Returns true
    /// if everything is successful.
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool CreateAdmin(string firstName, string lastName, string email, string password)
    {
        var listOfUsers = EntityFramework.Read.ReadHandler.GetUsers();
        var pass = true;

        //checking if email already exists.
        foreach (var item in listOfUsers)
            if (item.EMail == email)
                pass = false;
        try
        {
            //pass is false if email already detected in database.
            if (!pass)
                throw new Exception("Email already in use.");
            
            EntityFramework.Create.CreateHandler.CreateUser(firstName, lastName, email, password, true);
            return true;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
            return false;
        }
    }

    /// <summary>
    /// Deletes a user from the database if the in-parameter ID matches a user ID in the database. Returns true if
    /// successful. Returns false otherwise, or if the user ID is 1, which is the main admin of the system.
    /// (Can't delete the boss!)
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Uses regex to validate if the password is strong enough. Returns true if the following requirements are met:
    /// - At least one lowercase character.
    /// - At least one uppercase character.
    /// - At least eight characters.
    /// - At least one special character or digit.
    /// </summary>
    /// <param name="password"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public static bool ValidatePassword(string password, out string errorMessage)
    {
        errorMessage = "";

        if (string.IsNullOrWhiteSpace(password))
        {
            errorMessage = "L" + '\x00F6' + "senordet kan inte vara tomt";
            return false;
        }

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMinChars = new Regex(@".{8,}");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$�%^&*()_+=\[{\]};:<>|./?,-]");


        if (!hasLowerChar.IsMatch(password))
        {
            errorMessage = "L" + '\x00F6' + "senordet m" + '\x00E5' + "ste inneh" + '\x00E5' + "lla minst en liten bokstav";
            return false;
        }
        if (!hasUpperChar.IsMatch(password))
        {
            errorMessage = "L" + '\x00F6' + "senordet m" + '\x00E5' + "ste inneh" + '\x00E5' + "lla minst en stor bokstav";
            return false;
        }
        if (!hasMinChars.IsMatch(password))
        {
            errorMessage = "L" + '\x00F6' + "senordet m" + '\x00E5' + "ste ha minst 8 tecken";
            return false;
        }
        if (!(hasNumber.IsMatch(password) || hasSymbols.IsMatch(password)))
        {
            errorMessage = "L" + '\x00F6' + "senordet m" + '\x00E5' + "ste inneh" + '\x00E5' + "lla minst en siffra eller ett specialtecken";
            return false;
        } 
        return true;
    }
}