using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPIServices.Entities;

namespace UserAPIServices.Models
{
    public class FlightSearchModel
    {
        public DateTime? SearchDate { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public DateTime? RoundTripDate { get; set; }
    }
    public class FlightSearchResults
    {
        public List<Flight> OnDateResults { get; set; }
        public List<Flight> ReturnDateResults { get; set; }
    }
}
