using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Models
{
    public class FlightModel
    {

        public Guid? Id { get; set; }/*uniqueidentifier        not null, */

        public string FlightId { get; set; }/*VARCHAR(500)            not null, */



        public Guid? AirlineId { get; set; }/*uniqueidentifier        null,*/

        public DateTime? FromDate { get; set; }/*datetime2(7)            null, */

        public DateTime? ToDate { get; set; }/*datetime2(7)            null, */

        public string FromLocation { get; set; }/*VARCHAR(500)            null, */

        public string ToLocation { get; set; }/*VARCHAR(500)            null, */

        public bool? Veg { get; set; }/*Bit                     null,*/

        public bool? NonVeg { get; set; }/*bit                     null, */

        public int NoOfBUSeats { get; set; }/*int                     null, */

        public int NoOfNONBUSeats { get; set; }/*int                  null, */

        public string Remarks { get; set; }/*VARCHAR(500)            null,*/

        public int NoOfRows { get; set; }/*int                     null, */
        //[Column("Price")]

        public decimal? Price { get; set; }/*money                   null,*/

        public string? Sheduled { get; set; }/*VARCHAR(50)             null,--Daily ,Weekly ,EveryDay,Weekend,WeekDays*/

        public string Status { get; set; }/*bit                     null,*/
        public string? AirlineName { get; set; }
    }
    public class AirlineLu
    {
        public string AirlineName { get; set; }
        public Guid? AirlineId { get; set; }
    }
}
