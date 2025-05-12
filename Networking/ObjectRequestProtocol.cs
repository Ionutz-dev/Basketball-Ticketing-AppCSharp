using System;

namespace networking
{
    public interface IRequest
    {
    }

    [Serializable]
    public class LoginRequest : IRequest
    {
        public LoginRequest(string[] credentials)
        {
            Credentials = credentials;
        }

        public virtual string[] Credentials { get; }
    }

    [Serializable]
    public class LogoutRequest : IRequest
    {
        public LogoutRequest(UserDTO user)
        {
            User = user;
        }

        public virtual UserDTO User { get; }
    }

    [Serializable]
    public class SellTicketRequest : IRequest
    {
        public SellTicketRequest(TicketDTO ticket)
        {
            Ticket = ticket;
        }

        public virtual TicketDTO Ticket { get; }
    }

    [Serializable]
    public class GetAvailableMatchesRequest : IRequest
    {
    }
}