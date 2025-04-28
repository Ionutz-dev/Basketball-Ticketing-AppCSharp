using model;
using System.Collections.Generic;

namespace services
{
    public interface IBasketballServices
    {
        User Login(string username, string password, IBasketballObserver clientObserver);
        void Logout(User user);
        IEnumerable<Match> GetAvailableMatches();
        void SellTicket(int matchId, int userId, string customerName, int seatsToBuy);
    }
}