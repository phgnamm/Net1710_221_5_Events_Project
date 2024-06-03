using Events.Business.Business;
using Events.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Events.RazorWebApp.Pages
{
    public class EventModel : PageModel
    {
        private readonly IEventBusiness _eventBusiness = new EventBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Event Event { get; set; } = default;
        public List<Event> Events { get; set; } = new List<Event>();

        public void OnGet(int? id)
        {
            if (id.HasValue)
            {
                Event = this.GetEventById(id.Value);
            }
            else
            {
                Events = this.GetEvents();
            }
        }

        public void OnPost()
        {
            this.SaveEvent();
        }

        public void OnDelete()
        {
        }


        private List<Event> GetEvents()
        {
            var eventResult = _eventBusiness.GetAllEvents();

            if (eventResult.Status > 0 && eventResult.Result.Data != null)
            {
                var currencies = (List<Event>)eventResult.Result.Data;
                return currencies;
            }
            return new List<Event>();
        }

        private Event GetEventById(int id)
        {
            var eventResult = _eventBusiness.GetEventById(id);

            if (eventResult.Status > 0 && eventResult.Result.Data != null)
            {
                return (Event)eventResult.Result.Data;
            }
            return null;
        }

        private void SaveEvent()
        {
            var eventResult = _eventBusiness.CreateNewEvent(this.Event);

            if (eventResult != null)
            {
                this.Message = eventResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
    }
}
