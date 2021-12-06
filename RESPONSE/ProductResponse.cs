using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESPONSE
{
    public class ProductResponse
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime DateOfExpiry { get; set; }
        public float Discount { get; set; }
    }
}
