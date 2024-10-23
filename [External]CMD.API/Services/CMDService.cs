using _External_CMD.API.Models;

namespace _External_CMD.API.Services
{
    public class CMDService : ICMDService
    {
        private readonly List<User> _users =
        [
            new()
            {
                Name = "Fernando Pimenta",
                NIF = "999999990",
                Salary = 1000.00m,
                Age = 51,
                Nationality = "Portuguese",
                Loans = new List<Loan>
                {
                    new() { Date = DateTime.Now.AddMonths(-6), Amount = 500, IsActive = true },
                    new() { Date = DateTime.Now.AddYears(-1), Amount = 2000, IsActive = false }
                }
            },

            new()
            {
                Name = "Joana Guerra",
                NIF = "000000001",
                Salary = 1250.00m,
                Age = 37,
                Nationality = "Brazilian",
                Loans = new List<Loan>
                {
                    new() { Date = DateTime.Now.AddMonths(-3), Amount = 3000, IsActive = false }
                }
            }
        ];

        public async Task<User> GetUserInformation(string nif) => _users.FirstOrDefault(user => user.NIF == nif);
    }
}
