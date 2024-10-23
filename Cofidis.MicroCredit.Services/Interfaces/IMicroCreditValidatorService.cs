using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCredit.Services.Interfaces
{
    public interface IMicroCreditValidatorService
    {
        Task<double> CalculateMicroCreditRiskIndex(double UnemploymentTax, double Inflation, int LoansActives, double LoanToPay, double baseSalary);

        Task<bool> MicroCreditAvailability(double riskIndex, double companyRisk, decimal baseSalary);
    }
}
