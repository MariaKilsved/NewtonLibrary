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
    }
}

