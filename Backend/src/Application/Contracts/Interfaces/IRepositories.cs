
namespace Application.Contracts.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}

public interface IBusRepository : IRepository<Domain.Entities.Bus>
{
    Task<Domain.Entities.Bus> GetWithSchedulesAsync(Guid id);
}

public interface IBusScheduleRepository : IRepository<Domain.Entities.BusSchedule>
{
    Task<List<Domain.Entities.BusSchedule>> GetSchedulesWithDetailsAsync(string from, string to, DateTime journeyDate);
    Task<Domain.Entities.BusSchedule> GetWithDetailsAsync(Guid id);
}

public interface ITicketRepository : IRepository<Domain.Entities.Ticket>
{
    Task<List<Domain.Entities.Ticket>> GetTicketsForScheduleAsync(Guid busScheduleId);
    Task<bool> IsSeatAvailableAsync(Guid busScheduleId, Guid seatId);
}

public interface IPassengerRepository : IRepository<Domain.Entities.Passenger>
{
    Task<Domain.Entities.Passenger> GetByMobileAsync(string mobileNumber);
}
