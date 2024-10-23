using Newtonsoft.Json;

namespace Cofidis.MicroCredit.ViewModel.MicroCredit
{
    public class GetGrantingMicroCreditViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nif")]
        public string NIF { get; set; }

        [JsonProperty("baseSalary")]
        public decimal BaseSalary { get; set; }

        [JsonProperty("isAbleToCredit")]
        public bool IsAbleToCredit { get; set; }

        [JsonProperty("creditAvailabilityValue")]
        public decimal CreditAvailabilityValue { get; set; }
    }
}
