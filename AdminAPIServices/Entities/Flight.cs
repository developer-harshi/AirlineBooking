using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Entities
{
    public class Flight
    {
        [Column("Id")]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }/*uniqueidentifier        not null, */
        [Column("FlightId")]
        [StringLength(500)]
        public string FlightId { get; set; }/*VARCHAR(500)            not null, */
        [ForeignKey("AirlineId")]

        public Airline Airline { get; set; }
        public Guid? AirlineId { get; set; }/*uniqueidentifier        null,*/
        [Column("FromDate")]
        public DateTime? FromDate { get; set; }/*datetime2(7)            null, */
        [Column("ToDate")]
        public DateTime? ToDate { get; set; }/*datetime2(7)            null, */
        [Column("FromLocation")]
        [StringLength(500)]
        public string FromLocation { get; set; }/*VARCHAR(500)            null, */
        [Column("ToLocation")]
        [StringLength(500)]
        public string ToLocation { get; set; }/*VARCHAR(500)            null, */
        [Column("Veg")]
        public bool Veg { get; set; }/*Bit                     null,*/
        [Column("NonVeg")]
        public bool NonVeg { get; set; }/*bit                     null, */
        [Column("NoOfBUSeats")]
        public int NoOfBUSeats { get; set; }/*int                     null, */
        [Column("NoOfNONBUSeats")]
        public int NoOfNONBUSeats { get; set; }/*int                  null, */
        [Column("Remarks")]
        [StringLength(500)]
        public string Remarks { get; set; }/*VARCHAR(500)            null,*/
        [Column("NoOfRows")]
        public int NoOfRows { get; set; }/*int                     null, */
        //[Column("Price")]
        [Column(TypeName = "decimal(16,2)")]
        public decimal Price { get; set; }/*money                   null,*/
        [Column("Sheduled")]
        [StringLength(500)]
        public string Sheduled { get; set; }/*VARCHAR(50)             null,--Daily ,Weekly ,EveryDay,Weekend,WeekDays*/
        [Column("Status")]
        public bool Status { get; set; }/*bit                     null,*/
        [StringLength(500)]
        public string AirlineName { get; set; }
    }
}
//Alter table Flights Add AirlineName NVARCHAR(500) null