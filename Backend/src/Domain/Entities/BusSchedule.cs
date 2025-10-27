
namespace Domain.Entities;

public class BusSchedule : BaseEntity
{
    public Guid BusId { get; private set; }
    public Guid RouteId { get; private set; }
    public DateTime JourneyDate { get; private set; }
    public TimeSpan DepartureTime { get; private set; }
    public TimeSpan ArrivalTime { get; private set; }
    public decimal ActualPrice { get; private set; }

    public Bus Bus { get; private set; }
    public Route Route { get; private set; }
    public List<Ticket> Tickets { get; private set; } = new();

    private BusSchedule() { }

    public BusSchedule(Guid busId, Guid routeId, DateTime journeyDate, TimeSpan departureTime, TimeSpan arrivalTime, decimal actualPrice)
    {
        BusId = busId;
        RouteId = routeId;
        JourneyDate = journeyDate;
        DepartureTime = departureTime;
        ArrivalTime = arrivalTime;
        ActualPrice = actualPrice;
    }
}
