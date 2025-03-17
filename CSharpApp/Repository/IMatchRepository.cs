namespace CSharpApp.Repository;

using Model;
using System.Collections.Generic;

public interface IMatchRepository
{
    List<Match> FindAll();
    List<Match> FindAvailableMatches();
    void UpdateSeats(int matchId, int seatsSold);
}