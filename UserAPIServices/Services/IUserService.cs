using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPIServices.Entities;
using UserAPIServices.Models;

namespace UserAPIServices.Services
{
    public interface IUserService
    {
        FlightBookingModel SaveFlightBooking(FlightBookingModel flightBookingModel);
        FlightBooking GetTicketByPNR(string pnr);
        List<FlightBooking> GetTicketHistory(string emailId);
        bool CancelTicket(string pnr);
    }
}
