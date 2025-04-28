using System.Collections.Generic;
using model;

namespace persistence
{
    public interface IMatchRepository
    {
        List<Match> FindAll();
        List<Match> FindAvailableMatches();
        void UpdateSeats(int matchId, int seatsSold);
        Match FindMatchById(int matchId);
    }
}