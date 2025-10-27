
using Domain.Entities;

namespace Infrastructure.Data;

public static class SeedData
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        if (!context.Buses.Any())
        {
            var buses = new List<Bus>
            {
                new Bus("National Travels", "Luxury Coach", "99-DHA-CHA", 40, false),
                new Bus("Hanif Enterprise", "Executive", "68-RAJ-CHAPAI", 40, false),
                new Bus("Grameen Travels", "Comfort Line", "303-DHK-CHAP", 40, true),
                new Bus("Shohagh Paribahan", "Super Express", "45-CTG-DHK", 40, true)
            };

            await context.Buses.AddRangeAsync(buses);
            await context.SaveChangesAsync();
        }

        if (!context.Routes.Any())
        {
            var routes = new List<Route>
            {
                new Route("Dhaka", "Chittagong", 250, 800),
                new Route("Dhaka", "Rajshahi", 240, 700),
                new Route("Chittagong", "Cox's Bazar", 150, 500),
                new Route("Dhaka", "Sylhet", 200, 600)
            };

            await context.Routes.AddRangeAsync(routes);
            await context.SaveChangesAsync();
        }

        if (!context.BusSchedules.Any())
        {
            var buses = context.Buses.ToList();
            var routes = context.Routes.ToList();
            var random = new Random();

            var schedules = new List<BusSchedule>();

            for (int i = 0; i < 20; i++)
            {
                var bus = buses[random.Next(buses.Count)];
                var route = routes[random.Next(routes.Count)];
                var departureTime = new TimeSpan(random.Next(6, 22), random.Next(0, 60), 0);
                var arrivalTime = departureTime.Add(TimeSpan.FromHours(5));

                schedules.Add(new BusSchedule(
                    bus.Id,
                    route.Id,
                    DateTime.Today.AddDays(random.Next(1, 30)),
                    departureTime,
                    arrivalTime,
                    route.BasePrice + random.Next(0, 200)
                ));
            }

            await context.BusSchedules.AddRangeAsync(schedules);
            await context.SaveChangesAsync();
        }
    }
}
