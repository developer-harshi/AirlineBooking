using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Models
{
    public class AirlineModel
    {
       
        public Guid Id { get; set; }
      
        public string Name { get; set; }
       
        public string ContactNumber { get; set; }
       
        public string ContactAddress { get; set; }
       
        public string Status { get; set; }
    }
}
