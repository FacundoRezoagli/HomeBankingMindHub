using System.Security.Cryptography;

namespace HomeBankingMindHub.Utils
{
    public static class Utils
    {
        public static string GenerateAccountNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(000000, 999999);
            return $"VIN-{randomNumber}";
        }

        public static string GenerateCardNumber()
        {
            Random random = new Random();
            int randomNumber1 = random.Next(0, 9999);
            int randomNumber2 = random.Next(0, 9999);
            int randomNumber3 = random.Next(0, 9999);
            int randomNumber4 = random.Next(0, 9999);
            string formattedNumber1 = randomNumber1.ToString("D4");
            string formattedNumber2 = randomNumber2.ToString("D4");
            string formattedNumber3 = randomNumber3.ToString("D4");
            string formattedNumber4 = randomNumber4.ToString("D4");
            return $"{formattedNumber1}-{formattedNumber2}-{formattedNumber3}-{formattedNumber4}";
        }

        public static int GenerateCardCVV() 
        {
            Random random = new Random();
            int randomNumber = random.Next(100, 999);
            string formattedNumber = randomNumber.ToString("D3");
            return int.Parse(formattedNumber);
        }

        public static void EncryptPassword(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        }
        public static bool ValidatePassword(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(hash);
        }
    }
}
