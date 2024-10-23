namespace _External_CMD.API.Models
{
    public class User
    {
        public string Name { get; set; }
        public string NIF { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
    }
}
