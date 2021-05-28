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
        public void Withdraw_WhenAPositiveEqualThanTheBalanceAmountIsGiven_ShouldDoTheWithdraw()
        {
            // arrange
            const decimal valueToBeWithdrawn = 100m;
            var bankAccount = new BankAccount();
            bankAccount.Credit(100m);

            // act
            BankOperationService.Withdraw(bankAccount, valueToBeWithdrawn);

            // assert
            Assert.Equal(0.0m, bankAccount.Balance);
        }

        [Fact]
        public void Withdraw_WhenAPositiveSmallerThanTheBalanceAmountIsGiven_ShouldDoTheWithdraw()
        {
            // arrange
            const decimal valueToBeWithdrawn = 90m;
            var bankAccount = new BankAccount();
            bankAccount.Credit(100m);

            // act
            BankOperationService.Withdraw(bankAccount, valueToBeWithdrawn);

            // assert
            Assert.Equal(10m, bankAccount.Balance);
        }

        [Fact]
        public void Withdraw_WhenANegativeAmountIsGiven_ShouldGiveBackAnErrorAndNotWithdrawToTheBalance()
        {
            // arrange
            const decimal valueToBeWithdrawn = -100m;
            var bankAccount = new BankAccount();

            // act and assert
            Assert.Throws<ArgumentException>(() => BankOperationService.Withdraw(bankAccount, valueToBeWithdrawn));
            Assert.Equal(0.0m, bankAccount.Balance);
        }

        [Fact]
        public void Withdraw_WhenAPositiveAmountIsGreaterThanTheBalance_ShoudGiveBackAnErrorAndNotWithdrawToTheBalance()
        {
            // arrange
            const decimal valueToBeWithdrawn = 150.0m;
            var bankAccount = new BankAccount();
            bankAccount.Credit(100.0m);

            // act and assert
            Assert.Throws<ArgumentException>(() => BankOperationService.Withdraw(bankAccount, valueToBeWithdrawn));
            Assert.Equal(100.0m, bankAccount.Balance);
        }

        [Fact]
        public void Transfer_WhenAPositiveSmallerThanBalanceAmountIsGiven_ShouldDoTheTransfer()
        {
            // arrange
            const decimal valueToBeTransferred = 100m;
            var bankAccount = new BankAccount();
            var recipientAccount = new BankAccount();
            bankAccount.Credit(150m);

            // act
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
            bankAccount.Credit(100m);

            // act and assert
            Assert.Throws<ArgumentException>(
                () => BankOperationService.Transfer(bankAccount, valueToBeTransferred, recipientAccount));
            Assert.Equal(100.0m, bankAccount.Balance);
            Assert.Equal(0.0m, recipientAccount.Balance);
        }

    }
}
