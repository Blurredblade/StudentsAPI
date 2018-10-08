using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.Models
{
    public class Invoice
    {
        public int inv_Num { get; set; }
        public string inv_Date { get; set; }
        public List<LineItem> lineItems { get; set; }
    }
}
