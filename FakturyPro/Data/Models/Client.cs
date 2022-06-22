using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturyPro.Data.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string NIP { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }    
}
