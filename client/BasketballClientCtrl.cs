using System;
using System.Collections.Generic;
using model;
using services;

namespace client
{
    public sealed class BasketballClientCtrl : IBasketballObserver
    {
        public event EventHandler<BasketballUserEventArgs> UpdateEvent;
        private readonly IBasketballServices server;
        private User currentUser;

        public BasketballClientCtrl(IBasketballServices server)
        {
            this.server = server;
        }

        public void Login(string username, string password)
        {
            currentUser = server.Login(username, password, this);
            Console.WriteLine("Login succeeded. Current user: {0}", username);
        }

        public void Logout()
        {
            if (currentUser == null) return;
            
            server.Logout(currentUser);
            currentUser = null;
        }

        public IEnumerable<Match> GetAvailableMatches()
        {
            return server.GetAvailableMatches();
        }

        public void SellTicket(int matchId, string customerName, int seats)
        {
            server.SellTicket(matchId, currentUser.Id, customerName, seats);
        }

        public void TicketSoldUpdate()
        {
            var args = new BasketballUserEventArgs(BasketballUserEvent.TicketSold, null);
            OnUserEvent(args);
        }

        private void OnUserEvent(BasketballUserEventArgs e)
        {
            UpdateEvent?.Invoke(this, e);
            Console.WriteLine("Update event triggered: {0}", e.UserEventType);
        }
        
        public bool IsLoggedIn()
        {
            return currentUser != null;
        }
    }
}