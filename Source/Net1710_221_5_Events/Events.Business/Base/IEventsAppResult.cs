using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business.Base
{
    public interface IEventsAppResult
    {
        int Status { get; set; }
        string? Message { get; set; }
        object? Data { get; set; }
    }

}
