using AutoMapper;
using Cofidis.MicroCredit.Data.Models.External;
using Cofidis.MicroCredit.Data.Models;

namespace Cofidis.MicroCredit.Manager.Mapping
{
    public class ClientUserMappingProfile : Profile
    {

        public ClientUserMappingProfile()
        {
            CreateMap<ClientUser, User>();
        }
    }
}
