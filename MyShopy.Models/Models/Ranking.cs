using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopy.Models.Models
{
    public class Ranking
    {
        public int RankingId { get; set; }
        public string RankName { get; set; }
        public int MinOrders { get; set; }
        public decimal MinSpent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
