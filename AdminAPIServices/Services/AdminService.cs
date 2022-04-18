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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public AirlineModel SaveAirline(AirlineModel airlineModel)
        {
            try
            {
                Airline airline = _adminContext.Airline.Where(c => c.Id == airlineModel.Id).FirstOrDefault();
                if (airline != null)
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
                throw  ex;
            }

        }

        private  void FillAirlineModelToEntity(AirlineModel airlineModel, Airline airline)
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
            catch(Exception ex)
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
        public List<Flight> SearchFlights()
        {
            try
            {
                return _adminContext.Flights.Where(c => c.Status == true).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
