
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketRepository : Repository<Ticket>, Application.Contracts.Interfaces.ITicketRepository
{
    public TicketRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<Ticket>> GetTicketsForScheduleAsync(Guid busScheduleId)
    {
        return await _context.Tickets
            .Where(t => t.BusScheduleId == busScheduleId 
                     && (t.Status == TicketStatus.Booked || t.Status == TicketStatus.Sold))
            .ToListAsync();
    }

    public async Task<bool> IsSeatAvailableAsync(Guid busScheduleId, Guid seatId)
    {
        return !await _context.Tickets
            .AnyAsync(t => t.BusScheduleId == busScheduleId 
                        && t.SeatId == seatId 
                        && (t.Status == TicketStatus.Booked || t.Status == TicketStatus.Sold));
    }
}
