using System;

namespace networking
{
    public interface IResponse
    {
    }

    [Serializable]
    public class OkResponse : IResponse
    {
    }

    [Serializable]
    public class ErrorResponse : IResponse
    {
        public ErrorResponse(string message)
        {
            Message = message;
        }

        public virtual string Message { get; }
    }

    [Serializable]
    public class GetAvailableMatchesResponse : IResponse
    {
        public GetAvailableMatchesResponse(MatchDTO[] matches)
        {
            Matches = matches;
        }

        public virtual MatchDTO[] Matches { get; }
    }

    public interface IUpdateResponse : IResponse
    {
    }

    [Serializable]
    public class TicketSoldUpdate : IUpdateResponse
    {
        public TicketSoldUpdate()
        {
        }
    }

    [Serializable]
    public class TicketSoldResponse : IUpdateResponse
    {
    }

    [Serializable]
    public class TicketConfirmedResponse : IResponse
    {
    }
}