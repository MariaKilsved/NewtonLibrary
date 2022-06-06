using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class NewsModel : PageModel
    {
        [BindProperty]
        public List<Models.NewsAndEvent> News { get; set; }

        [BindProperty]
        public bool ShowModal { get; set; }

        [BindProperty]
        public string ModalBody { get; set; }

        public void OnGet(bool showModal = false, string modalBody = "")
        {
            //Get News
            News = EntityFramework.Read.ReadHandler.GetNewsAndEvents();

            //Reverse news to get latest first
            News.Reverse();

            //Show modal if necessary
            ShowModal = showModal;
            ModalBody = modalBody;
        }
    }
}
