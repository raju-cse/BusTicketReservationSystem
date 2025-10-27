
namespace Application.Contracts.Interfaces;

public interface ISearchService
{
    Task<List<Application.Contracts.DTOs.AvailableBusDto>> SearchAvailableBusesAsync(Application.Contracts.DTOs.SearchBusesQuery query);
}
