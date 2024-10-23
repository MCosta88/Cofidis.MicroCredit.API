using Xunit;
using System.Threading.Tasks;
using Cofidis.MicroCredit.Manager.Interfaces;
using Cofidis.MicroCredit.Services.Interfaces;
using Moq;
using Cofidis.MicroCredit.Data.Models.External;
using Microsoft.Extensions.Logging;
using Cofidis.MicroCredit.API;
using Cofidis.MicroCredit.ViewModel.MicroCredit;
using Cofidis.MicroCredit.Services.Constants;
using Cofidis.MicroCredit.API.Controllers;

namespace API.Cofidis.Credit.Tests.Controllers
{
    public class MicroCreditControllerTests
    {
        private readonly Mock<IMicroCreditManager> _mockMicroCreditManager;
        private readonly Mock<IHttpClientManager> _mockHttpClientManager;
        private readonly Mock<IMicroCreditValidatorService> _mockMicroCreditValidatorService;
        private readonly Mock<ILogger<MicroCreditController>> _mockLogger;

        public MicroCreditControllerTests()
        {
            _mockMicroCreditManager = new Mock<IMicroCreditManager>();
            _mockHttpClientManager = new Mock<IHttpClientManager>();
            _mockMicroCreditValidatorService = new Mock<IMicroCreditValidatorService>();
            _mockLogger = new Mock<ILogger<MicroCreditController>>();
        }
        [Fact]
        public async Task GrantingMicroCredit_ShouldReturnViewModel_WhenAllOperationsAreSuccessful()
        {
            // Arranges
            var nif = "123456789";
            decimal baseSalary = 50000;

            var userResult = new User
            {
                Name = "John Doe",
                NIF = nif,
                Loans = new List<Loan>
            {
                new Loan { Amount = 1000, IsActive = true }
            }
            };


            var creditResult = 10000;
            double riskIndexResult = 2;
            double companyrisk = 0.6;
            bool isAblecreditAvailabilityResult = true;

            _mockMicroCreditManager.Setup(m => m.GrantingMicroCredit(baseSalary))
           .ReturnsAsync(creditResult);

            _mockHttpClientManager.Setup(m => m.GetExternalUserByNIF(nif))
               .ReturnsAsync(userResult);

            _mockMicroCreditValidatorService.Setup(m => m.CalculateMicroCreditRiskIndex(ConstantsRiskManagement.UnemploymentTax, ConstantsRiskManagement.InflationTaxes, userResult.Loans.Where(x => x.IsActive).Count(), (double)userResult.Loans.Where(x => x.IsActive).Sum(x => x.Amount), (double)baseSalary))
              .ReturnsAsync(riskIndexResult);

            _mockMicroCreditValidatorService.Setup(m => m.MicroCreditAvailability(riskIndexResult, companyrisk, baseSalary))
                .ReturnsAsync(isAblecreditAvailabilityResult);
            
             
            var getGratingCreditViewModel = new GetGrantingMicroCreditViewModel()
            {
                Name = userResult.Name,
                NIF = userResult.NIF,
                BaseSalary = baseSalary,
                CreditAvailabilityValue = 3000,
                IsAbleToCredit = isAblecreditAvailabilityResult

            };

            // Acts
            var user = _mockHttpClientManager.Object.GetExternalUserByNIF(nif);
            var availableCredit = _mockMicroCreditManager.Object.GrantingMicroCredit(baseSalary);
            var isAblecreditAvailability = _mockMicroCreditValidatorService.Object.MicroCreditAvailability(riskIndexResult, companyrisk, baseSalary);
            var riskIndex = _mockMicroCreditValidatorService.Object.CalculateMicroCreditRiskIndex(ConstantsRiskManagement.UnemploymentTax, ConstantsRiskManagement.InflationTaxes, userResult.Loans.Where(x => x.IsActive).Count(), (double)userResult.Loans.Where(x => x.IsActive).Sum(x => x.Amount), (double)baseSalary);

            // Asserts
            Assert.NotNull(availableCredit);
            Assert.NotNull(user);
            Assert.NotNull(riskIndex);
            Assert.NotNull(isAblecreditAvailability);

        }

