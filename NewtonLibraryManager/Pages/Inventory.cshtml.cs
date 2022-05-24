using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class InventoryModel : PageModel
    {
        [BindProperty]
        public List<Models.DisplayProductModel> Products { get; set; }

        [BindProperty]
        public int InventoryAmount { get; set; }

        [BindProperty]
        public int AmountOfBorrowedProducts { get; set; }


        /// <summary>
        /// When page is loaded
        /// </summary>
        public void OnGet()
        {
            //Get all products
            Products = Handlers.ProductHandler.ListAllProducts();

            //Get sum of all products
            InventoryAmount = Handlers.InventoryHandler.GetInventoryAmount();

            //Get sum of borrowed products
            AmountOfBorrowedProducts = Handlers.InventoryHandler.GetAmountOfBorrowedProducts();
        }
    }
}
