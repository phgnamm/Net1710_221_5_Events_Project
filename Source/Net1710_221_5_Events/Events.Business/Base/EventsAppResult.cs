using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business.Base
{
    public class EventsAppResult : IEventsAppResult
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public EventsAppResult()
        {
            Status = -1;
            Message = "Action fail";
        }

        public EventsAppResult(int status, string message)
        {
            Status = status;
            Message = message;
        }

        public EventsAppResult(int status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
