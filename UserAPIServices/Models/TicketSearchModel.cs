using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPIServices.Models
{
    public class TicketSearchModel
    {

        public Guid Id { get; set; }//uniqueidentifier        not null, 



        public string FlightNumber { get; set; }     //VARCHAR(500)            not null, 

        public string AirlineName { get; set; }
        

        public DateTime FromDate { get; set; }         //datetime2(7)            null, 

        public DateTime ToDate { get; set; }//datetime2(7)            null, 

        public string FromLocation { get; set; }//VARCHAR(500)            null, 

        public string ToLocation { get; set; }//VARCHAR(500)            null, 
                                              //bit                     null, 






        public decimal TotalPrice { get; set; }//money                   null,

        public string PNRNumber { get; set; }        // varchar(300)            null, 

        public string RegisteredMailId { get; set; }//varchar(300)            null, 

        public string ContactNumber { get; set; }//varchar(300)            null, 
                                                 //[Column("UserRegistrestionId")]
                                                 //[StringLength(500)]


        public bool Status { get; set; }//bit                     null,
        public string SeatNos { get; set; }
        public string TicketStatus { get; set; }
        public List<Persons> Persons { get; set; }
    }
    public class Persons
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public int SeatNo { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

    }
}
