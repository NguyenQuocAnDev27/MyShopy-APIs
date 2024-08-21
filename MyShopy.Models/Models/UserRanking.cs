using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopy.Models.Models
{
    public class UserRanking
    {
        public int UserId { get; set; }
        public int RankingId { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
