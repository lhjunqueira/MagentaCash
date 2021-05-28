using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Domain.Entities;
using Domain.Services;

namespace UnitTest
{
    public class BankOperationServiceTests
    {
        [Fact]
        public void Deposit_WhenAPositiveAmountIsGiven_ShouldDepositToAccountBalance()
        {
            // arrange
            const decimal valueToBeDeposited = 100m;
            var bankAccount = new BankAccount();

            // act
            BankOperationService.Deposit(bankAccount, valueToBeDeposited);

            // assert
            Assert.Equal(valueToBeDeposited, bankAccount.Balance);
        }

        [Fact]
        public void Deposit_WhenANegativeAmountIsGiven_ShouldGiveBackAnErrorAndDoNotDeposit()
        {
            // arrange
            const decimal valueToBeDeposited = -100m;
            var bankAccount = new BankAccount();

            // act and assert
            Assert.Throws<ArgumentException>(
                () => BankOperationService.Deposit(bankAccount, valueToBeDeposited));
            Assert.Equal(0.0m, bankAccount.Balance);
        }


        [Fact]
        public void Withdrawal_WhenAPositiveEqualThanTheBalanceAmountIsGiven_ShouldDoTheWithdrawal()
        {
            // arrange
            const decimal valueToBeWithdrawn = 100m;
            var bankAccount = new BankAccount();

            // act
            bankAccount.Credit(100m);
            BankOperationService.Withdrawal(bankAccount, valueToBeWithdrawn);

            // assert
            Assert.Equal(0.0m, bankAccount.Balance);
        }

        [Fact]
        public void Withdrawal_WhenAPositiveSmallerThanTheBalanceAmountIsGiven_ShouldDoTheWithdrawal()
        {
            // arrange
            const decimal valueToBeWithdrawn = 90m;
            var bankAccount = new BankAccount();

            // act
            bankAccount.Credit(100m);
            BankOperationService.Withdrawal(bankAccount, valueToBeWithdrawn);

            // assert
            Assert.Equal(10m, bankAccount.Balance);
        }

        [Fact]
        public void Withdrawal_WhenANegativeAmountIsGiven_ShouldGiveBackAnErrorAndNotWithdrawalToTheBalance()
        {
            // arrange
            const decimal valueToBeWithdrawn = -100m;
            var bankAccount = new BankAccount();

            // act and assert
            Assert.Throws<ArgumentException>(() => BankOperationService.Withdrawal(bankAccount, valueToBeWithdrawn));
            Assert.Equal(0.0m, bankAccount.Balance);
        }

        [Fact]
        public void Withdrawal_WhenAPositiveAmountIsGreaterThanTheBalance_ShoudGiveBackAnErrorAndNotWithdrawalToTheBalance()
        {
            // arrange
            const decimal valueToBeWithdrawn = 150.0m;
            var bankAccount = new BankAccount();

            // act and assert
            bankAccount.Credit(100.0m);
            Assert.Throws<ArgumentException>(() => BankOperationService.Withdrawal(bankAccount, valueToBeWithdrawn));
            Assert.Equal(100.0m, bankAccount.Balance);
        }

        [Fact]
        public void Transfer_WhenAPositiveSmallerThanBalanceAmountIsGiven_ShouldDoTheTransfer()
        {
            // arrange
            const decimal valueToBeTransferred = 100m;
            var bankAccount = new BankAccount();
            var recipientAccount = new BankAccount();

            // act
            bankAccount.Credit(150m);
            BankOperationService.Transfer(bankAccount, valueToBeTransferred, recipientAccount);

            // assert
            Assert.Equal(50.0m, bankAccount.Balance);
            Assert.Equal(100.0m, recipientAccount.Balance);
        }

        [Fact]
        public void Transfer_WhenANegativeAmountIsGiven_ShouldGiveBackAnErrorAndNotDoTheTransfer()
        {
            // arrange
            const decimal valueToBeTransferred = -100m;
            var bankAccount = new BankAccount();
            var recipientAccount = new BankAccount();

            // act and assert
            Assert.Throws<ArgumentException>(
                () => BankOperationService.Transfer(bankAccount, valueToBeTransferred, recipientAccount));
            Assert.Equal(0.0m, bankAccount.Balance);
            Assert.Equal(0.0m, recipientAccount.Balance);
        }

        [Fact]
        public void Transfer_WhenAPositiveGreaterThanTheBalanceAmountIsGiven_ShoudGiveBankAnErrorAndNotDoTheTransfer()
        {
            // arrange
            const decimal valueToBeTransferred = 150m;
            var bankAccount = new BankAccount();
            var recipientAccount = new BankAccount();

            // act and assert
            bankAccount.Credit(100m);
            Assert.Throws<ArgumentException>(
                () => BankOperationService.Transfer(bankAccount, valueToBeTransferred, recipientAccount));
            Assert.Equal(100.0m, bankAccount.Balance);
            Assert.Equal(0.0m, recipientAccount.Balance);
        }

    }
}
