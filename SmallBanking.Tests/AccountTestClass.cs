using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallBanking.Entities;

namespace SmallBanking.Tests
{
    [TestClass]
    public class AccountTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AccountPerformTransactionDoesNotIncludeActingAccount()
        {
            CheckingAccount account1 = CheckingAccountTestClass.CreateValidCheckingAccount();
            CheckingAccount account2 = CheckingAccountTestClass.CreateValidCheckingAccount();
            CheckingAccount account3 = CheckingAccountTestClass.CreateValidCheckingAccount();
            Transaction transaction = new Transaction(1, DateTime.Now, account1, account2);

            account3.PerformTransaction(transaction);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AccountPerformTransactionWhenTransactionAmountExceedsBalanceOfTransmittingAccount()
        {
            CheckingAccount account1 = CheckingAccountTestClass.CreateValidCheckingAccount();
            CheckingAccount account2 = CheckingAccountTestClass.CreateValidCheckingAccount();
            Transaction transaction = new Transaction(500, DateTime.Now, account1, account2);

            account1.PerformTransaction(transaction);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AccountBalanceIsSetToLessThanZeroTest()
        {
            CheckingAccount account = new CheckingAccount(-2, "137");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AccountNumberIsSetToNullTest()
        {
            CheckingAccount account = new CheckingAccount(0, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AccountNumberIsSetToWhiteSpace()
        {
            CheckingAccount account = new CheckingAccount(0, " ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AccountNumberContainsDigitsThatAreNotNumbersTest()
        {
            CheckingAccount account = new CheckingAccount(0, "a137");
        }

        [TestMethod]
        public void AccountCalculateCostOfMonthTest()
        {
            Account account1 = SavingAccountTestClass.CreateValidSavingAccount();
            Account account2 = SavingAccountTestClass.CreateValidSavingAccount();
            Transaction transaction = new Transaction(2, DateTime.Now, account1, account2);
            decimal transactionCost = 1.95m;
            decimal monthlyCost = 15;
            decimal numberOfTransactions = 21;
            decimal costOfTransactions = transactionCost * numberOfTransactions;
            Month transactionMonth = (Month)DateTime.Now.Month;

            for (int i = 0; i < numberOfTransactions; i++)
            {
                account1.PerformTransaction(transaction);
            }
            decimal costOfMonth = account1.CalculateCostOfMonth(transactionMonth, transactionCost, monthlyCost);

            Assert.AreEqual(monthlyCost + costOfTransactions, costOfMonth);
        }
    }
}
