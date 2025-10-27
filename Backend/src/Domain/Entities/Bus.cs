
namespace Domain.Entities;

public class Bus : BaseEntity
{
    public string CompanyName { get; private set; }
    public string BusName { get; private set; }
    public string BusNumber { get; private set; }
    public int TotalSeats { get; private set; }
    public bool HasAC { get; private set; }
    public List<Seat> Seats { get; private set; } = new();
    public List<BusSchedule> Schedules { get; private set; } = new();

    private Bus() { }

    public Bus(string companyName, string busName, string busNumber, int totalSeats, bool hasAC)
    {
        CompanyName = companyName;
        BusName = busName;
        BusNumber = busNumber;
        TotalSeats = totalSeats;
        HasAC = hasAC;
        CreateSeats();
    }

    private void CreateSeats()
    {
        for (int row = 1; row <= TotalSeats / 4; row++)
        {
            for (int col = 1; col <= 4; col++)
            {
                var seatNumber = $"{row}{(char)('A' + col - 1)}";
                Seats.Add(new Seat(Id, seatNumber, row, col));
            }
        }
    }
}
