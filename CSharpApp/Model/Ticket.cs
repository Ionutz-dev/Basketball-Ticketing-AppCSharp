namespace CSharpApp.Model;

public class Ticket
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int UserId { get; set; }
    public string CustomerName { get; set; }
    public int SeatsSold { get; set; }
}