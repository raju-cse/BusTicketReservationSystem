
namespace Application.Contracts.DTOs;

public class SeatPlanDto
{
    public Guid BusScheduleId { get; set; }
    public List<SeatDto> Seats { get; set; } = new();
    public List<string> BoardingPoints { get; set; } = new();
    public List<string> DroppingPoints { get; set; } = new();
}

public class SeatDto
{
    public Guid SeatId { get; set; }
    public string SeatNumber { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public string Status { get; set; }
}

public class BookSeatInputDto
{
    public Guid BusScheduleId { get; set; }
    public Guid SeatId { get; set; }
    public string PassengerName { get; set; }
    public string MobileNumber { get; set; }
    public string BoardingPoint { get; set; }
    public string DroppingPoint { get; set; }
}

public class BookSeatResultDto
{
    public bool Success { get; set; }
    public Guid TicketId { get; set; }
    public string Message { get; set; }
}
