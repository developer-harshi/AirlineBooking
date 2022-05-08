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
        List<TicketSearchModel> GetTicketByPNR(string pnr);
        List<TicketSearchModel> GetTicketHistory(string emailId);
        bool CancelTicket(string pnr);
        FlightSearchResults SearchFlights(FlightSearchModel flightSearchModel);
        FlightBookingModel CreateFlightBookingModel(Guid id,  Guid? flightId);
        List<FlightModel> FlightLu();
        //FlightBookingModel CreateFlightBookingModel(Guid id);
        BookingPersonsModel GetEmptyPerson();
        List<DiscountModel> GetDiscount();
        bool ActiveInActive(string tableName, Guid id, string status);
    }
}
