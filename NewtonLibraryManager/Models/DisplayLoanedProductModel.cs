namespace NewtonLibraryManager.Models;

public class DisplayLoanedProductModel
{
    public string Title { get; set; }
    public string Isbn { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}