using Events.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business
{
    public interface IEventBusiness
    {
        public Task<IEventsAppResult> GetAllEvents();
        public Task<IEventsAppResult> GetEventById(int id);
        public Task<IEventsAppResult> CreateNewEvent(Event newEvent);
        public Task<IEventsAppResult> UpdateEventById(Event updateEvent);
        public Task<IEventsAppResult> DeleteEventById(int id);
    }
    public class EventBusiness : IEventBusiness
    {
        private readonly Net17102215EventsContext _context;

        public EventBusiness(Net17102215EventsContext context)
        {
            _context = context;
        }

        public async Task<IEventsAppResult> CreateNewEvent(Event newEvent)
        {
            try
            {
                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();
                return new EventsAppResult
                {
                    Status = 200,
                    Message = "Add event success",
                    Data = newEvent
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = 400,
                    Message = "Add event error: " + ex,
                };
            }
            
        }

        public async Task<IEventsAppResult> DeleteEventById(int id)
        {
            try
            {
                var existEvent = await _context.Events.FirstOrDefaultAsync(e => e.EventId == id);
                if (existEvent == null)
                {
                    return new EventsAppResult
                    {
                        Status = 404,
                        Message = "Not found"
                    };
                }
                // edit info event
                existEvent.IsDelete = true;

                // save event
                _context.Update(existEvent);
                await _context.SaveChangesAsync();

                return new EventsAppResult
                {
                    Status = 200,
                    Message = "Delete event success",
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = 400,
                    Message = "Delete event error: " + ex,
                };
            }
        }

        public async Task<IEventsAppResult> GetAllEvents()
        {
            try
            {
                var events = await _context.Events.OrderByDescending(e => e.StartDate).ToListAsync();
                if (!events.Any())
                {
                    return new EventsAppResult
                    {
                        Status = 404,
                        Message = "Not found"
                    };
                }
                return new EventsAppResult
                {
                    Status = 200,
                    Message = "Get events success",
                    Data = events.ToList()
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = 400,
                    Message = "Get list event error: " + ex,
                };
            }
            
        }

        public async Task<IEventsAppResult> GetEventById(int id)
        {
            try
            {
                var ev = await _context.Events.FirstOrDefaultAsync(e => e.EventId == id);
                if (ev != null)
                {
                    return new EventsAppResult
                    {
                        Status = 404,
                        Message = "Not found"
                    };
                }
                return new EventsAppResult
                {
                    Status = 200,
                    Message = "Get event success",
                    Data = ev
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = 400,
                    Message = "Get event error: " + ex,
                };
            }
           
        }

        public async Task<IEventsAppResult> UpdateEventById(Event updateEvent)
        {
            try
            {
                var existEvent = await _context.Events.FirstOrDefaultAsync(e => e.EventId == updateEvent.EventId);
                if (existEvent == null)
                {
                    return new EventsAppResult
                    {
                        Status = 404,
                        Message = "Not found"
                    };
                }
                // edit info event
                existEvent.Name = updateEvent.Name;
                existEvent.Location = updateEvent.Location;
                existEvent.Description = updateEvent.Description;
                existEvent.ImageLink = updateEvent.ImageLink;
                existEvent.StartDate = updateEvent.StartDate;
                existEvent.EndDate = updateEvent.EndDate;
                existEvent.OpenTicket = updateEvent.OpenTicket;
                existEvent.CloseTicket = updateEvent.CloseTicket;
                existEvent.TicketPrice = updateEvent.TicketPrice;
                existEvent.Quantity = updateEvent.Quantity;
                existEvent.OperatorName = updateEvent.OperatorName;

                // save event
                _context.Update(existEvent);
                await _context.SaveChangesAsync();

                return new EventsAppResult
                {
                    Status = 200,
                    Message = "Update event success",
                    Data = existEvent
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = 400,
                    Message = "Update event error: " + ex,
                };
            }
            
        }
    }

}
