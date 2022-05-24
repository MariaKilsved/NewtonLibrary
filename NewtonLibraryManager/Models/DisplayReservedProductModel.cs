namespace NewtonLibraryManager.Models
{
    public class DisplayReservedProductModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public DateTime? ReservationDate { get; set; }
        public string ReservationDateAsString
        {
            get
            {
                return ReservationDate?.ToString("MM/dd/yyyy") ?? "-";
            }
        }
    }
}
