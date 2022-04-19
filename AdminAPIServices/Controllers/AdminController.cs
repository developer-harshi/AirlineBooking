﻿using AdminAPIServices.Models;
using AdminAPIServices.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminAPIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly IAdminService _adminSrvice;
        public AdminController(IAdminService adminService)
        {
            this._adminSrvice = adminService;
        }

        // GET: api/<AdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AdminController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpGet]
        [Route("getallairlines")]
        public ActionResult GetAllAirlines()
        {
            try
            {
                return Ok(_adminSrvice.GetAllAirlines());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("getairline")]
        public ActionResult GetAirline(Guid id)
        {
            try
            {
                return Ok(_adminSrvice.GetAirline(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        [Route("airlineregister")]
        public ActionResult SaveAirline(AirlineModel airlineModel)
        {
            try
            {
                return Ok(_adminSrvice.SaveAirline(airlineModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        [Route("scheduleflight")]
        public ActionResult ScheduleFlight(FlightModel flightModel)
        {
            try
            {
                return Ok(_adminSrvice.ScheduleFlight(flightModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Route("getflight")]
        public ActionResult GetFlight(Guid id)
        {
            try
            {
                return Ok(_adminSrvice.GetFlight(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        [Route("searchflight")]
        public ActionResult SerchFlights(FlightSearchModel flightSearchModel)
        {
            try
            {
                return Ok(_adminSrvice.SearchFlights(flightSearchModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        [Route("login")]
        public ActionResult UserLogin(LoginModel loginModel)
        {
            try
            {
                return Ok(_adminSrvice.UserLogIn(loginModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        [Route("register")]
        public ActionResult UserSignUp(UserRegistrestionModel userRegistrestionModel)
        {
            try
            {
                return Ok(_adminSrvice.UserSignUp(userRegistrestionModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
