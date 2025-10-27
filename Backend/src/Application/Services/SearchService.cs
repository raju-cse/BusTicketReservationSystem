
using Application.Contracts.DTOs;
using Application.Contracts.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class SearchService : ISearchService
{
    private readonly IBusScheduleRepository _scheduleRepository;
    private readonly ITicketRepository _ticketRepository;

    public SearchService(IBusScheduleRepository scheduleRepository, ITicketRepository ticketRepository)
    {
        _scheduleRepository = scheduleRepository;
        _ticketRepository = ticketRepository;
    }

    public async Task<List<AvailableBusDto>> SearchAvailableBusesAsync(SearchBusesQuery query)
    {
        var schedules = await _scheduleRepository.GetSchedulesWithDetailsAsync(query.From, query.To, query.JourneyDate);
        var result = new List<AvailableBusDto>();

        foreach (var schedule in schedules)
        {
            var bookedTickets = await _ticketRepository.GetTicketsForScheduleAsync(schedule.Id);
            var bookedSeatsCount = bookedTickets.Count(t => t.Status == TicketStatus.Booked || t.Status == TicketStatus.Sold);
            var seatsLeft = schedule.Bus.TotalSeats - bookedSeatsCount;

            result.Add(new AvailableBusDto
            {
                ScheduleId = schedule.Id,
                CompanyName = schedule.Bus.CompanyName,
                BusName = schedule.Bus.BusName,
                BusNumber = schedule.Bus.BusNumber,
                HasAC = schedule.Bus.HasAC,
                StartTime = schedule.DepartureTime,
                ArrivalTime = schedule.ArrivalTime,
                SeatsLeft = seatsLeft,
                Price = schedule.ActualPrice
            });
        }

        return result;
    }
}
