using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.DTOs.Interface;

namespace taskmaster_api.Services.Interface
{
    public interface ITicketService
    {
        ICoreActionResult<TicketDto> GetTicketById(int id);
        ICoreActionResult<List<TicketDto>> GetAllTickets();
        ICoreActionResult<TicketDto> CreateTicket(TicketDto ticketDto);
        ICoreActionResult<TicketDto> UpdateTicket(int id, TicketDto ticketDto);
        ICoreActionResult DeleteTicket(int id);
    }
}
