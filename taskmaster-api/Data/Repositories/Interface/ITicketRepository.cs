using taskmaster_api.Data.Entities;

namespace taskmaster_api.Data.Repositories.Interface
{
    public interface ITicketRepository
    {
        TicketEntity GetTicketById(int id);
        IEnumerable<TicketEntity> GetAllTickets();
        TicketEntity CreateTicket(TicketEntity ticket);
        TicketEntity UpdateTicket(int id, TicketEntity ticket);
        int DeleteTicket(int id);
    }
}
