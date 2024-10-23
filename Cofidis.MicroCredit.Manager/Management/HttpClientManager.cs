using AutoMapper;
using Cofidis.MicroCredit.Data.Models.External;
using Cofidis.MicroCredit.Manager.Interfaces;
using Cofidis.MicroCredit.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cofidis.MicroCredit.Manager.Management
{
    public class HttpClientManager : IHttpClientManager
    {
        private readonly ILogger<HttpClientManager> _logger;

        private readonly IHttpClientService _httpClientService;

        public readonly IMapper _mapper;

        public HttpClientManager(ILogger<HttpClientManager> logger, IHttpClientService httpClientService, IMapper mapper)
        {
            _logger = logger;
            _httpClientService = httpClientService;
            _mapper = mapper;
        }
        public async Task<User> GetExternalUserByNIF(string nif)
        {
            var user = await _httpClientService.GetExternalUserByNIF(nif);
            var result = _mapper.Map<User>(user);
            return result;

        }
    }
}
