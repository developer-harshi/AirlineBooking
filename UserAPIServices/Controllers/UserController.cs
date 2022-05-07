﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPIServices.Authentication;
using UserAPIServices.Models;
using UserAPIServices.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserAPIServices.Controllers
{
    [Authorize]
    [Route("api/v1.0/flight")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public UserController(IUserService userService, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this._userService = userService;
            this._jwtAuthenticationManager = jwtAuthenticationManager;
        }
        [AllowAnonymous]
        [HttpGet("user")]
        public string Hello()
        {
            return "Hello from User API Service";
        }
        [AllowAnonymous]
        [HttpGet("Authenticate")]
        public IActionResult GetAuthentication(string username, string password)
        {
            var token = _jwtAuthenticationManager.Authenticate(username, password);
            if (token == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(token);
            }
        }
        #region Commented By me
        //// GET: api/<UserController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<UserController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<UserController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        #endregion Commented By me
        [HttpPost("booking")]
        //[Route("booking")]
        public ActionResult SaveFlightBooking(FlightBookingModel flightBookingModel)
        {
            try
            {
                return Ok(_userService.SaveFlightBooking(flightBookingModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ticket/{pnr}")]
        //[Route("ticket")]
        public ActionResult GetTicketByPNR(string pnr)
        {
            try
            {
                return Ok(_userService.GetTicketByPNR(pnr));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("booking/history/{emailId}")]
        //[Route("gettickethistory")]
        public ActionResult GetTicketHistory(string emailId)
        {
            try
            {
                return Ok(_userService.GetTicketHistory(emailId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("booking/cancel/{pnr}")]
        //[Route("cancelticket")]
        public ActionResult CancelTicket(string pnr)
        {
            try
            {
                return Ok(_userService.CancelTicket(pnr));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("search")]
        //[Route("search")]
        public ActionResult SerchFlights(FlightSearchModel flightSearchModel)
        {
            try
            {
                return Ok(_userService.SearchFlights(flightSearchModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("flightlu")]
        //[Route("gettickethistory")]
        public ActionResult GetFlightLu()
        {
            try
            {
                return Ok(_userService.FlightLu());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getflightbooking/{id}")]
        //[Route("ticket")]
        public ActionResult GetFlightBooking(Guid id)
        {
            try
            {
                return Ok(_userService.CreateFlightBookingModel(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getempty")]
        //[Route("ticket")]
        public ActionResult GetEmptyPerson()
        {
            try
            {
                return Ok(_userService.GetEmptyPerson());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getdiscount")]
        //[Route("ticket")]
        public ActionResult GetDiscount()
        {
            try
            {
                return Ok(_userService.GetDiscount());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("activeinActiveuser/{tableName}/{id}/{status}")]
        public ActionResult ActiveInActive(string tableName, Guid id, string status)
        {
            try
            {
                return Ok(_userService.ActiveInActive(tableName, id, status));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
