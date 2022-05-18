using NewtonLibraryManager.Models;
using System.Linq;
using System;

namespace NewtonLibraryManager.Handlers
{
	public static class InventoryHandler
	{
        //Returnerar antal böcker i lagret
		public static int GetInventoryAmount()
        {
            int amt = 0;
			var products = EntityFramework.Read.ReadHandler.GetProducts();
            foreach (var item in products)
            {
                amt = amt + item.NrOfCopies;
            }
            return amt;
        }

        //Returnerar antal lånade produkter
        public static int GetAmountOfBorrowedProducts()
        {
            var products = EntityFramework.Read.ReadHandler.GetLendingDetails();
            return products.Count();
        }

        //Returnerar utlånade böcker fårn ett visst ID
        public static List<LendingDetail> GetBorrowedFromProductId(int productId)
        {
            var list = EntityFramework.Read.ReadHandler.GetLendingDetails();
            var lendingDetails = list.Where(x => x.ProductId == productId).ToList();
            return lendingDetails;
        }

        //Returnerar antalet utlåningar för en viss produkt
        public static int GetNrOfBorrowedFromProductId(int productId)
        {
            var list = EntityFramework.Read.ReadHandler.GetLendingDetails();
            var lendingDetails = list.Where(x => x.ProductId == productId).ToList();
            return lendingDetails.Count;
        }

        //Kollar tidigaste återkommande bok för ett visst ID
        //och returnerar 
        public static DateTime? ReturnsToStock(int productId)
        {
            var list = GetBorrowedFromProductId(productId);
            DateTime? borrowedTo = DateTime.MaxValue;
            foreach (var product in list)
            {
                if (product.BorrowedTo < borrowedTo)
                {
                    borrowedTo = product.BorrowedTo;
                }
            }
            return borrowedTo;
        }
    }
}

