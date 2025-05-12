using System;

namespace client
{
    public enum BasketballUserEvent
    {
        TicketSold
    }

    public class BasketballUserEventArgs : EventArgs
    {
        public BasketballUserEventArgs(BasketballUserEvent userEvent, object data)
        {
            UserEventType = userEvent;
            Data = data;
        }

        public BasketballUserEvent UserEventType { get; }
        public object Data { get; }
    }
}