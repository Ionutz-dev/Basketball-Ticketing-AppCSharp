using model;

namespace persistence
{
    public interface ITicketRepository
    {
        void Save(Ticket ticket);
    }
}