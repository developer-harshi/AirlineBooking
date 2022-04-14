using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Entities
{
    public class Airline
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string ContactNumber { get; set; }

        public string ContactAddress { get; set; }

        public bool Status { get; set; }
    }
}
