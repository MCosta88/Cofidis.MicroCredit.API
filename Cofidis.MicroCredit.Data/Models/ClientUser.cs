using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCredit.Data.Models
{
    public class ClientUser
    {
        public string Name { get; set; }
        public string NIF { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public IEnumerable<ClientLoan> ClientLoans { get; set; }
    }
}
