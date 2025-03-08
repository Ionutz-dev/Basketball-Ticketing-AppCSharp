namespace CSharpApp.Model;

public class Match
{
    public int MatchID { get; set; }
    public string Title { get; set; }
    public string Stage { get; set; }
    public decimal Price { get; set; }
    public int AvailableSeats { get; set; }
}