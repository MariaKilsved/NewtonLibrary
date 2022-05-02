using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.EntityFramework;

public class DBHandler
{
    /*
    public int addCustomer(string inputName, DateTime inputBirthDate, string inputCity, string inputEmail)
    {
        using (StoreContext db = new StoreContext())
        {
            var Customer = new Customer()
            {

                Name = inputName,
                Birthdate = inputBirthDate,
                City = inputCity,
                Email = inputEmail
            };
            db.Add(Customer);
            return db.SaveChanges();
        }
    }
}
*/

    public int AddAccount(string firstName, string lastName, string email, string password, bool isAdmin)
    {
        using (NewtonLibraryContext db = new())
        {
            var User = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                EMail = email,
                Password = password,
                IsAdmin = isAdmin
            };
            db.Add(User);
            return db.SaveChanges();
        }
    }
}