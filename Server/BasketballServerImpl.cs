using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using model;
using persistence;
using services;

namespace Server
{
    public class BasketballServerImpl : IBasketballServices
    {
        private readonly IUserRepository userRepo;
        private readonly IMatchRepository matchRepo;
        private readonly ITicketRepository ticketRepo;
        private readonly IDictionary<int, IBasketballObserver> loggedClients;

        public BasketballServerImpl(IUserRepository userRepo, IMatchRepository matchRepo, ITicketRepository ticketRepo)
        {
            this.userRepo = userRepo;
            this.matchRepo = matchRepo;
            this.ticketRepo = ticketRepo;
            loggedClients = new Dictionary<int, IBasketballObserver>();
        }

        public User Login(string username, string password, IBasketballObserver clientObs)
        {
            var user = userRepo.FindByUsernameAndPassword(username, password);
            if (user != null)
            {
                if (loggedClients.ContainsKey(user.Id))
                    throw new BasketballException("User already logged in.");
                loggedClients[user.Id] = clientObs;
                return user;
            }
            else
                throw new BasketballException("Authentication failed.");
        }

        public void Logout(User user)
        {
            var foundUser = userRepo.FindByUsernameAndPassword(user.Username, user.Password);
            if (foundUser == null)
                throw new BasketballException("User not logged in.");

            if (!loggedClients.ContainsKey(foundUser.Id))
                throw new BasketballException("User not logged in.");

            loggedClients.Remove(foundUser.Id);
        }

        public IEnumerable<Match> GetAvailableMatches()
        {
            return matchRepo.FindAvailableMatches();
        }

        public void SellTicket(int matchId, int userId, string customerName, int seatsToBuy)
        {
            var match = matchRepo.FindMatchById(matchId);
            if (match == null)
                throw new BasketballException("Match not found.");

            if (match.AvailableSeats < seatsToBuy)
                throw new BasketballException("Not enough seats available.");

            ticketRepo.Save(new Ticket(0, matchId, userId, customerName, seatsToBuy));
            matchRepo.UpdateSeats(matchId, seatsToBuy);

            NotifyTicketSold();
        }

        private void NotifyTicketSold()
        {
            foreach (var client in loggedClients.Values)
            {
                try
                {
                    Task.Run(() => client.TicketSoldUpdate());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error notifying client: " + ex.Message);
                }
            }
        }
    }
}