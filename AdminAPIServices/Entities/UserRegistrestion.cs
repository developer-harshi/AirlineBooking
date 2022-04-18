using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Entities
{
    public class UserRegistrestion
    {
        [Column("Id")]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }/*uniqueidentifier        not null, */
        [Column("Name")]
        [StringLength(500)]
        public string Name { get; set; }/*VARCHAR(500)            not null, */
        [Column("Mobile")]
        [StringLength(500)]
        public string Mobile { get; set; }/*Varchar(100)            null, */
        [Column("Email")]
        [StringLength(500)]
        public string Email { get; set; }/*Varchar(1000)           null, */
        [Column("Role")]
        [StringLength(100)]
        public string Role { get; set; }
        [Column("Status")]
        public bool Status { get; set; }/*bit                     null */
        [Column("Password")]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
