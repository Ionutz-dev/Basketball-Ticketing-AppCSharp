using CSharpApp.Model;
using CSharpApp.Repository;

namespace CSharpApp.Service;

public class TicketService
{
    private readonly IMatchRepository _matchRepository;
    private readonly ITicketRepository _ticketRepository;

    public TicketService(IMatchRepository matchRepo, ITicketRepository ticketRepo)
    {
        _matchRepository = matchRepo;
        _ticketRepository = ticketRepo;
    }

    public List<Match> GetAvailableMatches() => _matchRepository.FindAvailableMatches();

    public void BuyTicket(int matchId, string customerName, int seats, int userId)
    {
        var ticket = new Ticket
        {
            MatchId = matchId,
            UserId = userId,
            CustomerName = customerName,
            SeatsSold = seats
        };
        _ticketRepository.Save(ticket);
        _matchRepository.UpdateSeats(matchId, seats);
    }
}