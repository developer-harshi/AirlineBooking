using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Models
{
    public class UserRegistrestionModel
    {

        public Guid? Id { get; set; }/*uniqueidentifier        not null, */

        public string Name { get; set; }/*VARCHAR(500)            not null, */

        public string Mobile { get; set; }/*Varchar(100)            null, */

        public string Email { get; set; }/*Varchar(1000)           null, */

        public string Role { get; set; }

        public bool Status { get; set; }/*bit                     null */

        public string Password { get; set; }
    }
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

    }
}
