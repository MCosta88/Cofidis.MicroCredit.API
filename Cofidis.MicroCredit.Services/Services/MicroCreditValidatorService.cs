using Cofidis.MicroCredit.Services.Constants;
using Cofidis.MicroCredit.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cofidis.MicroCredit.Services.Services
{
    public class MicroCreditValidatorService(ILogger<MicroCreditValidatorService> logger) : IMicroCreditValidatorService
    {
        public async Task<double> CalculateMicroCreditRiskIndex(double unemploymentTax, double inflation, int loansActive, double loanToPay, double baseSalary)
        {
            double riskIndex = (unemploymentTax * ConstantsRiskManagement.UnemploymentRateWeight) +
                               (inflation * ConstantsRiskManagement.InflationRateWeight) +
                               ((100 - loansActive) / 100.0 * ConstantsRiskManagement.DebtWeight) +
                               (loanToPay * ConstantsRiskManagement.CreditHistoryWeight) -
                               (baseSalary * ConstantsRiskManagement.BaseSalaryWeight);

            logger.LogInformation($"Calculated Risk Index: {riskIndex} for base salary: {baseSalary}");
            return riskIndex;
        }

        public async Task<bool> MicroCreditAvailability(double riskIndex, double companyRisk, decimal baseSalary)
        {
            if (baseSalary < ConstantsRiskManagement.MinimumSalary)
            {
                logger.LogWarning($"Base salary {baseSalary} is less than minimum salary {ConstantsRiskManagement.MinimumSalary}. Credit not available.");
                return false;
            }

            bool isAvailable = riskIndex <= companyRisk;
            logger.LogInformation($"Credit Availability: {isAvailable} based on risk index: {riskIndex} and company risk: {companyRisk}");

            return isAvailable;
        }
    }
}
