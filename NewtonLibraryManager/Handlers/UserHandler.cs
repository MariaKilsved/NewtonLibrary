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

		public static User GetAdmin(int userId)
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

		public static User GetUser(int userId)
		{
			User user = EntityFramework.Read.ReadHandler.GetUsers(userId);
			return user;
		}

		public static List<ReservationDetail> GetUserReservations(int userId)
        {
			List<ReservationDetail> reservationDetails = EntityFramework.Read.ReadHandler.GetReservationDetails();
			reservationDetails = reservationDetails.Where(x => x.UserId == userId).ToList();
			return reservationDetails;
        }

		// also show which product
		public static List<DisplayLoanedProductModel> GetUserLoans(int userId)
		{
			List<DisplayLoanedProductModel> loanedProducts = new();

			using (var db = new NewtonLibraryContext())
			{
				var queryable = from ld in db.LendingDetails
					join usr in db.Users on ld.UserId equals usr.Id
					join prdct in db.Products on ld.ProductId equals prdct.Id
					where usr.Id == userId
					select new
					{
						TITLE = prdct.Title,
						ISBN = prdct.Isbn,
						FROM = ld.BorrowedFrom,
						TO = ld.BorrowedTo
					};

				foreach (var item in queryable)
				{
					DisplayLoanedProductModel lpdm = new();
					lpdm.Title = item.TITLE;
					lpdm.Isbn = item.ISBN;
					lpdm.From = item.FROM;
					lpdm.To = item.TO;
					loanedProducts.Add(lpdm);
				}
			}
			return loanedProducts;
		}
	}
}

