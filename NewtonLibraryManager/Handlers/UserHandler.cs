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

		public static List<User> GetUsers()
		{
			List<User> user = EntityFramework.Read.ReadHandler.GetUsers();
			return user;
		}

		public static User GetUsers(int userId)
		{
			User user = EntityFramework.Read.ReadHandler.GetUsers(userId);
			return user;
		}

		public static List<ReservationDetail> getUserReservations(int userId)
        {
			List<ReservationDetail> reservationDetails = EntityFramework.Read.ReadHandler.GetReservationDetails();
			reservationDetails = reservationDetails.Where(x => x.UserId == userId).ToList();
			return reservationDetails;
        }

		public static List<LendingDetail> getUserLoans(int userId)
		{
			List<LendingDetail> lendingDetails = EntityFramework.Read.ReadHandler.GetLendingDetails();
			lendingDetails = lendingDetails.Where(x => x.UserId == userId).ToList();
			return lendingDetails;
		}
	}
}

