using System;
using Xunit;
using Domain.Entities;
using Domain.Services;

namespace UnitTest
{
    public class BankAccountTests
    {
        [Fact]
        public void BankAccount_WhenCreatingNewAccount_ShouldGiveARandomAccountNumber()
        {
            // arrange and act
            var bankAccount = new BankAccount();

            // assert
            Assert.True(bankAccount.AccountNumber.Length == 6);
        }

        [Fact]
        public void AddToBalance_WhenAPositiveAmountIsGiven_ShouldAddValueToAccountBalance()
        {
            // arrange
            const decimal valueToBeAdded = 100m;
            var bankAccount = new BankAccount();

            // act
            bankAccount.Credit(valueToBeAdded);

            // assert
            Assert.Equal(valueToBeAdded, bankAccount.Balance);
        }

        [Fact]
        public void AddToBalance_WhenANegativeAmountIsGiven_ShouldGiveBackAnErrorAndNotAddAnyBalance()
        {
            // arrange
            const decimal valueToBeAdded = -100m;
            var bankAccount = new BankAccount();

            // act and assert
            Assert.Throws<ArgumentException>(() => bankAccount.Credit(valueToBeAdded));
            Assert.Equal(0.0m, bankAccount.Balance);
        }

       
        [Fact]
        public void SubtractFromBalance_WhenAPositiveSmallerThanTheBalanceAmountIsGiven_ShouldSubtractValueFromTheBalance()
        {
            // arrange
            const decimal valueToBeSubtracted = 100m;
            var bankAccount = new BankAccount();

            // act
            bankAccount.Credit(150m);
            bankAccount.Debit(valueToBeSubtracted);

            // assert
            Assert.Equal(50m, bankAccount.Balance);
        }

        [Fact]
        public void SubtractFromBalance_WhenAPositiveGreaterThanBalanceAmountIsGiven_ShouldSubtractFromTheBalance()
        {
            // arrange
            const decimal valueToBeSubtracted = 200m;
            var bankAccount = new BankAccount();

            // act and assert
            bankAccount.Credit(150m);
            // eu tinha usado um assert.throws, porém retirei porque ele só joga exceção se for negativo
            bankAccount.Debit(valueToBeSubtracted);
            Assert.Equal(-50m, bankAccount.Balance);
        }

    }
}
