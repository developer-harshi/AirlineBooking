using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPIServices.Context;
using UserAPIServices.Entities;
using UserAPIServices.Models;

namespace UserAPIServices.Services
{
    public class UserService:IUserService
    {
        private readonly UserContext _userContext;
        public UserService(UserContext userContext)
        {
            _userContext = userContext;
        }
        public FlightBookingModel SaveFlightBooking(FlightBookingModel flightBookingModel)
        {
            try
            {
                FlightBooking flightBooking = _userContext.FlightBooking.Where(c => c.Id == flightBookingModel.Id).FirstOrDefault();
                if (flightBooking != null)
                {
                    FillFlightBookingModelToEntity(flightBookingModel, flightBooking);
                }
                else
                {
                    flightBooking = new FlightBooking();
                    flightBooking.Id = Guid.NewGuid();
                    flightBooking.Status = true;
                    flightBookingModel.Id = flightBooking.Id;
                    flightBooking.PNRNumber = DateTime.UtcNow.Day.ToString() + DateTime.UtcNow.Month.ToString() + "-" + DateTime.UtcNow.Year.ToString() + DateTime.UtcNow.Hour.ToString() + DateTime.UtcNow.Minute.ToString() + DateTime.UtcNow.Millisecond.ToString();
                    FillFlightBookingModelToEntity(flightBookingModel, flightBooking);

                }
                return flightBookingModel;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private static void FillFlightBookingModelToEntity(FlightBookingModel flightBookingModel, FlightBooking flightBooking)
        {
            flightBooking.AirlineId = flightBookingModel.AirlineId;
            flightBooking.ContactNumber = flightBookingModel.ContactNumber;
            flightBooking.FlightId = flightBookingModel.FlightId;
            flightBooking.FlightNumber = flightBookingModel.FlightNumber;
            flightBooking.FromDate = flightBookingModel.FromDate;
            flightBooking.FromLocation = flightBookingModel.FromLocation;

            flightBooking.MailId = flightBookingModel.MailId;
            flightBooking.NonVeg = flightBookingModel.NonVeg;
            flightBooking.NoOfBUSeats = flightBookingModel.NoOfBUSeats;
            flightBooking.NoOfNONBUSeats = flightBookingModel.NoOfNONBUSeats;
           
            flightBooking.Price = flightBookingModel.Price;
            flightBooking.Remarks = flightBookingModel.Remarks;
            flightBooking.SeatNo = flightBookingModel.SeatNo;

            flightBooking.ToDate = flightBookingModel.ToDate;
            flightBooking.ToLocation = flightBookingModel.ToLocation;
            flightBooking.UserRegistrestionId = flightBookingModel.UserRegistrestionId;
            flightBooking.Veg = flightBookingModel.Veg;
        }
    }
}
