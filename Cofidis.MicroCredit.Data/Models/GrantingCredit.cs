using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCredit.Data.Models
{
    public class GrantingMicroCredit
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string CustomerNif { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }


    }
}
