using AdminAPIServices.Context;
using AdminAPIServices.Entities;
using AdminAPIServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Services
{
    public class AdminService : IAdminService
    {
        private readonly AdminContext _adminContext;
        public AdminService(AdminContext adminContext)
        {
            _adminContext = adminContext;
        }


        public List<Airline> GetAllAirlines()
        {
            try
            {
                return _adminContext.Airline.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Airline GetAirline(Guid id)
        {
            try
            {
                Airline airline = _adminContext.Airline.Where(c => c.Id == id).FirstOrDefault();
                if (airline == null)
                {
                    airline = new Airline();
                }
                return airline;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public AirlineModel SaveAirline(AirlineModel airlineModel)
        {
            try
            {
                Airline airline = _adminContext.Airline.Where(c => c.Id == airlineModel.Id).FirstOrDefault();
                if (airline == null)
                {
                    FillAirlineModelToEntity(airlineModel, airline);
                    airlineModel.Id = Guid.NewGuid();
                    airline.Id = airlineModel.Id;
                    airline.Status = true;
                    _adminContext.Airline.Add(airline);
                }
                else
                {
                    FillAirlineModelToEntity(airlineModel, airline);
                    _adminContext.Airline.Update(airline);
                }
                _adminContext.SaveChanges();
                return airlineModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void FillAirlineModelToEntity(AirlineModel airlineModel, Airline airline)
        {
            airline.ContactAddress = airlineModel.ContactAddress;
            airline.ContactNumber = airlineModel.ContactNumber;
            airline.Name = airlineModel.Name;
        }
        public FlightModel ScheduleFlight(FlightModel flightModel)
        {
            try
            {
                Flight flight = _adminContext.Flights.Where(c => c.Id == flightModel.Id).FirstOrDefault();
                if (flight != null)
                {
                    FillFlightModeltoEntity(flightModel, flight);
                    _adminContext.Flights.Update(flight);
                }
                else
                {
                    var checkShedule = _adminContext.Flights.Where(c => c.FlightId == flightModel.FlightId && c.AirlineId == flightModel.AirlineId).FirstOrDefault();
                    if (checkShedule != null)
                    {
                        throw new Exception("Already some flight exists with flight number in this airline .Please choose unique one");
                    }

                    flight = new Flight();
                    flight.Id = Guid.NewGuid();
                    flight.Status = true;
                    FillFlightModeltoEntity(flightModel, flight);
                    _adminContext.Flights.Add(flight);
                }
                _adminContext.SaveChanges();
                return flightModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillFlightModeltoEntity(FlightModel flightModel, Flight flight)
        {
            flight.AirlineId = flightModel.AirlineId;
            flight.FlightId = flightModel.FlightId;
            flight.FromDate = flightModel.FromDate;
            flight.FromLocation = flightModel.FromLocation;

            flight.NonVeg = flightModel.NonVeg;
            flight.Veg = flightModel.Veg;
            flight.NoOfBUSeats = flightModel.NoOfBUSeats;
            flight.NoOfNONBUSeats = flightModel.NoOfNONBUSeats;
            flight.NoOfRows = flightModel.NoOfRows;
            flight.Price = flightModel.Price;
            flight.Remarks = flightModel.Remarks;
            flight.Sheduled = flightModel.Sheduled;

            flight.ToDate = flightModel.ToDate;
            flight.ToLocation = flightModel.ToLocation;
        }
        public Flight GetFlight(Guid id)
        {
            try
            {
                Flight flight = _adminContext.Flights.Where(c => c.Id == id).FirstOrDefault();
                if (flight == null)
                {
                    flight = new Flight();
                }
                return flight;
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

                flightSearchResults.OnDateResults = _adminContext.Flights.Where(c => (c.Sheduled == "Daily" || c.Sheduled == (isWeekend == true ? "Week Ends" : "Week Days") || c.ToDate == flightSearchModel.SearchDate.Value.Date) && c.Status == true).ToList();
                if (flightSearchModel.RoundTripDate != null)
                {
                    bool isReturnDateWeekend = (flightSearchModel.RoundTripDate.Value.DayOfWeek.ToString().ToLower() == "saturday" || flightSearchModel.RoundTripDate.Value.DayOfWeek.ToString().ToLower() == "sunday") ? true : false;
                    flightSearchResults.ReturnDateResults = _adminContext.Flights.Where(c => (c.Sheduled == "Daily" || c.Sheduled == (isReturnDateWeekend == true ? "Week Ends" : "Week Days") || c.ToDate == flightSearchModel.SearchDate.Value.Date) && c.Status == true).ToList();
                }
                return flightSearchResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserRegistrestionModel UserSignUp(UserRegistrestionModel userRegistrestionModel)
        {
            try
            {
                UserRegistrestion userRegistrestion = _adminContext.UserRegistrestion.Where(c => c.Email == userRegistrestionModel.Email).FirstOrDefault();
                if (userRegistrestion != null)
                {
                    throw new Exception("Email is already in use .Please try with other one .");
                }
                else
                {
                    userRegistrestion = new UserRegistrestion();
                    userRegistrestion.Email = userRegistrestionModel.Email;
                    userRegistrestion.Id = Guid.NewGuid();
                    userRegistrestion.Mobile = userRegistrestionModel.Mobile;
                    userRegistrestion.Name = userRegistrestionModel.Name;
                    userRegistrestion.Password = userRegistrestionModel.Password;
                    userRegistrestion.Role = userRegistrestionModel.Role;
                    userRegistrestion.Status = true;
                    userRegistrestionModel.Id = userRegistrestion.Id;
                    _adminContext.UserRegistrestion.Add(userRegistrestion);
                    _adminContext.SaveChanges();
                }
                return userRegistrestionModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserRegistrestion UserLogIn(string email ,string password)
        {
            try
            {
                UserRegistrestion userRegistrestion = _adminContext.UserRegistrestion.Where(c => c.Email.ToLower() == email.ToLower()).FirstOrDefault();
                if (userRegistrestion != null && userRegistrestion.Password == password)
                {

                    return userRegistrestion;
                }
                else
                {
                    throw new Exception("Please enter valid password");
                }
                return userRegistrestion;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
