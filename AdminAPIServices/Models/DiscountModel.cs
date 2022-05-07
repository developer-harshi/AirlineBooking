using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Models
{
    public class DiscountModel
    {
        public Guid? Id { get; set; }
        public string CouponName { get; set; }
        public decimal? Value { get; set; }
        public string Status { get; set; }
    }
}
