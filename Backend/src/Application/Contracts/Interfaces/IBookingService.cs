
namespace Application.Contracts.Interfaces;

public interface IBookingService
{
    Task<Application.Contracts.DTOs.SeatPlanDto> GetSeatPlanAsync(Guid busScheduleId);
    Task<Application.Contracts.DTOs.BookSeatResultDto> BookSeatAsync(Application.Contracts.DTOs.BookSeatInputDto input);
}
