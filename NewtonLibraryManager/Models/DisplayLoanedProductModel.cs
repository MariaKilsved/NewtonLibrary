namespace NewtonLibraryManager.Models;

public class DisplayLoanedProductModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Isbn { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public string FromAsString
    {
        get
        {
            return From?.ToString("MM/dd/yyyy") ?? "-";
        }
    }

    public string ToAsString
    {
        get
        {
            return To?.ToString("MM/dd/yyyy") ?? "-";
        }
    }
}