
namespace Domain.Entities;

public class Seat : BaseEntity
{
    public Guid BusId { get; private set; }
    public string SeatNumber { get; private set; }
    public int Row { get; private set; }
    public int Column { get; private set; }
    public SeatClass Class { get; private set; }

    public Bus Bus { get; private set; }

    private Seat() { }

    public Seat(Guid busId, string seatNumber, int row, int column, SeatClass seatClass = SeatClass.Standard)
    {
        BusId = busId;
        SeatNumber = seatNumber;
        Row = row;
        Column = column;
        Class = seatClass;
    }
}
