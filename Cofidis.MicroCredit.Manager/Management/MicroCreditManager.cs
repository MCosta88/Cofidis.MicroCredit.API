using Cofidis.MicroCredit.Manager.Interfaces;
using Cofidis.MicroCredit.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCredit.Manager.Management
{
    public class MicroCreditManager : IMicroCreditManager
    {
        private readonly ILogger<MicroCreditManager> _logger;

        private readonly IMicroCreditService _microCreditService;
        public MicroCreditManager(ILogger<MicroCreditManager> logger, IMicroCreditService microCreditService)
        {
            _logger = logger;
            _microCreditService = microCreditService;
        }
        public async Task<decimal> GrantingMicroCredit(decimal baseSalary)
        {
            var result = await _microCreditService.GrantingMicroCredit(baseSalary);
            return result;
        }
    }
}
