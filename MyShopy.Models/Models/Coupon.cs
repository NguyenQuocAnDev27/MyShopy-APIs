using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopy.Models.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public decimal Discount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int MaxUsesPerUser { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
