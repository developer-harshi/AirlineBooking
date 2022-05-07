using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPIServices.Entities
{
    public class FlightBooking
    {
        [Column("Id")]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }//uniqueidentifier        not null, 
        [ForeignKey("FlightId")]
        public Flight Flight { get; set; }
        public Guid? FlightId { get; set; }//uniqueidentifier         null,

        public string FlightNumber { get; set; }     //VARCHAR(500)            not null, 
        [ForeignKey("AirlineId")]
        public Airline Airline { get; set; }
        public Guid? AirlineId { get; set; }//uniqueidentifier        null,
        [Column("FromDate")]
        public DateTime? FromDate { get; set; }         //datetime2(7)            null, 
        [Column("ToDate")]
        public DateTime? ToDate { get; set; }//datetime2(7)            null, 
        [Column("FromLocation")]
        [StringLength(500)]
        public string FromLocation { get; set; }//VARCHAR(500)            null, 
        [Column("ToLocation")]
        [StringLength(500)]
        public string ToLocation { get; set; }//VARCHAR(500)            null, 
                //bit                     null, 
        [Column("NoOfBUSeats")]
        public int? NoOfBUSeats { get; set; }              //     null, 
        [Column("NoOfNONBUSeats")]
        public int? NoOfNONBUSeats { get; set; }               //  null, 
        [Column("Remarks")]
        [StringLength(500)]
        public string Remarks { get; set; }//VARCHAR(500)            null, 
        
                  //  null, 
        //[Column("Price")]
        [Column(TypeName = "decimal(16,2)")]
        public decimal? TotalPrice { get; set; }//money                   null,
        [Column("PNRNumber")]
        [StringLength(500)]
        public string PNRNumber { get; set; }        // varchar(300)            null, 
        [Column("MailId")]
        [StringLength(500)]
        public string RegisteredMailId { get; set; }//varchar(300)            null, 
        [Column("ContactNumber")]
        [StringLength(500)]
        public string ContactNumber { get; set; }//varchar(300)            null, 

        public UserRegistrestion UserRegistrestion { get; set; }
        [Column("UserRegistrestionId")]
        //[StringLength(500)]
        public Guid? UserRegistrestionId { get; set; }//uniqueIdentifier        null 
        [Column("Status")]
        public bool Status { get; set; }//bit                     null,
        public string SeatNos { get; set; }
        public decimal? Discount { get; set; }
        public ICollection<BookingPersons> BookingPersons { get; set; }
    }
}
//Alter table FlightBooking alter column  seatno NVARCHAR(500) null