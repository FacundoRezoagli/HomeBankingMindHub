using HomeBankingMindHub.DTOs;
using System.Net;

namespace HomeBankingMindHub.Models
{
    public class Client
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public ICollection<ClientLoan> ClientLoans { get; set; }
        public ICollection<Card> Cards { get; set; }
        public Client(ClientRegisterDTO clientCreateDTO)
        {
            byte[] cHash;
            byte[] cSalt;
            Utils.Utils.EncryptPassword(clientCreateDTO.Password, out cHash, out cSalt);

            Email = clientCreateDTO.Email;
            Hash = cHash;
            Salt = cSalt;
            FirstName = clientCreateDTO.FirstName;
            LastName = clientCreateDTO.LastName;
            Accounts = new List<Account>();
        }
        public Client(string FirstName, string LastName, string Email, byte[] Hash, byte[] Salt) 
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Hash = Hash;
            this.Salt = Salt;
            Accounts = new List<Account>();
            ClientLoans = new List<ClientLoan>();
            Cards = new List<Card>();
        }
    }
}
