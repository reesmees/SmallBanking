using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallBanking.Entities;

namespace SmallBanking.Tests
{
    [TestClass]
    public class CustomerTestClass
    {
        public static Customer CreateValidCustomer()
        {
            return new Customer("1234567890", "Rasmus Naver", new List<Account>() { CheckingAccountTestClass.CreateValidCheckingAccount(), SavingAccountTestClass.CreateValidSavingAccount() });
        }

        [TestMethod]
        public void CustomerValidConstructorTest()
        {
            string cpr = "1234567890";
            string name = "Rasmus Naver";

            Customer customer = new Customer(cpr, name, new List<Account>());

            Assert.AreEqual(name, customer.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CustomerCPRIsSetToNullTest()
        {
            Customer customer = new Customer(null, "Bla", new List<Account>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CustomerCPRIsSetToWhiteSpaceTest()
        {
            Customer customer = new Customer(" ", "Bla", new List<Account>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CustomerCPRIsNotTenDigitsLongTest()
        {
            Customer customer = new Customer("12", "Bla", new List<Account>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CustomerCPRContainsNonNumbersTest()
        {
            Customer customer = new Customer("abcdefghij", "Bla", new List<Account>());
        }

        [TestMethod]
        public void CustomerCalculateCostOfMonthTest()
        {
            Customer customer = CreateValidCustomer();
            CheckingAccount checkingAccount = CheckingAccountTestClass.CreateValidCheckingAccount();
            decimal transactionCost = customer.TransactionCost;
            decimal monthlyCost = customer.MonthlyAccountFee;
            decimal numberOfTransactionFees = 0;
            Transaction transaction1 = new Transaction(1, DateTime.Now, customer.Accounts[0], checkingAccount);
            Transaction transaction2 = new Transaction(1, DateTime.Now, customer.Accounts[1], checkingAccount);
            int numberOfTransaction1Performed = 21;
            int numberOfTransaction2Performed = 2;

            for (int i = 0; i < numberOfTransaction1Performed; i++)
            {
                customer.Accounts[0].PerformTransaction(transaction1);
            }

            for (int i = 0; i < numberOfTransaction2Performed; i++)
            {
                customer.Accounts[1].PerformTransaction(transaction2);
            }

            foreach (Account a in customer.Accounts)
            {
                if (a is CheckingAccount && a.Transactions.Count > CheckingAccount.noMonthlyFreeTransactions)
                    numberOfTransactionFees += a.Transactions.Count - CheckingAccount.noMonthlyFreeTransactions;
                else
                    numberOfTransactionFees += a.Transactions.Count;
            }
            Month month = (Month)DateTime.Now.Month;


            decimal costOfMonth = customer.CalculateCostOfMonth(month);


            Assert.AreEqual((monthlyCost * customer.Accounts.Count) + (transactionCost * numberOfTransactionFees), costOfMonth);
        }
    }
}
