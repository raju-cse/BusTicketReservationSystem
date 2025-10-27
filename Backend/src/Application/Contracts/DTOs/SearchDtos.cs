
namespace Application.Contracts.DTOs;

public class AvailableBusDto
{
    public Guid ScheduleId { get; set; }
    public string CompanyName { get; set; }
    public string BusName { get; set; }
    public string BusNumber { get; set; }
    public bool HasAC { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public int SeatsLeft { get; set; }
    public decimal Price { get; set; }
}

public class SearchBusesQuery
{
    public string From { get; set; }
    public string To { get; set; }
    public DateTime JourneyDate { get; set; }
}
