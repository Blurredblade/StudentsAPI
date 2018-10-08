using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.Models
{
    public class Invoice
    {
        public int Inv_Number { get; set; }
        public DateTime Inv_Date { get; set; }
        public List<LineItem> LineItems { get; set; }
    }
}
