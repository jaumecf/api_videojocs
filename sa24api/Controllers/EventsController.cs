using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sa24api.Models;
using sa24api.Services.Events;
using sa24api.Services.LeaderboardL1;
using System.Security.Claims;

namespace sa24api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;

        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpPost("InsertDimoniMatat"), Authorize]
        public async Task<ActionResult<Event>> InsertDimoniMatat(Event _event)
        {
            var @event = await _eventsService.InsertEventDimoniMatatAsync(_event);
            if (_event == null)
            {
                return BadRequest("Error inserting event: Dimoni Matat");
            }

            return Ok(_event);
        }

        [HttpPost("InsertInvaderMatat"), Authorize]
        public async Task<ActionResult<Event>> InsertInvaderMatat(Event _event)
        {
            var @event = await _eventsService.InsertEventInvaderMatatAsync(_event);
            if (_event == null)
            {
                return BadRequest("Error inserting event: Invader Matat");
            }

            return Ok(_event);
        }
    }
}
