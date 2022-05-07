using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPIServices.Entities
{
    public class Discount
    {
        public Guid? Id { get; set; }
        public string CouponName { get; set; }
        public decimal? Value { get; set; }
        public bool? Status { get; set; }
    }
}
