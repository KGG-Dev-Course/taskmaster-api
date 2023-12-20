using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Services.Interface;

namespace taskmaster_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ApplicationControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public IActionResult GetAllTickets()
        {
            return ToHttpResult<List<TicketDto>>(_ticketService.GetAllTickets());
        }

        [HttpGet("{id}")]
        public IActionResult GetTicket(int id)
        {
            return ToHttpResult<TicketDto>(_ticketService.GetTicketById(id));
        }

        [HttpPost]
        public IActionResult CreateTicket(TicketDto ticketDto)
        {
            return ToHttpResult<TicketDto>(_ticketService.CreateTicket(ticketDto));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTicket(int id, TicketDto ticketDto)
        {
            return ToHttpResult<TicketDto>(_ticketService.UpdateTicket(id, ticketDto));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id)
        {
            return ToHttpResult(_ticketService.DeleteTicket(id));
        }
    }
}
