using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.DTOs.Interface;
using taskmaster_api.Data.Repositories.Interface;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger<TicketService> _logger;

        public TicketService(ITicketRepository ticketRepository, ILogger<TicketService> logger)
        {
            _ticketRepository = ticketRepository;
            _logger = logger;
        }

        public ICoreActionResult<TicketDto> GetTicketById(int id)
        {
            try
            {
                var ticket = _ticketRepository.GetTicketById(id);
                if (ticket == null)
                {
                    _logger.LogInformation("Ticket not found");
                    return CoreActionResult<TicketDto>.Failure("Ticket not found", "NotFound");
                }

                return CoreActionResult<TicketDto>.Success(ticket.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<TicketDto>.Exception(ex);
            }
        }

        public ICoreActionResult<List<TicketDto>> GetAllTickets()
        {
            try
            {
                var tickets = _ticketRepository.GetAllTickets();
                return CoreActionResult<List<TicketDto>>.Success(tickets.Select(ticket => ticket.ToDto()).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<List<TicketDto>>.Exception(ex);
            }
        }

        public ICoreActionResult<TicketDto> CreateTicket(TicketDto ticketDto)
        {
            try
            {
                var ticket = ticketDto.ToEntity();
                var newTicket = _ticketRepository.CreateTicket(ticket);
                return CoreActionResult<TicketDto>.Success(newTicket.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<TicketDto>.Exception(ex);
            }
        }

        public ICoreActionResult<TicketDto> UpdateTicket(int id, TicketDto ticketDto)
        {
            try
            {
                var ticket = ticketDto.ToEntity();
                var existingTicket = _ticketRepository.GetTicketById(id);
                if (existingTicket == null)
                {
                    _logger.LogInformation("Ticket not found");
                    return CoreActionResult<TicketDto>.Failure("Ticket not found", "NotFound");
                }

                var updatedTicket = _ticketRepository.UpdateTicket(id, ticket);
                return CoreActionResult<TicketDto>.Success(updatedTicket.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult<TicketDto>.Exception(ex);
            }
        }

        public ICoreActionResult DeleteTicket(int id)
        {
            try
            {
                var deletedTicketId = _ticketRepository.DeleteTicket(id);
                if (deletedTicketId == 0)
                {
                    _logger.LogInformation("Ticket not found");
                    return CoreActionResult.Ignore("Ticket not found", "NotFound");
                }
                return CoreActionResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return CoreActionResult.Exception(ex);
            }
        }
    }
}
