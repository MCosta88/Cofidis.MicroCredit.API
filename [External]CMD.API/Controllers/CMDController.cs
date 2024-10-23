using _External_CMD.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace _External_CMD.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CMDController(ICMDService cmdService) : ControllerBase
    {
        [HttpGet]
        [Route("{nif}")]
        public Task<Models.User> GetUserByNIF(string nif)
        {
            var user = cmdService.GetUserInformation(nif);

            if (user == null)
            {
                throw new Exception();
            }

            return user;
        }
    }
}
