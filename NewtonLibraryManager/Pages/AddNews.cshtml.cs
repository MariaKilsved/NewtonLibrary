using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace NewtonLibraryManager.Pages
{
    public class AddNewsModel : PageModel
    {
        [BindProperty]
        public List<SelectListItem> Categories { get; set; }            //Used to make the category dropdown (select)

        [BindProperty, Required]
        public string SelectedCategory { get; set; }                    //The chosen option of the category dropdown

        [BindProperty, Required, MinLength(2), MaxLength(100)]
        public string Title { get; set; }

        [BindProperty, Required]
        public string Story { get; set; }

        [BindProperty]
        public bool ShowModal { get; set; }

        [BindProperty]
        public string ModalBody { get; set; }

        public void OnGet(bool showModal = false, string modalBody = "")
        {
            //Get all news categories
            var NewsCategories = EntityFramework.Read.ReadHandler.GetNECategories();

            //Convert into SelectListItem for dropdown
            Categories = NewsCategories.Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Category
            }).ToList();

            //Show modal if necessary
            ShowModal = showModal;
            ModalBody = modalBody;
        }

        public IActionResult OnPost()
        {

            var newsEvent = new Models.NewsAndEvent()
            {
                CategoryId = Int32.Parse(SelectedCategory),
                Title = Title,
                ContentText = Story,
                PublishedDate = DateTime.Now
            };

            Console.WriteLine("---AddNews---");
            Console.WriteLine("newsEvent.CategoryId: " + newsEvent.CategoryId);
            Console.WriteLine("newsEvent.Title: " + newsEvent.Title);
            Console.WriteLine("newsEvent.ContentText: " + newsEvent.ContentText);
            Console.WriteLine("newsEvent.PublishedDate: " + newsEvent.PublishedDate);
            Console.WriteLine();

            if(EntityFramework.Create.CreateHandler.CreateNewsEvent(newsEvent))
            {
                return RedirectToPage("/News", new { showModal = true, modalBody = "Nyhet publicerad" });
            }

            return RedirectToPage("/AddNews", new { showModal = true, modalBody = "Misslyckades med att publicera nyhet" });
        }
    }
}
