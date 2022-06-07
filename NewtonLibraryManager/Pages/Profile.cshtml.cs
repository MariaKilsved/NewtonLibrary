using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewtonLibraryManager.Pages
{
    public class ProfileModel : PageModel
    {

        [BindProperty]
        public Models.User User1 { get; set; }

        [BindProperty]
        public Models.User EditedUser { get; set; }

        [BindProperty]
        public string PasswordError { get; set; }

        [BindProperty]
        public List<Models.DisplayLoanedProductModel> UserLoans { get; set; } 

        [BindProperty]
        public List<Models.DisplayReservedProductModel> UserReservations { get; set; }

        [BindProperty]
        public bool ShowModal { get; set; }

        [BindProperty]
        public string ModalBody { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }


        /// <summary>
        /// Load page
        /// </summary>
        /// <param name="id">Id of the user to be displayed</param>
        /// <param name="showModal">Whether to show a modal or not</param>
        /// <param name="modalBody">What to put in the body of the modal</param>
        public void OnGet(string id, bool showModal = false, string modalBody = "")
        {
            //Uses the user Id determined in the Url to set everything
            Id = id;

            //int loggedIn = Handlers.AccountHandler.CurrentIdLoggedIn;

            //Check cookies
            var cookieValue = Request.Cookies["LibraryCookie"];
            var cookieValue1 = Request.Cookies["LibraryCookie1"];
            string cookieComparerTrue = Models.SecurePasswordHasher.Hash("NewtonLibraryManager_True");
            string cookieComparerId = Models.SecurePasswordHasher.Hash("NewtonLibraryManager_" + Id);

            if(cookieValue == cookieComparerId || cookieValue1 == cookieComparerTrue)
            {
                //Needs to be User1 instead of User to avoid hiding PageModel.User
                User1 = Handlers.UserHandler.GetUser(Int32.Parse(Id));
            }

            //Deep copy in order to keep frontend and user edits separate
            EditedUser = new Models.User() { 
                Id = User1.Id, 
                EMail = User1.EMail, 
                FirstName = User1.FirstName, 
                LastName = User1.LastName, 
                IsAdmin = User1.IsAdmin, 
                Password = User1.Password };

            //Obtain user loans
            UserLoans = Handlers.UserHandler.GetUserLoans(Int32.Parse(Id)).Where(x => x.Returned == null).ToList();

            //Obtain user reservations
            UserReservations = Handlers.UserHandler.GetUserReservations(Int32.Parse(Id));

            //Show modal if necessary
            ShowModal = showModal;
            ModalBody = modalBody;

        }

        /// <summary>
        /// When submit button for updating user is pressed
        /// </summary>
        /// <returns>Redirect to index or reload page</returns>
        public IActionResult OnPostEdit()
        {
            //Must have password
            if (ModelState.IsValid == false || String.IsNullOrWhiteSpace(EditedUser.Password))
            {
                PasswordError = "Ange l" + '\x00F6' + "senord";
                return Page();
            }
            else
            {
                //Regex validation
                if (!Handlers.AccountHandler.ValidatePassword(EditedUser.Password, out string passwordError))
                {
                    PasswordError = passwordError;
                    return Page();
                }
                //Password hashing
                else
                {
                    EditedUser.Password = Models.SecurePasswordHasher.Hash(EditedUser.Password);
                }
            }

            //Set IsAdmin as false if it is null for some reason
            if (EditedUser.IsAdmin == null)
            {
                EditedUser.IsAdmin = false;
            }
            
            //Update the user
            try
            {
                EntityFramework.Update.UpdateHandler.UpdateUser(EditedUser);
                return RedirectToPage("/Profile", new { id = Id, showModal = true, modalBody = "Anv�ndare uppdaterad" });
            }
            catch 
            {
                return RedirectToPage("/Profile", new { id = Id, showModal = true, modalBody = "Misslyckades med att uppdatera anv�ndare" });
            }

        }

        /// <summary>
        /// When the submit button to delete the user is pressed
        /// </summary>
        /// <param name="id">Id of the user to be deleted</param>
        /// <returns>Redirect to index or logot, or reload page</returns>
        public IActionResult OnPostDelete(int id)
        {
            //Validate user through cookies
            string selectedIdHash = Models.SecurePasswordHasher.Hash("NewtonLibraryManager_" + id.ToString());

            //Never delete the head librarian (boss)
            if (selectedIdHash != Models.SecurePasswordHasher.Hash("NewtonLibraryManager_1") && Handlers.AccountHandler.DeleteUser(id))
            {
                //Delete cookies and log out if deleting yourself
                if (Request.Cookies["LibraryCookie"] == selectedIdHash)
                {
                    Response.Cookies.Delete("LibraryCookie");
                    Response.Cookies.Delete("LibraryCookie1");
                    Response.Cookies.Delete("LibraryCookie2");
                    return RedirectToPage("/Logout");
                }
                //Go to index page if deleting someone else
                return RedirectToPage("/Index", new { showModal = true, modalBody = "Anv�ndaren borttagen" });
            }
            //Reload page if it fails
            return RedirectToPage("/Profile", new { id = Id, showModal = true, modalBody = "Misslyckades med att ta bort anv�ndare" });
        }

        public IActionResult OnPostReturn(int id)
        {
            try
            {
                if (Handlers.ProductHandler.ReturnProduct(id))
                {
                    Console.WriteLine("Returned product");
                    return RedirectToPage("/Profile", new { id = Handlers.AccountHandler.CurrentIdLoggedIn, showModal = true, modalBody = "Produkt �terl�mnad" });
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return RedirectToPage("/Profile", new { id = Handlers.AccountHandler.CurrentIdLoggedIn, showModal = true, modalBody = "Misslyckades med att �terl�mna produkt" });
        }

        public IActionResult OnPostReborrow(int id)
        {
            try
            {
                if (Handlers.ProductHandler.ReBorrowProduct(User1.Id, id))
                {
                    return RedirectToPage("/Profile", new { id = Id, showModal = true, modalBody = "L�nade om produkt" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToPage("/Profile", new { id = Id, showModal = true, modalBody = "Misslyckades med att l�na om produkt" });
        }
    }
}
