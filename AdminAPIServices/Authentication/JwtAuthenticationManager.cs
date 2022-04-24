using AdminAPIServices.Context;
using AdminAPIServices.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdminAPIServices.Authentication
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        //private readonly AdminContext _adminContext;
        private readonly string _key;
        private IConfiguration Configuration;

      
        public JwtAuthenticationManager(/*AdminContext adminContext,*/string key, IConfiguration _configuration)
        {
            //this._adminContext = adminContext;
            this._key = key;
            this.Configuration = _configuration;
        }
        public string Authenticate(string userName, string password)
        {
            //AdminContext adminContext = new AdminContext();
            SqlConnection cn = new SqlConnection(Configuration.GetConnectionString("DatabaseConnection"));
            //var query = $"select JSON_QUERY(ARPN.[Permissions],'$.Tabs')  from Auth.UserPermissionNew as ARPN join Common.CompanyUser as CCU on CCU.Id = ARPN.CompanyUserId where CCU.CompanyId ={companyId} and CCU.Username='{userName}' and ARPN.ModuleDetailId ={moduleDetailId}";
            var query = $"select Status from dbo.UserRegistrestion where Email='{userName}' and [Password]='{password}'";
            SqlCommand cmd = new SqlCommand(query, cn);
            cn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            var permission = rdr[0];
            cn.Close();
            if (permission.Equals(true))
            {


                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(_key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, userName)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            else
            { return null; }
        }
    }
}
