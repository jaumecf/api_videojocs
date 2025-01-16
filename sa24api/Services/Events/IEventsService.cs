using sa24api.Models;

namespace sa24api.Services.Events
{
    public interface IEventsService
    {
        public Task<Event> InsertEventDimoniMatatAsync(Event _event);
        public Task<Event> InsertEventInvaderMatatAsync(Event _event);
        public Task<Event> InsertEventDimoniCreat(Event _event);
    }
}
