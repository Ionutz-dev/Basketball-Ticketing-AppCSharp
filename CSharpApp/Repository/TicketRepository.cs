using CSharpApp.DB;
using CSharpApp.Model;

namespace CSharpApp.Repository;

public class TicketRepository : ITicketRepository
{
    public void Save(Ticket ticket)
    {
        using var conn = DBUtils.GetConnection();
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
                INSERT INTO Tickets (matchId, userId, customerName, seatsSold)
                VALUES (@matchId, @userId, @customerName, @seatsSold)";
        cmd.Parameters.AddWithValue("@matchId", ticket.MatchId);
        cmd.Parameters.AddWithValue("@userId", ticket.UserId);
        cmd.Parameters.AddWithValue("@customerName", ticket.CustomerName);
        cmd.Parameters.AddWithValue("@seatsSold", ticket.SeatsSold);
        cmd.ExecuteNonQuery();
    }
}