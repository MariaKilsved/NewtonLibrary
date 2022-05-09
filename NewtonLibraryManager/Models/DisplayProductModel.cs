namespace NewtonLibraryManager.Models;

public class DisplayProductModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Authors { get { return FirstName + " " + LastName; } }
}