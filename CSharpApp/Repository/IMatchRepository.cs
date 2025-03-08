namespace CSharpApp.Repository;

using CSharpApp.Model;
using System.Collections.Generic;

public interface IMatchRepository
{
    List<Match> GetAllAvailableMatches();
    Match GetMatchById(int matchID);
    void UpdateAvailableSeats(int matchID, int seatsSold);
}