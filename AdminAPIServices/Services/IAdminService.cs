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
        List<Airline> GetAllAirlines();
        Airline GetAirline(Guid id);
        AirlineModel SaveAirline(AirlineModel airlineModel);
        FlightModel ScheduleFlight(FlightModel flightModel);
        Flight GetFlight(Guid id);
        FlightSearchResults SearchFlights(FlightSearchModel flightSearchModel);
        UserRegistrestionModel UserSignUp(UserRegistrestionModel userRegistrestionModel);
        UserRegistrestion UserLogIn(LoginModel loginModel);
    }
}
