
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BusScheduleRepository : Repository<BusSchedule>, Application.Contracts.Interfaces.IBusScheduleRepository
{
    public BusScheduleRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<BusSchedule>> GetSchedulesWithDetailsAsync(string from, string to, DateTime journeyDate)
    {
        return await _context.BusSchedules
            .Include(bs => bs.Bus)
                .ThenInclude(b => b.Seats)
            .Include(bs => bs.Route)
            .Where(bs => bs.Route.FromCity.ToLower() == from.ToLower() 
                      && bs.Route.ToCity.ToLower() == to.ToLower()
                      && bs.JourneyDate.Date == journeyDate.Date
                      && bs.JourneyDate >= DateTime.Now)
            .OrderBy(bs => bs.DepartureTime)
            .ToListAsync();
    }

    public async Task<BusSchedule> GetWithDetailsAsync(Guid id)
    {
        return await _context.BusSchedules
            .Include(bs => bs.Bus)
                .ThenInclude(b => b.Seats)
            .Include(bs => bs.Route)
            .FirstOrDefaultAsync(bs => bs.Id == id);
    }
}
