using Cofidis.MicroCredit.Data.Models.External;

namespace Cofidis.MicroCredit.Services.Interfaces
{
    public interface IHttpClientService
    {
        Task<User?> GetExternalUserByNIF(string nif);
    }
}
