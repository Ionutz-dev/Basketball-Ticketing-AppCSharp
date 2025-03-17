namespace CSharpApp.Repository;

using Model;

public interface ITicketRepository
{
    void Save(Ticket ticket);
}