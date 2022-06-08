using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.Handlers
{
	public static class InventoryHandler
	{
        /// <summary>
        /// Returns the amount of all products.
        /// </summary>
        /// <returns></returns>
		public static int GetInventoryAmount()
        {
            var products = EntityFramework.Read.ReadHandler.GetProducts();
            return products.Sum(item => item.NrOfCopies);
        }

        /// <summary>
        /// Returns the amount of borrowed products.
        /// </summary>
        /// <returns></returns>
        public static int GetAmountOfBorrowedProducts()
        {
            var products = EntityFramework.Read.ReadHandler.GetLendingDetails();
            return products.Count;
        }

        /// <summary>
        /// Returns a list of lending details from the database, based on a product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static List<LendingDetail> GetBorrowedFromProductId(int productId)
        {
            var list = EntityFramework.Read.ReadHandler.GetLendingDetails();
            var lendingDetails = list.Where(x => x.ProductId == productId).ToList();
            return lendingDetails;
        }

        /// <summary>
        /// Returns the amount of lending details based on a product id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static int GetNrOfBorrowedFromProductId(int productId)
        {
            var list = EntityFramework.Read.ReadHandler.GetLendingDetails();
            var lendingDetails = list.Where(x => x.ProductId == productId && x.ReturnDate == null).ToList();
            return lendingDetails.Count;
        }

        /// <summary>
        /// Returns the date of the earliest restock of a product, based on its' ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static DateTime? ReturnsToStock(int productId)
        {
            var list = GetBorrowedFromProductId(productId);
            DateTime? borrowedTo = DateTime.MaxValue;
            foreach (var product in list)
                if (product.BorrowedTo < borrowedTo)
                    borrowedTo = product.BorrowedTo;
            
            return borrowedTo;
        }

        /// <summary>
        /// Returns a list of borrowed books.
        /// </summary>
        /// <returns></returns>
        public static List<DisplayCurrentBorrowedBooks> GetAllBorrowedBooks()
        {
            var list = new List<DisplayCurrentBorrowedBooks>();
            using (var db = new NewtonLibraryContext())
            {
                var books = from p in db.Products
                            join ad in db.AuthorDetails on p.Id equals ad.ProductId
                            join a in db.Authors on ad.AuthorId equals a.Id
                            join ld in db.LendingDetails on p.Id equals ld.ProductId
                            join u in db.Users on ld.UserId equals u.Id
                            select new DisplayCurrentBorrowedBooks
                            {
                                Title = p.Title,
                                AuthorName = a.FirstName + a.LastName,
                                Isbn = p.Isbn,
                                BorrowerName = u.FirstName + u.LastName,
                                BorrowedFrom = ld.BorrowedFrom,
                                ReturnDate = ld.ReturnDate
                            };
                foreach (var item in books)
                {
                    var dp = new DisplayCurrentBorrowedBooks();
                    dp.Title = item.Title;
                    dp.AuthorName = item.AuthorName;
                    dp.Isbn = item.Isbn;
                    dp.BorrowerName = item.BorrowerName;
                    dp.BorrowedFrom = item.BorrowedFrom;
                    dp.ReturnDate = item.ReturnDate;

                    list.Add(dp);
                }
            }
            return list;
        }
    }
}

