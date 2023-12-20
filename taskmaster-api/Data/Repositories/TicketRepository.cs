using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using taskmaster_api.Data.Contexts;
using taskmaster_api.Data.Entities;
using taskmaster_api.Data.Repositories.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace taskmaster_api.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public TicketEntity GetTicketById(int id)
        {
            return _context.Tickets.Find(id);
        }

        public IEnumerable<TicketEntity> GetAllTickets()
        {
            return _context.Tickets.ToList();
        }

        public TicketEntity CreateTicket(TicketEntity ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return ticket;
        }

        public TicketEntity UpdateTicket(int id, TicketEntity ticket)
        {
            if (_context.Tickets.Find(id) is TicketEntity oldTicket)
            {
                ticket.Id = id;
                _context.Tickets.Entry(oldTicket).State = EntityState.Detached;
                _context.Tickets.Entry(ticket).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return ticket;
        }

        public int DeleteTicket(int id)
        {
            var ticketToDelete = _context.Tickets.Find(id);
            if (ticketToDelete != null)
            {
                _context.Tickets.Remove(ticketToDelete);
                _context.SaveChanges();
                return id;
            }
            return -1;
        }
    }
}
