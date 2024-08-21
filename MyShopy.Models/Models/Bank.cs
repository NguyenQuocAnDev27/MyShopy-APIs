using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopy.Models.Models
{
    public class Bank
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankingLogoPath { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
