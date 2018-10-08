using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.Models
{
    public class Customer
    {
        public int Cus_Code { get; set; }
        public string Cus_Fname { get; set; }
        public string Cus_Lname { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
