﻿using System;
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
                    _userContext.FlightBooking.Update(flightBooking);
                }
                else
                {
                    flightBooking = new FlightBooking();
                    flightBooking.Id = Guid.NewGuid();
                    flightBooking.Status = true;
                    flightBookingModel.Id = flightBooking.Id;
                    flightBooking.PNRNumber = DateTime.UtcNow.Day.ToString() + DateTime.UtcNow.Month.ToString() + "-" + DateTime.UtcNow.Year.ToString() + DateTime.UtcNow.Hour.ToString() + DateTime.UtcNow.Minute.ToString() + DateTime.UtcNow.Millisecond.ToString();
                    FillFlightBookingModelToEntity(flightBookingModel, flightBooking);
                    _userContext.FlightBooking.Add(flightBooking);

                }
                _userContext.SaveChanges();
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

            flightBooking.RegisteredMailId = flightBookingModel.RegisteredMailId;
            //flightBooking.NonVeg = flightBookingModel.NonVeg;
            flightBooking.NoOfBUSeats = flightBookingModel.NoOfBUSeats;
            flightBooking.NoOfNONBUSeats = flightBookingModel.NoOfNONBUSeats;

            flightBooking.TotalPrice = flightBookingModel.TotalPrice;
            flightBooking.Remarks = flightBookingModel.Remarks;
            flightBooking.SeatNos = flightBookingModel.SeatNos;

            flightBooking.ToDate = flightBookingModel.ToDate;
            flightBooking.ToLocation = flightBookingModel.ToLocation;
            flightBooking.UserRegistrestionId = flightBookingModel.UserRegistrestionId;
            //flightBooking.Veg = flightBookingModel.Veg;
        }
        public FlightBooking GetTicketByPNR(string pnr)
        {
            try
            {
                return _userContext.FlightBooking.Where(c => c.PNRNumber == pnr ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FlightBooking> GetTicketHistory(string emailId)
        {
            try
            {
                return _userContext.FlightBooking.Where(c => c.RegisteredMailId.ToLower() == emailId.ToLower()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CancelTicket(string pnr)
        {
            try
            {
                FlightBooking flightBooking = _userContext.FlightBooking.Where(c => c.PNRNumber == pnr).FirstOrDefault();
                if (flightBooking != null)
                {
                    flightBooking.Status = false;
                    _userContext.FlightBooking.Update(flightBooking);
                    _userContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public FlightSearchResults SearchFlights(FlightSearchModel flightSearchModel)
        {
            try
            {
                FlightSearchResults flightSearchResults = new FlightSearchResults();
                bool isWeekend = (flightSearchModel.SearchDate.Value.DayOfWeek.ToString().ToLower() == "saturday" || flightSearchModel.SearchDate.Value.DayOfWeek.ToString().ToLower() == "sunday") ? true : false;

                flightSearchResults.OnDateResults = _userContext.Flights.Where(c => (c.Sheduled.ToLower() == "daily" || c.Sheduled.ToLower() == (isWeekend == true ? "week ends" : "week days") || c.ToDate.Value.Date == flightSearchModel.SearchDate.Value.Date) && c.Status == true && c.FromLocation.ToLower() == flightSearchModel.FromLocation.ToLower() && c.ToLocation.ToLower() == flightSearchModel.ToLocation.ToLower()).ToList();
                if (flightSearchModel.RoundTripDate != null)
                {
                    bool isReturnDateWeekend = (flightSearchModel.RoundTripDate.Value.DayOfWeek.ToString().ToLower() == "saturday" || flightSearchModel.RoundTripDate.Value.DayOfWeek.ToString().ToLower() == "sunday") ? true : false;
                    flightSearchResults.ReturnDateResults = _userContext.Flights.Where(c => (c.Sheduled.ToLower() == "daily" || c.Sheduled.ToLower() == (isReturnDateWeekend == true ? "week ends" : "week days") || c.ToDate.Value.Date == flightSearchModel.SearchDate.Value.Date) && c.Status == true && c.FromLocation.ToLower() == flightSearchModel.ToLocation.ToLower() && c.ToLocation.ToLower() == flightSearchModel.FromLocation.ToLower()).ToList();
                }
                return flightSearchResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FlightBookingModel CreateFlightBookingModel(Guid id)
        {
            FlightBookingModel flightBookingModel = new FlightBookingModel();
            List<BookingPersonsModel> bookingPersonsModel = new List<BookingPersonsModel>();
            var flightBooking = _userContext.FlightBooking.Where(c => c.Id == id).AsQueryable();
            if(flightBooking!=null&& flightBooking.Count()>0)
            {
               FlightBooking flightBooking1 = flightBooking.FirstOrDefault();
                List<BookingPersons> lstBookingPersons = flightBooking1.BookingPersons.ToList();
                flightBookingModel.AirlineId = flightBooking1.AirlineId;
                flightBookingModel.ContactNumber = flightBooking1.ContactNumber;
                flightBookingModel.FlightId = flightBooking1.FlightId;
                flightBookingModel.FlightNumber = flightBooking1.FlightNumber;
                flightBookingModel.FromDate = flightBooking1.FromDate;
                flightBookingModel.FromLocation = flightBooking1.FromLocation;
                flightBookingModel.Id = flightBooking1.Id;
                flightBookingModel.NoOfBUSeats = flightBooking1.NoOfBUSeats;
                flightBookingModel.NoOfNONBUSeats = flightBooking1.NoOfNONBUSeats;
                flightBookingModel.PNRNumber = flightBooking1.PNRNumber;
                flightBookingModel.RegisteredMailId = flightBooking1.RegisteredMailId;
                flightBookingModel.Remarks = flightBooking1.Remarks;
                flightBookingModel.SeatNos = flightBooking1.SeatNos;
                flightBookingModel.Status = flightBooking1.Status;
                flightBookingModel.ToDate = flightBooking1.ToDate;
                flightBookingModel.ToLocation = flightBooking1.ToLocation;
                flightBookingModel.TotalPrice = flightBooking1.TotalPrice;
                flightBookingModel.UserRegistrestionId = flightBooking1.UserRegistrestionId;
                if (lstBookingPersons != null && lstBookingPersons.Count() > 0)
                {
                    foreach (var bookingPerson in lstBookingPersons)
                    {
                        BookingPersonsModel bookingPersonModel = new BookingPersonsModel();
                        bookingPersonModel.Age = bookingPerson.Age;
                        bookingPersonModel.ContactNumber = bookingPerson.ContactNumber;
                        bookingPersonModel.DOB = bookingPerson.DOB;
                        bookingPersonModel.Email = bookingPerson.Email;
                        bookingPersonModel.FlightBookingId = bookingPerson.FlightBookingId;
                        bookingPersonModel.Gender = bookingPerson.Gender;
                        bookingPersonModel.Id = bookingPerson.Id;
                        bookingPersonModel.Name = bookingPerson.Name;
                        bookingPersonModel.NonVeg = bookingPerson.NonVeg;
                        bookingPersonModel.Price = bookingPerson.Price;
                        bookingPersonModel.SeatNo = bookingPerson.SeatNo;
                        bookingPersonModel.Veg = bookingPerson.Veg;
                        bookingPersonsModel.Add(bookingPersonModel);
                    }
                }
                flightBookingModel.BookingPersonsModel = bookingPersonsModel;

            }
            else
            {
                BookingPersonsModel bookingPersonModel = new BookingPersonsModel();
                bookingPersonsModel.Add(bookingPersonModel);
                flightBookingModel.BookingPersonsModel = bookingPersonsModel;
            }
            return flightBookingModel;
        }
    }
}
