using _External_CMD.API.Models;

namespace _External_CMD.API.Services
{
    public interface ICMDService
    {
       Task<User> GetUserInformation(string nif);
    }
}
