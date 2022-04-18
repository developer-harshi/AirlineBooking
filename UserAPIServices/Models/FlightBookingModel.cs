using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPIServices.Models
{
    public class FlightBookingModel
    {

        public Guid Id { get; set; }//uniqueidentifier        not null, 


        public Guid? FlightId { get; set; }//uniqueidentifier         null,

        public string FlightNumber { get; set; }     //VARCHAR(500)            not null, 


        public Guid? AirlineId { get; set; }//uniqueidentifier        null,

        public DateTime FromDate { get; set; }         //datetime2(7)            null, 

        public DateTime ToDate { get; set; }//datetime2(7)            null, 

        public string FromLocation { get; set; }//VARCHAR(500)            null, 

        public string ToLocation { get; set; }//VARCHAR(500)            null, 

        public bool Veg { get; set; }//Bit                     null,

        public bool NonVeg { get; set; }            //bit                     null, 

        public int NoOfBUSeats { get; set; }              //     null, 

        public int NoOfNONBUSeats { get; set; }               //  null, 

        public string Remarks { get; set; }//VARCHAR(500)            null, 

        public int SeatNo { get; set; }                //  null, 
                                                       //[Column("Price")]

        public decimal Price { get; set; }//money                   null,

        public string PNRNumber { get; set; }        // varchar(300)            null, 

        public string MailId { get; set; }//varchar(300)            null, 

        public string ContactNumber { get; set; }//varchar(300)            null, 

        public string UserRegistrestionId { get; set; }//uniqueIdentifier        null 

        public bool Status { get; set; }//bit                     null,
    }
}
