namespace CSharpApp.Model;

using System;

public class TicketSale
{
    public int SaleID { get; set; }
    public int MatchID { get; set; }
    public string CustomerName { get; set; }
    public int NumberOfSeats { get; set; }
    public DateTime SaleDateTime { get; set; }
}