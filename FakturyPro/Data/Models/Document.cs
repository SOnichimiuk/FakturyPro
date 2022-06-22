using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturyPro.Data.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string DocumentNr { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Product> Products{ get; set; }
        public DocumentState State { get; set; }
        public DateTime SaleDate { get; set; }
        public DocumentType Type { get; set; }
    }
}
