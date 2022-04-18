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
                    FillAirlineFromAirlineModel(airlineModel, airline);
                    airlineModel.Id = Guid.NewGuid();
                    airline.Id = airlineModel.Id;
                    airline.Status = true;
                    _adminContext.Airline.Add(airline);
                }
                else
                {
                    FillAirlineFromAirlineModel(airlineModel, airline);
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

        private static void FillAirlineFromAirlineModel(AirlineModel airlineModel, Airline airline)
        {
            airline.ContactAddress = airlineModel.ContactAddress;
            airline.ContactNumber = airlineModel.ContactNumber;
            airline.Name = airlineModel.Name;
        }
    }
}
