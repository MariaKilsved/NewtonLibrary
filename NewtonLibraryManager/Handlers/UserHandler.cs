using System;
using NewtonLibraryManager.Models;


namespace NewtonLibraryManager.Handlers
{
	public static class UserHandler
	{
		public static List<User> GetAdmins()
        {
			var list = EntityFramework.Read.ReadHandler.GetUsers();
			var admins = list.Where(x => x.IsAdmin == true).ToList();
			return admins;
        }

		public static User GetAdmins(int userId)
		{
			var admins = GetAdmins();
			var admin = admins.FirstOrDefault(x => x.Id == userId);
			return admin;
		}


	}
}

