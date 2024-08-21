using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopy.Models.Models
{
    public class UserCoupon
    {
        public int UserCouponId { get; set; }
        public int UserId { get; set; }
        public int CouponId { get; set; }
        public int? OrderId { get; set; }
        public DateTime? UsedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
