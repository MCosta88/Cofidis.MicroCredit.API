namespace Cofidis.MicroCredit.Manager.Interfaces
{
    public interface IMicroCreditManager
    {
        Task<decimal> GrantingMicroCredit(decimal baseSalary);
    }
}
