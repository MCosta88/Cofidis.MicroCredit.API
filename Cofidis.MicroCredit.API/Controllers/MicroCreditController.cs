using Cofidis.MicroCredit.Data.Models.External;
using Cofidis.MicroCredit.Manager.Interfaces;
using Cofidis.MicroCredit.Services.Constants;
using Cofidis.MicroCredit.Services.Interfaces;
using Cofidis.MicroCredit.ViewModel.MicroCredit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cofidis.MicroCredit.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MicroCreditController : ControllerBase
    {
        private readonly ILogger<MicroCreditController> _logger;

        private readonly IMicroCreditManager _microCreditManager;

        private readonly IHttpClientManager _httpClientManager;

        private readonly IMicroCreditValidatorService _microCreditValidatorService;

        public User _user { get; set; }

        public MicroCreditController(ILogger<MicroCreditController> logger, IMicroCreditManager microcreditManager, IHttpClientManager httpClientManager, IMicroCreditValidatorService microcreditValidatorService)
        {
            _logger = logger;
            _microCreditManager = microcreditManager;
            _httpClientManager = httpClientManager;
            _microCreditValidatorService = microcreditValidatorService;

        }

        [HttpGet(Name = "GrantingMicroCredit")]
        public async Task<GetGrantingMicroCreditViewModel> GrantingCredit(string nif, decimal baseSalary)
        {
            _logger.LogInformation("[MicroCreditController - GrantingMicroCredit] -> params:", nif, baseSalary);

            var user = await _httpClientManager.GetExternalUserByNIF(nif);

            _logger.LogInformation("[MicroCreditController - GrantingMicroCredit - GetExternalUserByNIF] -> User:", user);


            if (user == null)
            {
                _logger.LogError("[MicroCreditController - GrantingMicroCredit - - GetExternalUserByNIF] -> null object:");
                throw new Exception("Null object");
            }

            var result = await _microCreditManager.GrantingMicroCredit(baseSalary);

            _logger.LogInformation("[MicroCreditController - GrantingMicroCredit - GrantingMicroCredit  -> result:", result);


            var getGratingMicroCreditViewModel = new GetGrantingMicroCreditViewModel()
            {
                Name = user.Name,
                NIF = user.NIF,
                BaseSalary = baseSalary,
                CreditAvailabilityValue = result

            };


            var riskIndex = _microCreditValidatorService.CalculateMicroCreditRiskIndex(ConstantsRiskManagement.UnemploymentTax, ConstantsRiskManagement.InflationTaxes, user.Loans.Where(x => x.IsActive).Count(), (double)user.Loans.Where(x => x.IsActive).Sum(x => x.Amount), (double)baseSalary);

            _logger.LogInformation("[MicroCreditController - GrantingMicroCredit - CalculateMicroCreditRiskIndex] -> RiskIndex:", riskIndex);

            var microCreditAvaibility = await _microCreditValidatorService.MicroCreditAvailability(riskIndex.Result, ConstantsRiskManagement.HighRiskThresholdCompany, baseSalary);

            _logger.LogInformation("[MicroCreditController - GrantingMicroCredit - CalculateMicroCreditRiskIndex] -> MicroCreditAvailability:", microCreditAvaibility);

            getGratingMicroCreditViewModel.IsAbleToCredit = microCreditAvaibility;


            _logger.LogInformation("[MicroCreditController - GrantingMicroCredit - CalculateMicroCreditRiskIndex] -> getGratingMicroCreditViewModel:", getGratingMicroCreditViewModel);

            return getGratingMicroCreditViewModel;
        }
    }
}
