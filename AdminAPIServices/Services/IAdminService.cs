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
    }
}
