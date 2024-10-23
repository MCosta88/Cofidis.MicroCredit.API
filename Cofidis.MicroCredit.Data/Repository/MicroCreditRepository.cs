using Cofidis.MicroCredit.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Cofidis.MicroCredit.Data.Repository
{
    public class MicroCreditRepository : IMicroCreditRepository
    {

        private readonly DataContext _context;

        public MicroCreditRepository(DataContext context)
        {

            _context = context;

        }
        public async Task<decimal> GrantingMicroCredit(decimal baseSalary)
        {

            var baseSalaryParam = new SqlParameter("@baseSalary", baseSalary);


            var creditLimitParam = new SqlParameter
            {
                ParameterName = "@creditLimit",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Direction = System.Data.ParameterDirection.Output,
                Precision = 18,
                Scale = 2
            };

            _context.Database.ExecuteSqlRaw(
                "EXEC sp_CalcCreditLimit @baseSalary, @creditLimit OUTPUT",
                baseSalaryParam,
                creditLimitParam);

            if (creditLimitParam.Value == null)
            {

            }

            var result = (decimal)creditLimitParam.Value;

            return result;
        }
    }
}
