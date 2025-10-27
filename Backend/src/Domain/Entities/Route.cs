
namespace Domain.Entities;

public class Route : BaseEntity
{
    public string FromCity { get; private set; }
    public string ToCity { get; private set; }
    public decimal Distance { get; private set; }
    public decimal BasePrice { get; private set; }
    public List<BusSchedule> Schedules { get; private set; } = new();

    private Route() { }

    public Route(string fromCity, string toCity, decimal distance, decimal basePrice)
    {
        FromCity = fromCity;
        ToCity = toCity;
        Distance = distance;
        BasePrice = basePrice;
    }
}
