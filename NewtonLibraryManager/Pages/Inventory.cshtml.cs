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


        public void OnGet()
        {
            Products = Handlers.ProductHandler.ListAllProducts();

            InventoryAmount = Handlers.InventoryHandler.GetInventoryAmount();

            AmountOfBorrowedProducts = Handlers.InventoryHandler.GetAmountOfBorrowedProducts();
        }
    }
}
