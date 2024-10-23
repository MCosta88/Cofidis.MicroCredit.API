namespace Cofidis.MicroCredit.Data.Interfaces
{
    public interface IMicroCreditRepository
    {
        Task<decimal> GrantingMicroCredit(decimal baseSalary);
    }
}