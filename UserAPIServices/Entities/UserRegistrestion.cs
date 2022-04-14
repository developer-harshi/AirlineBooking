using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPIServices.Entities
{
    public class UserRegistrestion
    {
        public Guid Id { get; set; }/*uniqueidentifier        not null, */

        public string Name { get; set; }/*VARCHAR(500)            not null, */

        public string Mobile { get; set; }/*Varchar(100)            null, */

        public string Email { get; set; }/*Varchar(1000)           null, */

        public bool Status { get; set; }/*bit                     null */
    }
}
