using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services
{
    public static class BankOperationService
    {
        public static void Deposit(BankAccount targetAccount, decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("The deposited value should be a positive value.");
            }
            targetAccount.Credit(amount);
        }

        public static void Withdrawal(BankAccount targetAccount, decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("The withdrawal amount should be a positive value.");
            }
            if (amount > targetAccount.Balance)
            {
                throw new ArgumentException("The withdrawal amount should be less than the balance.");
            }
            targetAccount.Debit(amount);
        }

        public static void Transfer(BankAccount targetAccount, decimal amount, BankAccount recipientAccount)
        {
            if (amount <= 0)
            { 
                throw new ArgumentException("The transfer amount should be a positive value.");
            }
            if (amount > targetAccount.Balance)
            {
                throw new ArgumentException("The transfer amount should be less than the balance.");
            }
            targetAccount.Debit(amount);
            recipientAccount.Credit(amount);
        }
    }
}
