using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCredit.Services.Interfaces
{
    public interface IMicroCreditService
    {
        Task<decimal> GrantingMicroCredit(decimal baseSalary);
    }
}
