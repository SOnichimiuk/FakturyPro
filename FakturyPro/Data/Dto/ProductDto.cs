using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturyPro.Data.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public VatRate VatRate { get; set; }
        public double PriceNetto { get; set; }
        public double Quantity { get; set; }
    }
}
