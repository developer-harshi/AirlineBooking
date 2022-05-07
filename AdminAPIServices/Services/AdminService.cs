using AdminAPIServices.Context;
using AdminAPIServices.Entities;
using AdminAPIServices.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Services
{
    public class AdminService : IAdminService
    {
        private readonly AdminContext _adminContext;
        private IConfiguration _configuration;
        public AdminService(AdminContext adminContext, IConfiguration configuration)
        {
            _adminContext = adminContext;
            this._configuration = configuration;
        }


        public List<AirlineModel> GetAllAirlines()
        {
            try
            {
                List<AirlineModel> airlineModels = new List<AirlineModel>();
                var airlines = _adminContext.Airline.ToList();
                if (airlines != null)
                {
                    foreach (var airline in airlines)
                    {
                        AirlineModel airlineModel = new AirlineModel();
                        airlineModel.ContactAddress = airline.ContactAddress;
                        airlineModel.ContactNumber = airline.ContactNumber;
                        airlineModel.Id = airline.Id;
                        airlineModel.Name = airline.Name;
                        airlineModel.Status = airline.Status == false ? "InActive" : "Active";
                        airlineModels.Add(airlineModel);
                    }
                }
                return airlineModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public AirlineModel GetAirline(Guid id)
        {
            try
            {
                AirlineModel airlineModel = new AirlineModel();
                Airline airline = _adminContext.Airline.Where(c => c.Id == id).FirstOrDefault();
                if(airline!=null)
                {
                    airlineModel.ContactAddress = airline.ContactAddress;
                    airlineModel.ContactNumber = airline.ContactNumber;
                    airlineModel.Id = airline.Id;
                    airlineModel.Name = airline.Name;
                    airlineModel.Status = airline.Status == false ? "InActive" : "Active";
                }
                
                return airlineModel;
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
                    if(_adminContext.Airline.Where(c => c.Name.ToLower() == airlineModel.Name.ToLower()).FirstOrDefault()!=null)
                    {
                        throw new Exception("Airline name already exist ! .");
                    }
                    airline = new Airline();
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
                    flight = new Flight();
                    var checkShedule = _adminContext.Flights.Where(c => c.FlightId == flightModel.FlightId && c.AirlineId == flightModel.AirlineId).FirstOrDefault();
                    Airline airline = _adminContext.Airline.Where(c => c.Id == flightModel.AirlineId).FirstOrDefault();
                    if(airline!=null)
                    {
                        flightModel.AirlineName = airline.Name;
                        flight.AirlineName= airline.Name;
                    }
                    if (checkShedule != null)
                    {
                        throw new Exception("Already some flight exists with flight number in this airline .Please choose unique one");
                    }

                    
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
            flight.FromDate = flightModel.FromDate??DateTime.UtcNow.Date;
            flight.FromLocation = flightModel.FromLocation;

            flight.NonVeg = flightModel.NonVeg??false;
            flight.Veg = flightModel.Veg??false;
            flight.NoOfBUSeats = flightModel.NoOfBUSeats;
            flight.NoOfNONBUSeats = flightModel.NoOfNONBUSeats;
            flight.NoOfRows = flightModel.NoOfRows;
            flight.Price = flightModel.Price??0;
            flight.Remarks = flightModel.Remarks;
            flight.Sheduled = flightModel.Sheduled;

            flight.ToDate = flightModel.ToDate ?? DateTime.UtcNow.Date;
            flight.ToLocation = flightModel.ToLocation;
        }
        public FlightModel GetFlight(Guid id)
        {
            try
            {
                FlightModel flightModel = new FlightModel();
                Flight flight = _adminContext.Flights.Where(c => c.Id == id).FirstOrDefault();
                if (flight != null)
                {
                    flightModel.Id = flight.Id;
                    flightModel.Status = flight.Status==false?"InActive":"Active";
                    flightModel.AirlineId = flight.AirlineId;
                    flightModel.FlightId = flight.FlightId;
                    flightModel.FromDate = flight.FromDate ?? DateTime.UtcNow.Date;
                    flightModel.FromLocation = flight.FromLocation;

                    flightModel.NonVeg = flight.NonVeg ;
                    flightModel.Veg = flight.Veg ;
                    flightModel.NoOfBUSeats = flight.NoOfBUSeats;
                    flightModel.NoOfNONBUSeats = flight.NoOfNONBUSeats;
                    flightModel.NoOfRows = flight.NoOfRows;
                    flightModel.Price = flight.Price ;
                    flightModel.Remarks = flight.Remarks;
                    flightModel.Sheduled = flight.Sheduled;

                    flightModel.ToDate = flight.ToDate ?? DateTime.UtcNow.Date;
                    flightModel.ToLocation = flight.ToLocation;
                }
                return flightModel;
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
        public UserRegistrestion UserLogIn(LoginModel loginModel)
        {
            try
            {
                UserRegistrestion userRegistrestion = _adminContext.UserRegistrestion.Where(c => c.Email.ToLower() == loginModel.Email.ToLower()).FirstOrDefault();
                if (userRegistrestion == null)
                {
                    throw new Exception("No user exist with this email.Please enter valid email");
                }
                if (userRegistrestion != null && userRegistrestion.Password == loginModel.Password)
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
        public List<FlightModel> GetAllFlights()
        {
            try
            {
                List<FlightModel> flightModels = new List<FlightModel>();
                var flights = _adminContext.Flights.ToList();
                foreach (var flight in flights)
                {
                    FlightModel flightModel = new FlightModel();
                    flightModel.Id = flight.Id;
                    flightModel.Status = flight.Status == false ? "InActive" : "Active";
                    flightModel.AirlineId = flight.AirlineId;
                    flightModel.FlightId = flight.FlightId;
                    flightModel.FromDate = flight.FromDate ?? DateTime.UtcNow.Date;
                    flightModel.FromLocation = flight.FromLocation;

                    flightModel.NonVeg = flight.NonVeg;
                    flightModel.Veg = flight.Veg;
                    flightModel.NoOfBUSeats = flight.NoOfBUSeats;
                    flightModel.NoOfNONBUSeats = flight.NoOfNONBUSeats;
                    flightModel.NoOfRows = flight.NoOfRows;
                    flightModel.Price = flight.Price;
                    flightModel.Remarks = flight.Remarks;
                    flightModel.Sheduled = flight.Sheduled;

                    flightModel.ToDate = flight.ToDate ?? DateTime.UtcNow.Date;
                    flightModel.ToLocation = flight.ToLocation;
                    flightModel.AirlineName = flight.AirlineName;
                    flightModels.Add(flightModel);
                }
                return flightModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AirlineLu> GetAirlineLu()
        {
            List<AirlineLu> lookup = new List<AirlineLu>();
            var airlines = _adminContext.Airline.ToList();
            if (airlines != null && airlines.Count > 0)
            {
                foreach (var airline in airlines)
                {
                    AirlineLu airlineLu = new AirlineLu();
                    airlineLu.AirlineName = airline.Name;
                    airlineLu.AirlineId = airline.Id;
                    lookup.Add(airlineLu);
                }
            }
            return lookup;
        }
        public DiscountModel GetDiscount(Guid id)
        {
            DiscountModel discountModel = new DiscountModel();
            Discount discount = _adminContext.Discount.Where(c => c.Id == id).FirstOrDefault();
            if (discount != null)
            {
                discountModel.Id = discount.Id;
                discountModel.CouponName = discount.CouponName;
                discountModel.Value = discount.Value;
                discountModel.Status = discount.Status == true ? "Active" : "InActive";
            }
            return discountModel;
        }
        public List<DiscountModel> GetDiscounts()
        {
            List<DiscountModel> discountModels = new List<DiscountModel>();
            var discounts = _adminContext.Discount.ToList();
            foreach (var discount in discounts)
            {
                DiscountModel discountModel = new DiscountModel();
                discountModel.Id = discount.Id;
                discountModel.CouponName = discount.CouponName;
                discountModel.Value = discount.Value;
                discountModel.Status = discount.Status == true ? "Active" : "InActive";
                discountModels.Add(discountModel);
            }
            return discountModels;
        }
        public DiscountModel SaveDiscount(DiscountModel discountModel)
        {
            Discount discount = _adminContext.Discount.Where(c => c.Id == discountModel.Id).FirstOrDefault();
            if (discount != null)
            {
                //discountModel.Id = discount.Id;
                discount.CouponName = discountModel.CouponName;
                discount.Value = discountModel.Value;
                //discountModel.Status = discount.Status == true ? "Active" : "InActive";  
                _adminContext.Discount.Update(discount);

            }
            else
            {
                discount = new Discount();
                discountModel.Id = Guid.NewGuid();
                discount.Id = discountModel.Id;
                discount.CouponName = discountModel.CouponName;
                discount.Value = discountModel.Value;
                discount.Status = true;
                _adminContext.Discount.Add(discount);
            }
            _adminContext.SaveChanges();
            return discountModel;
        }
        public bool ActiveInActive(string tableName, Guid id, string status)
        {
            try
            {
                int changedStatus = (status.ToLower() == "inactive") ? 1 : 0;
                SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
                //var query = $"select Status from dbo.UserRegistrestion where Email='{userName}' and [Password]='{password}'";
                var query = $"update {tableName} set status={changedStatus} where Id='{id}'";
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
