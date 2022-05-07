using AdminAPIServices.Entities;
using AdminAPIServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Services
{
    public interface IAdminService
    {
        List<AirlineModel> GetAllAirlines();
        AirlineModel GetAirline(Guid id);
        AirlineModel SaveAirline(AirlineModel airlineModel);
        FlightModel ScheduleFlight(FlightModel flightModel);
        FlightModel GetFlight(Guid id);
        
        UserRegistrestionModel UserSignUp(UserRegistrestionModel userRegistrestionModel);
        UserRegistrestion UserLogIn(LoginModel loginModel);
        List<FlightModel> GetAllFlights();
        List<AirlineLu> GetAirlineLu();
        DiscountModel GetDiscount(Guid id);
        List<DiscountModel> GetDiscounts();
        DiscountModel SaveDiscount(DiscountModel discountModel);
        bool ActiveInActive(string tableName, Guid id, string status);
    }
}
