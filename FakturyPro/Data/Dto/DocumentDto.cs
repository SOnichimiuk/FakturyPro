using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturyPro.Data.Dto
{
    public class DocumentDto
    {
        public DocumentDto()
        {
            Products = new List<ProductDto>();
        }
        
        public int Id { get; set; }
        public string DocumentNr { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public DateTime CreationDate { get; set; }
        public DocumentState State { get; set; }
        public DateTime SaleDate { get; set; }
        public DocumentType Type { get; set; }
        public ICollection<ProductDto> Products { get; set; }
        public double PriceNetto => Products.Sum(p => p.PriceNetto * p.Quantity);
        public double PriceGross => Products.Sum(p => p.PriceGross * p.Quantity);
    }
}
