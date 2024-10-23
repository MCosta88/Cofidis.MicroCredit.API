using Cofidis.MicroCredit.Data.Models.External;

namespace Cofidis.MicroCredit.Manager.Interfaces
{
    public interface IHttpClientManager
    {
        Task<User> GetExternalUserByNIF(string nif);
    }
}
