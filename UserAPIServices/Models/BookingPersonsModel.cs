using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPIServices.Models
{
    public class BookingPersonsModel
    {
        public Guid? Id { get; set; }
        //[ForeignKey("FlightBookingId")]
        //public FlightBooking FlightBooking { get; set; }
        public Guid? FlightBookingId { get; set; }//uniqueidentifier    
        //[Column("Veg")]
        //public bool Veg { get; set; }//Bit                     null,
        ////[Column("NonVeg")]
        //public bool NonVeg { get; set; }
        //[Column("SeatNo")]
        //[StringLength(500)]
        public int? SeatNo { get; set; }
        //[Column(TypeName = "decimal(16,2)")]
        //public decimal Price { get; set; }
        //[StringLength(500)]
        public string Name { get; set; }
        public int Age { get; set; }
        //public DateTime? DOB { get; set; }
        //[StringLength(50)]
        public string Gender { get; set; }
        //[StringLength(50)]
        //public string Email { get; set; }
        ////[StringLength(50)]
        //public string ContactNumber { get; set; }
    }
}
