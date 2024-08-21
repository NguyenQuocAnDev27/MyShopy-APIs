using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopy.Models.Models
{
    public class ProductImage
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
