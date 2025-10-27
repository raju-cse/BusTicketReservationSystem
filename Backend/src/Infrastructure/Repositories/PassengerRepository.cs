
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PassengerRepository : Repository<Passenger>, Application.Contracts.Interfaces.IPassengerRepository
{
    public PassengerRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Passenger> GetByMobileAsync(string mobileNumber)
    {
        return await _context.Passengers
            .FirstOrDefaultAsync(p => p.MobileNumber == mobileNumber);
    }
}
