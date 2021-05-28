using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BankAccount
    {
        private static Random _randomAccountNumberGenerate = new();
        public BankAccount()
        {
            Balance = 0m;
            AccountNumber = _randomAccountNumberGenerate.Next(100000, 999999).ToString();
        }
        public int Id { get; }
        public string AccountNumber { get;}
        public decimal Balance { get; private set; }
        public int AccountHolderId { get; }

        private static string GenerateAccountNumber()
        {
            int i = 5;
            string s = String.Empty;
            while (i >= 0)
            {
                s += _randomAccountNumberGenerate.Next(0, 9).ToString();
                i--;
            }
            return s;
        }

        public void AddToBalance(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Can't add negative values to the account.");
            Balance += amount;
        }

        public void SubtractFromBalance(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Can't subtract negative values from the account.");
            Balance -= amount;
        }

    }
}