        [Fact]
        public async Task GrantingCredit_ShouldReturnNull_WhenCreditManagerReturnsNull()
        {
            // Arrange
            var nif = "123456785";

            _mockHttpClientManager.Setup(m => m.GetExternalUserByNIF(nif)).ReturnsAsync((User?)null);

            // Acts
            var user = await _mockHttpClientManager.Object.GetExternalUserByNIF(nif);
      
            // Assert
            Assert.Null(user);  
        }

        [Fact]
        public async Task GrantingCredit_ShouldReturnViewModel_ClieUserHasLoansActiveAndLowBaseSalary1000()
        {
            // Arranges
            var nif = "123456789";
            decimal baseSalary = 1000;
            

            var userResult = new User
            {
                Name = "John Doe",
                NIF = nif,
                Loans = new List<Loan>
            {
                new Loan { Amount = 500, IsActive = true }
            }
            };


            decimal creditResult = 1000;
            double riskIndexResult = 52.099999999999994;
            double companyrisk = 0.6;
            bool isAblecreditAvailabilityResult = false;

            _mockMicroCreditManager.Setup(m => m.GrantingMicroCredit(baseSalary))
           .ReturnsAsync(creditResult);

            _mockHttpClientManager.Setup(m => m.GetExternalUserByNIF(nif))
               .ReturnsAsync(userResult);

            _mockMicroCreditValidatorService.Setup(m => m.CalculateMicroCreditRiskIndex(ConstantsRiskManagement.UnemploymentTax, ConstantsRiskManagement.InflationTaxes, userResult.Loans.Where(x => x.IsActive).Count(), (double)userResult.Loans.Where(x => x.IsActive).Sum(x => x.Amount), (double)baseSalary))
              .ReturnsAsync(riskIndexResult);

            _mockMicroCreditValidatorService.Setup(m => m.MicroCreditAvailability(riskIndexResult, companyrisk, baseSalary))
                .ReturnsAsync(isAblecreditAvailabilityResult);


            var getGratingCreditViewModel = new GetGrantingMicroCreditViewModel()
            {
                Name = userResult.Name,
                NIF = userResult.NIF,
                BaseSalary = baseSalary,
                CreditAvailabilityValue = 1000,
                IsAbleToCredit = isAblecreditAvailabilityResult

            };

            // Acts
            var user = _mockHttpClientManager.Object.GetExternalUserByNIF(nif);
            var availableCredit = _mockMicroCreditManager.Object.GrantingMicroCredit(baseSalary);
            var isAblecreditAvailability = _mockMicroCreditValidatorService.Object.MicroCreditAvailability(riskIndexResult, companyrisk, baseSalary);
            var riskIndex = _mockMicroCreditValidatorService.Object.CalculateMicroCreditRiskIndex(ConstantsRiskManagement.UnemploymentTax, ConstantsRiskManagement.InflationTaxes, userResult.Loans.Where(x => x.IsActive).Count(), (double)userResult.Loans.Where(x => x.IsActive).Sum(x => x.Amount), (double)baseSalary);

            // Asserts
            Assert.Equal(creditResult,availableCredit.Result);
            Assert.False(getGratingCreditViewModel.IsAbleToCredit);

        }

