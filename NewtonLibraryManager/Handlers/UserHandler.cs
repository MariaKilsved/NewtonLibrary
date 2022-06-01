using System;
using NewtonLibraryManager.Models;


namespace NewtonLibraryManager.Handlers
{
	public static class UserHandler
	{
		/// <summary>
		///	Returns a list of admin users from the database.
		/// </summary>
		/// <returns></returns>
		public static List<User> GetAdmins()
        {
			var list = EntityFramework.Read.ReadHandler.GetUsers();
			var admins = list.Where(x => x.IsAdmin == true).ToList();
			return admins;
        }

		/// <summary>
		/// Returns a specific admin from the database.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public static User GetAdmin(int userId)
		{
			var admins = GetAdmins();
			var admin = admins.FirstOrDefault(x => x.Id == userId);
			return admin;
		}

		/// <summary>
		/// Returns a list of all users from the database.
		/// </summary>
		/// <returns></returns>
		public static List<User> GetUsers()
		{
			var user = EntityFramework.Read.ReadHandler.GetUsers();
			return user;
		}

		/// <summary>
		/// Returns a specific user from the database, based on user ID.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public static User GetUser(int userId)
		{
			var user = EntityFramework.Read.ReadHandler.GetUsers(userId);
			return user;
		}

		/// <summary>
		/// Returns a list of reservations from the database. Based on user ID.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns>List of DisplayReservedProductModel with the reservations.</returns>
		public static List<DisplayReservedProductModel> GetUserReservations(int userId)
        {
			List<DisplayReservedProductModel> reservedProducts = new();

			using (var db = new NewtonLibraryContext())
            {
				var queryable = from rd in db.ReservationDetails
								join usr in db.Users on rd.UserId equals usr.Id
								join prdct in db.Products on rd.ProductId equals prdct.Id
								where usr.Id == userId
								select new DisplayReservedProductModel
								{
									ProductId = prdct.Id,
									Title = prdct.Title,
									Isbn = prdct.Isbn,
									ReservationDate = rd.ReservationDate
								};

				reservedProducts = queryable.ToList();
			}
			return reservedProducts;
        }

		/// <summary>
		/// Returns a list of DisplayLoanedProductModel classes. These classes represent a loaned product based on
		/// user ID. This method is used to display loaned products from a user. 
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
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
						ID = prdct.Id,
						TITLE = prdct.Title,
						ISBN = prdct.Isbn,
						FROM = ld.BorrowedFrom,
						TO = ld.BorrowedTo,
						RETURNED = ld.ReturnDate
					};

				foreach (var item in queryable)
				{
					DisplayLoanedProductModel lpdm = new();
					lpdm.Id = item.ID;
					lpdm.Title = item.TITLE;
					lpdm.Isbn = item.ISBN;
					lpdm.From = item.FROM;
					lpdm.To = item.TO;
					lpdm.Returned = item.RETURNED;
                    if (lpdm.Returned == null)
                    {
						loanedProducts.Add(lpdm);
					}
				}
			}
			return loanedProducts;
		}
	}
}

