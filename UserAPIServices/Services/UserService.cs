using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPIServices.Context;
using UserAPIServices.Entities;
using UserAPIServices.Models;

namespace UserAPIServices.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;
        private IConfiguration _configuration;
        public UserService(UserContext userContext, IConfiguration configuration)
        {
            _userContext = userContext;
            this._configuration = configuration;
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
                    var flight = _userContext.Flights.Where(c => c.Id == flightBookingModel.FlightId).FirstOrDefault();
                    var user = _userContext.UserRegistrestion.Where(c => c.Email == flightBookingModel.RegisteredMailId).FirstOrDefault();
                    flightBookingModel.FlightNumber = flight.FlightId;
                    flightBookingModel.UserRegistrestionId = user.Id;
                    flightBookingModel.FromLocation = flight.FromLocation;
                    flightBookingModel.ToLocation = flight.ToLocation;
                    flightBooking = new FlightBooking();
                    flightBooking.Id = Guid.NewGuid();
                    flightBooking.Status = true;
                    flightBookingModel.Id = flightBooking.Id;
                    flightBooking.PNRNumber = DateTime.UtcNow.Day.ToString() + DateTime.UtcNow.Month.ToString() + "-" + DateTime.UtcNow.Year.ToString() + DateTime.UtcNow.Hour.ToString() + DateTime.UtcNow.Minute.ToString() + DateTime.UtcNow.Millisecond.ToString();
                    FillFlightBookingModelToEntity(flightBookingModel, flightBooking);
                    _userContext.FlightBooking.Add(flightBooking);
                    FillBookingPersonsModeltoEntity(flightBookingModel, true);

                }
                _userContext.SaveChanges();
                return flightBookingModel;
            }
            catch (Exception ex)
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
            flightBooking.Discount = flightBookingModel.Discount;
        }
        public List<TicketSearchModel> GetTicketByPNR(string pnr)
        {
            try
            {
                List<TicketSearchModel> ticketSearchModels = new List<TicketSearchModel>();
                TicketSearchModel ticketSearchModel = new TicketSearchModel();
                List<Persons> lstPersons = new List<Persons>();
                var booking = _userContext.FlightBooking.Where(c => c.PNRNumber == pnr).FirstOrDefault();
                if (booking != null)
                {
                    var persons = _userContext.BookingPersons.Where(c => c.FlightBookingId == booking.Id).ToList();
                    if (persons != null)
                    {
                        FillTicketSearchModel(ticketSearchModel, booking);
                        if (persons != null && persons.Count() > 0)
                        {
                            FillPersons(lstPersons, persons);
                        }
                        ticketSearchModel.Persons = lstPersons;
                        ticketSearchModels.Add(ticketSearchModel);
                    }
                }
                return ticketSearchModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void FillPersons(List<Persons> lstPersons, List<BookingPersons> persons)
        {
            foreach (var bookingPerson in persons)
            {
                Persons bookingPersonModel = new Persons();
                bookingPersonModel.Age = bookingPerson.Age;
                //bookingPersonModel.ContactNumber = bookingPerson.ContactNumber;
                //bookingPersonModel.DOB = bookingPerson.DOB;
                //bookingPersonModel.Email = bookingPerson.Email;
                //bookingPersonModel.FlightBookingId = bookingPerson.FlightBookingId;
                bookingPersonModel.Gender = bookingPerson.Gender;
                bookingPersonModel.Id = bookingPerson.Id;
                bookingPersonModel.FirstName = bookingPerson.Name;
                //bookingPersonModel.NonVeg = bookingPerson.NonVeg;
                //bookingPersonModel.Price = bookingPerson.Price;
                bookingPersonModel.SeatNo = bookingPerson.SeatNo;
                //bookingPersonModel.Veg = bookingPerson.Veg;
                lstPersons.Add(bookingPersonModel);
            }
        }

        private static void FillTicketSearchModel(TicketSearchModel ticketSearchModel, FlightBooking booking)
        {
            //ticketSearchModel.AirlineId = booking.AirlineId;
            ticketSearchModel.ContactNumber = booking.ContactNumber;
            //ticketSearchModel.FlightId = booking.FlightId;
            ticketSearchModel.FlightNumber = booking.FlightNumber;
            ticketSearchModel.FromDate = booking.FromDate??DateTime.UtcNow.Date;
            ticketSearchModel.FromLocation = booking.FromLocation;
            ticketSearchModel.Id = booking.Id;
            //ticketSearchModel.NoOfBUSeats = booking.NoOfBUSeats;
            //ticketSearchModel.NoOfNONBUSeats = booking.NoOfNONBUSeats;
            ticketSearchModel.PNRNumber = booking.PNRNumber;
            ticketSearchModel.RegisteredMailId = booking.RegisteredMailId;
            //ticketSearchModel.Remarks = booking.Remarks;
            ticketSearchModel.SeatNos = booking.SeatNos;
            ticketSearchModel.Status = booking.Status;
            //ticketSearchModel.ToDate = booking.ToDate;
            ticketSearchModel.ToLocation = booking.ToLocation;
            ticketSearchModel.TotalPrice = booking.TotalPrice??0;
            ticketSearchModel.TicketStatus = booking.Status == true ? "booked" : "Cancelled";
        }

        public List<TicketSearchModel> GetTicketHistory(string emailId)
        {
            try
            {
                List<TicketSearchModel> lstTicketSearchModel = new List<TicketSearchModel>();
                var bopokings = _userContext.FlightBooking.Where(c => c.RegisteredMailId == emailId).ToList();
                foreach (var booking in bopokings)
                {
                    TicketSearchModel ticketSearchModel = new TicketSearchModel();
                    FillTicketSearchModel(ticketSearchModel, booking);
                    var persons = _userContext.BookingPersons.Where(c => c.FlightBookingId == booking.Id).ToList();
                    if (persons != null)
                    {
                        List<Persons> lstPersons = new List<Persons>();
                        FillTicketSearchModel(ticketSearchModel, booking);
                        if (persons != null && persons.Count() > 0)
                        {
                            FillPersons(lstPersons, persons);
                        }
                        ticketSearchModel.Persons = lstPersons;

                    }
                    lstTicketSearchModel.Add(ticketSearchModel);
                }
                return lstTicketSearchModel;
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

        public FlightBookingModel CreateFlightBookingModel(Guid id,Guid? flightId)
        {
            FlightBookingModel flightBookingModel = new FlightBookingModel();
            List<BookingPersonsModel> bookingPersonsModel = new List<BookingPersonsModel>();
            var flightBooking = _userContext.FlightBooking.Where(c => c.Id == id).AsQueryable();
            if (flightBooking != null && flightBooking.Count() > 0)
            {
                FlightBooking flightBooking1 = flightBooking.FirstOrDefault();
                List<BookingPersons> lstBookingPersons = flightBooking1.BookingPersons.ToList();
                flightBookingModel.AirlineId = flightBooking1.AirlineId;
                flightBookingModel.ContactNumber = flightBooking1.ContactNumber;
                flightBookingModel.FlightId = flightBooking1.FlightId;
                flightBookingModel.FlightNumber = flightBooking1.FlightNumber;
                //flightBookingModel.FromDate = flightBooking1.FromDate;
                flightBookingModel.FromLocation = flightBooking1.FromLocation;
                flightBookingModel.Id = flightBooking1.Id;
                //flightBookingModel.NoOfBUSeats = flightBooking1.NoOfBUSeats;
                //flightBookingModel.NoOfNONBUSeats = flightBooking1.NoOfNONBUSeats;
                flightBookingModel.PNRNumber = flightBooking1.PNRNumber;
                flightBookingModel.RegisteredMailId = flightBooking1.RegisteredMailId;
                flightBookingModel.Remarks = flightBooking1.Remarks;
                flightBookingModel.SeatNos = flightBooking1.SeatNos;
                flightBookingModel.Status = flightBooking1.Status;
                //flightBookingModel.ToDate = flightBooking1.ToDate;
                flightBookingModel.ToLocation = flightBooking1.ToLocation;
                //flightBookingModel.TotalPrice = flightBooking1.TotalPrice;
                flightBookingModel.UserRegistrestionId = flightBooking1.UserRegistrestionId;
                if (lstBookingPersons != null && lstBookingPersons.Count() > 0)
                {
                    foreach (var bookingPerson in lstBookingPersons)
                    {
                        BookingPersonsModel bookingPersonModel = new BookingPersonsModel();
                        bookingPersonModel.Age = bookingPerson.Age;
                        //bookingPersonModel.ContactNumber = bookingPerson.ContactNumber;
                        //bookingPersonModel.DOB = bookingPerson.DOB;
                        //bookingPersonModel.Email = bookingPerson.Email;
                        bookingPersonModel.FlightBookingId = bookingPerson.FlightBookingId;
                        bookingPersonModel.Gender = bookingPerson.Gender;
                        bookingPersonModel.Id = bookingPerson.Id;
                        bookingPersonModel.Name = bookingPerson.Name;
                        //bookingPersonModel.NonVeg = bookingPerson.NonVeg;
                        //bookingPersonModel.Price = bookingPerson.Price;
                        bookingPersonModel.SeatNo = bookingPerson.SeatNo;
                        //bookingPersonModel.Veg = bookingPerson.Veg;
                        bookingPersonsModel.Add(bookingPersonModel);
                    }
                }
                flightBookingModel.BookingPersonsModel = bookingPersonsModel;

            }
            else
            {
                BookingPersonsModel bookingPersonModel = new BookingPersonsModel();
                Flight flight = _userContext.Flights.Where(c => c.Id == flightId).FirstOrDefault();
                if(flight!=null)
                {
                    flightBookingModel.FlightId = flight.Id;
                    flightBookingModel.FlightNumber = flight.FlightId;
                    flightBookingModel.FlightPrice = flight.Price;
                    flightBookingModel.AirlineId = flight.AirlineId;
                    flightBookingModel.AirlineName = flight.AirlineName;
                }
                bookingPersonsModel.Add(bookingPersonModel);
                flightBookingModel.BookingPersonsModel = bookingPersonsModel;
            }
            return flightBookingModel;
        }
        public void FillBookingPersonsModeltoEntity(FlightBookingModel flightBookingModel, bool isAdd)
        {
            foreach (var bookingPersonModel in flightBookingModel.BookingPersonsModel)
            {
                //BookingPersonsModel bookingPersonModel = new BookingPersonsModel();
                BookingPersons bookingPerson = new BookingPersons();
                bookingPerson.Id = Guid.NewGuid();
                bookingPerson.Age = bookingPersonModel.Age;
                //bookingPerson.ContactNumber = bookingPersonModel.ContactNumber;
                //bookingPerson.DOB = bookingPersonModel.DOB;
                //bookingPerson.Email = bookingPersonModel.Email;
                bookingPerson.FlightBookingId = flightBookingModel.Id;
                bookingPerson.Gender = bookingPersonModel.Gender;

                bookingPerson.Name = bookingPersonModel.Name;
                //bookingPerson.NonVeg = bookingPersonModel.NonVeg;
                //bookingPerson.Price = bookingPersonModel.Price;
                bookingPerson.SeatNo = bookingPersonModel.SeatNo??0;
                //bookingPerson.Veg = bookingPersonModel.Veg;
                _userContext.BookingPersons.Add(bookingPerson);


            }
        }
        public List<FlightModel> FlightLu()
        {
            List<FlightModel> flightModels = new List<FlightModel>();
            var flights = _userContext.Flights.Where(c => c.Status == true).ToList();
            if (flights != null && flights.Count > 0)
            {
                foreach (var flight in flights)
                {
                    FlightModel flightModel = new FlightModel();
                    flightModel.AirlineId = flight.AirlineId;
                    flightModel.FlightNumber = flight.FlightId;
                    flightModel.FlightId = flight.Id;
                    flightModels.Add(flightModel);
                }
            }
            return flightModels;
        }

        public BookingPersonsModel GetEmptyPerson()
        {
            BookingPersonsModel bookingPersonsModel = new BookingPersonsModel();
            return bookingPersonsModel;
        }
        public List<DiscountModel> GetDiscount()
        {
            List<DiscountModel> discountModels = new List<DiscountModel>();
            var discounts = _userContext.Discount.Where(c => c.Status == true).ToList();
            foreach (var discount in discounts)
            {
                DiscountModel discountModel = new DiscountModel();
                discountModel.Name = discount.CouponName;
                discountModel.Value = discount.Value;
                discountModels.Add(discountModel);
            }
            return discountModels;
        }
        public bool ActiveInActive(string tableName,Guid id ,string status)
        {
            try
            {
                int changedStatus = (status.ToLower() == "inactive") ? 1 : 0;
                SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
                //var query = $"select Status from dbo.UserRegistrestion where Email='{userName}' and [Password]='{password}'";
                var query = $"update '{tableName}' set status='{changedStatus}' where Id='{id}'";
                SqlCommand cmd = new SqlCommand(query, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