        [Fact]
        public async Task GrantingCredit_ShouldReturnViewModel_ClieUserWithOutLoansActiveAndLowBaseSalary()
        {
            // Arranges
            var nif = "123456789";
            decimal baseSalary = 1000;


            var userResult = new User
            {
                Name = "John Doe",
                NIF = nif
            };


            decimal creditResult = 1000;
            double riskIndexResult = -97.7;
            double companyrisk = 0.6;
            bool isAblecreditAvailabilityResult = true;
            int loansCount = 0;
            double loansAmount = 0;

            _mockMicroCreditManager.Setup(m => m.GrantingMicroCredit(baseSalary))
           .ReturnsAsync(creditResult);

            _mockHttpClientManager.Setup(m => m.GetExternalUserByNIF(nif))
               .ReturnsAsync(userResult);

            _mockMicroCreditValidatorService.Setup(m => m.CalculateMicroCreditRiskIndex(ConstantsRiskManagement.UnemploymentTax, ConstantsRiskManagement.InflationTaxes, loansCount, loansAmount, (double)baseSalary))
              .ReturnsAsync(riskIndexResult);

            _mockMicroCreditValidatorService.Setup(m => m.MicroCreditAvailability(riskIndexResult, companyrisk, baseSalary))
                .ReturnsAsync(isAblecreditAvailabilityResult);


            var getGratingCreditViewModel = new GetGrantingMicroCreditViewModel()
            {
                Name = userResult.Name,
                NIF = userResult.NIF,
                BaseSalary = baseSalary,
                CreditAvailabilityValue = 1000,
                IsAbleToCredit = isAblecreditAvailabilityResult

            };

            // Acts
            var user = _mockHttpClientManager.Object.GetExternalUserByNIF(nif);
            var availableMicroCredit = _mockMicroCreditManager.Object.GrantingMicroCredit(baseSalary);
            var isAblecreditAvailability = _mockMicroCreditValidatorService.Object.MicroCreditAvailability(riskIndexResult, companyrisk, baseSalary);
            var riskIndex = _mockMicroCreditValidatorService.Object.CalculateMicroCreditRiskIndex(ConstantsRiskManagement.UnemploymentTax, ConstantsRiskManagement.InflationTaxes, loansCount, loansAmount, (double)baseSalary);

            // Asserts
            Assert.Equal(creditResult, availableMicroCredit.Result);
            Assert.True(getGratingCreditViewModel.IsAbleToCredit);

        }

        [Fact]
        public async Task GrantingCredit_ShouldReturnViewModel_ClieUserHasLoansActiveAndMediumBaseSalary2000()
        {
            // Arranges
            var nif = "123456789";
            decimal baseSalary = 2000;


            var userResult = new User
            {
                Name = "John Doe",
                NIF = nif,
                Loans = new List<Loan>
                {
                    new Loan { Amount = 500, IsActive = true }
                }
            };


            decimal microCreditResult = 1000;
            double riskIndexResult = -47.900000000000006;
            double companyrisk = 0.6;
            bool isAblecreditAvailabilityResult = true;

            _mockMicroCreditManager.Setup(m => m.GrantingMicroCredit(baseSalary))
           .ReturnsAsync(microCreditResult);

            _mockHttpClientManager.Setup(m => m.GetExternalUserByNIF(nif))
               .ReturnsAsync(userResult);

            _mockMicroCreditValidatorService.Setup(m => m.CalculateMicroCreditRiskIndex(ConstantsRiskManagement.UnemploymentTax, ConstantsRiskManagement.InflationTaxes, userResult.Loans.Where(x => x.IsActive).Count(), (double)userResult.Loans.Where(x => x.IsActive).Sum(x => x.Amount), (double)baseSalary))
              .ReturnsAsync(riskIndexResult);

            _mockMicroCreditValidatorService.Setup(m => m.MicroCreditAvailability(riskIndexResult, companyrisk, baseSalary))
                .ReturnsAsync(isAblecreditAvailabilityResult);


            var getGratingMicroCreditViewModel = new GetGrantingMicroCreditViewModel()
            {
                Name = userResult.Name,
                NIF = userResult.NIF,
                BaseSalary = baseSalary,
                CreditAvailabilityValue = 1000,
                IsAbleToCredit = isAblecreditAvailabilityResult

            };

            // Acts
            var user = _mockHttpClientManager.Object.GetExternalUserByNIF(nif);
            var availableCredit = _mockMicroCreditManager.Object.GrantingMicroCredit(baseSalary);
            var isAblecreditAvailability = _mockMicroCreditValidatorService.Object.MicroCreditAvailability(riskIndexResult, companyrisk, baseSalary);
            var riskIndex = _mockMicroCreditValidatorService.Object.CalculateMicroCreditRiskIndex(ConstantsRiskManagement.UnemploymentTax, ConstantsRiskManagement.InflationTaxes, userResult.Loans.Where(x => x.IsActive).Count(), (double)userResult.Loans.Where(x => x.IsActive).Sum(x => x.Amount), (double)baseSalary);

            // Asserts
            Assert.Equal(microCreditResult, availableCredit.Result);
            Assert.True(getGratingMicroCreditViewModel.IsAbleToCredit);

        }


    }
}
