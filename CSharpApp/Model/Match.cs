namespace CSharpApp.Model;

public class Match
{
    public int Id { get; set; }
    public string TeamA { get; set; }
    public string TeamB { get; set; }
    public double TicketPrice { get; set; }
    public int AvailableSeats { get; set; }
}