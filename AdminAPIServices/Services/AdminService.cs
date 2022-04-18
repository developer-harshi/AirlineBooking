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
            return _adminContext.Airline.ToList();
        }
        public Airline GetAirline(Guid id)
        {
            Airline airline = _adminContext.Airline.Where(c => c.Id == id).FirstOrDefault();
            if (airline == null)
            {
                airline = new Airline();
            }
            return airline;
        }


        public AirlineModel SaveAirline(AirlineModel airlineModel)
        {
            Airline airline = _adminContext.Airline.Where(c => c.Id == airlineModel.Id).FirstOrDefault();
            if (airline != null)
            {

            }
            else
            {

            }
            _adminContext.SaveChanges();
            return airlineModel;
        }
    }
}
