using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Entities
{
    public class Airline
    {
        [Column("Id")]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }
        [Column("Name")]
        [StringLength(500)]
        public string Name { get; set; }
        [Column("ContactNumber")]
        [StringLength(500)]
        public string ContactNumber { get; set; }
        [Column("ContactAddress")]
        [StringLength(500)]
        public string ContactAddress { get; set; }
        [Column("Status")]
        public bool Status { get; set; }
        public ICollection<Flight> Flights { get; set; }
    }
}
