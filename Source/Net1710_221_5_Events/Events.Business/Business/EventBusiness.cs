using Events.Business.Base;
using Events.Common;
using Events.Data.DAO;
using Events.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business.Business
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
        private readonly EventDAO _DAO;

        public EventBusiness()
        {
            _DAO = new EventDAO();
        }

        public async Task<IEventsAppResult> CreateNewEvent(Event newEvent)
        {
            try
            {
                await _DAO.CreateAsync(newEvent);
                return new EventsAppResult
                {
                    Status = Const.SUCCESS_CREATE_CODE,
                    Message = Const.SUCCESS_CREATE_MSG,
                    Data = newEvent
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = Const.ERROR_EXCEPTION,
                    Message = ex.ToString(),
                };
            }

        }

        public async Task<IEventsAppResult> DeleteEventById(int id)
        {
            try
            {
                var currency = await _DAO.GetByIdAsync(id);
                if (currency != null)
                {
                    var result = await _DAO.RemoveAsync(currency);
                    if (result)
                    {
                        return new EventsAppResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new EventsAppResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new EventsAppResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new EventsAppResult(-4, ex.ToString());
            }
        }

        public async Task<IEventsAppResult> GetAllEvents()
        {
            try
            {
                var events = await _DAO.GetAllAsync();
                if (!events.Any())
                {
                    return new EventsAppResult
                    {
                        Status = Const.WARNING_NO_DATA_CODE,
                        Message = Const.WARNING_NO_DATA__MSG,
                    };
                }
                return new EventsAppResult
                {
                    Status = Const.SUCCESS_READ_CODE,
                    Message = Const.SUCCESS_READ_MSG,
                    Data = events.ToList()
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = Const.ERROR_EXCEPTION,
                    Message = ex.ToString(),
                };
            }

        }

        public async Task<IEventsAppResult> GetEventById(int id)
        {
            try
            {
                var ev = await _DAO.GetByIdAsync(id);
                if (ev != null)
                {
                    return new EventsAppResult
                    {
                        Status = Const.WARNING_NO_DATA_CODE,
                        Message = Const.WARNING_NO_DATA__MSG
                    };
                }
                return new EventsAppResult
                {
                    Status = Const.SUCCESS_READ_CODE,
                    Message = Const.SUCCESS_READ_MSG,
                    Data = ev
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = Const.ERROR_EXCEPTION,
                    Message = ex.ToString(),
                };
            }

        }

        public async Task<IEventsAppResult> UpdateEventById(Event updateEvent)
        {
            try
            {
                var existEvent = await _DAO.GetByIdAsync(updateEvent.EventId);
                if (existEvent == null)
                {
                    return new EventsAppResult
                    {
                        Status = Const.FAIL_UPDATE_CODE,
                        Message = Const.FAIL_UPDATE_MSG
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
                await _DAO.UpdateAsync(existEvent);

                return new EventsAppResult
                {
                    Status = Const.SUCCESS_UPDATE_CODE,
                    Message = Const.SUCCESS_UPDATE_MSG,
                };
            }
            catch (Exception ex)
            {
                return new EventsAppResult
                {
                    Status = Const.ERROR_EXCEPTION,
                    Message = ex.ToString(),
                };
            }

        }
    }

}
