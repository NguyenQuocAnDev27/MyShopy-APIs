using MyShopy.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyShopy.Models.Models
{
    public class ProductStatus
    {
        public int StatusId { get; set; }
        public string Usid { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }
        public bool Buyable { get; set; }
        public bool Shippable { get; set; }
        public bool Active { get; set; }
        [JsonConverter(typeof(Global.CustomDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        [JsonConverter(typeof(Global.CustomDateTimeConverter))]
        public DateTime UpdatedAt { get; set; }
    }
}
