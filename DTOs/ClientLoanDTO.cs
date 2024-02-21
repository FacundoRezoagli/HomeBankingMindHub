using HomeBankingMindHub.Models;

namespace HomeBankingMindHub.DTOs
{
    public class ClientLoanDTO
    {
        public long Id { get; set; }
        public long LoanId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public int Payments { get; set; }
        public ClientLoanDTO(ClientLoan clientLoan) 
        {
            Id = clientLoan.Id;
            LoanId = clientLoan.LoanId;
            Amount = clientLoan.Amount;
            Payments = int.Parse(clientLoan.Payments);
        }
    }
}
