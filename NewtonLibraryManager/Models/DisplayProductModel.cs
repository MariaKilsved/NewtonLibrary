namespace NewtonLibraryManager.Models;

public class DisplayProductModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }
    public string Category { get; set; }
    public int NrOfCopies { get; set; }
    public decimal Dewey { get; set; }
    public string Description { get; set; }
    public string Isbn { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProductType { get; set; }
    public string Authors { get { return LastName + ", " + FirstName;  } }
    public List<string> FirstNames { get; set; }
    public List<string> LastNames { get; set; }
}