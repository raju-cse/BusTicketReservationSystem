
using Application.Contracts.DTOs;
using Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet("buses")]
    public async Task<ActionResult<List<AvailableBusDto>>> SearchBuses(
        [FromQuery] string from, 
        [FromQuery] string to, 
        [FromQuery] DateTime journeyDate)
    {
        var query = new SearchBusesQuery { From = from, To = to, JourneyDate = journeyDate };
        var result = await _searchService.SearchAvailableBusesAsync(query);
        return Ok(result);
    }
}
