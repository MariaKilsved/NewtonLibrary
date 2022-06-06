namespace NewtonLibraryManager.Models
{
    public class DisplayCurrentBorrowedBooks
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Isbn { get; set; }
        public string BorrowerName { get; set; }
        public DateTime? BorrowedFrom { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}
