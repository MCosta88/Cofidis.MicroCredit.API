using Cofidis.MicroCredit.Data.Interfaces;
using Microsoft.Extensions.Logging;
using Cofidis.MicroCredit.Services.Interfaces;

namespace Cofidis.MicroCredit.Services.Services
{
    public class MicroCreditService(ILogger<MicroCreditService> logger, IMicroCreditRepository microCreditRepository) : IMicroCreditService
    {
        public async Task<decimal> GrantingMicroCredit(decimal baseSalary)
        {
            if (baseSalary <= 0)
            {
                logger.LogWarning("[MicroCreditService - GrantingMicroCredit] -> Invalid baseSalary: {BaseSalary}", baseSalary);
                throw new ArgumentException("Base salary must be greater than zero.", nameof(baseSalary));
            }

            logger.LogInformation("[MicroCreditService - GrantingMicroCredit] -> Processing baseSalary: {BaseSalary}", baseSalary);

            var result = await microCreditRepository.GrantingMicroCredit(baseSalary);

            logger.LogInformation("[MicroCreditService - GrantingMicroCredit] -> Credit result: {Result}", result);

            return result;
        }
    }
}
