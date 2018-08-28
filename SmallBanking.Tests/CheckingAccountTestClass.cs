using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallBanking.Entities;

namespace SmallBanking.Tests
{
    [TestClass]
    public class CheckingAccountTestClass
    {
        public static CheckingAccount CreateValidCheckingAccount()
        {
            return new CheckingAccount(420.69m, "137");
        }

        [TestMethod]
        public void PerformTransactionTest()
        {
            CheckingAccount transmittingAccount = CreateValidCheckingAccount();
            CheckingAccount receivingAccount = CreateValidCheckingAccount();
            decimal receivingAccountBalance = receivingAccount.Balance;
            decimal transactionAmount = 20;
            Transaction transaction = new Transaction(transactionAmount, DateTime.Now, transmittingAccount, receivingAccount);

            transmittingAccount.PerformTransaction(transaction);

            Assert.AreEqual(receivingAccountBalance + transactionAmount, receivingAccount.Balance);
        }

        [TestMethod]
        public void CheckingAccountCalculateCostOfMonthTest()
        {
            CheckingAccount account1 = CreateValidCheckingAccount();
            CheckingAccount account2 = CreateValidCheckingAccount();
            Transaction transaction = new Transaction(10, DateTime.Now, account1, account2);
            decimal transactionCost = 1.95m;
            decimal monthlyCost = 15;
            decimal numberOfTransactions = 21;
            decimal costOfTransactions = transactionCost * (numberOfTransactions - CheckingAccount.noMonthlyFreeTransactions);
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
