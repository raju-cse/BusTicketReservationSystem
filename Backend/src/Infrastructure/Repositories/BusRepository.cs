
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BusRepository : Repository<Bus>, Application.Contracts.Interfaces.IBusRepository
{
    public BusRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Bus> GetWithSchedulesAsync(Guid id)
    {
        return await _context.Buses
            .Include(b => b.Schedules)
                .ThenInclude(s => s.Route)
            .Include(b => b.Seats)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}
