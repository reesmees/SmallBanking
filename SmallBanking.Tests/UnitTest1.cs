using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallBanking.Entities;

namespace SmallBanking.Tests
{
    [TestClass]
    public class AccountTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "You can only perform transactions that include your account as either the transmitter or receiver.")]
        public void AccountPerformTransactionDoesNotIncludeActingAccount()
        {
            CheckingAccount account1 = CheckingAccountTestClass.CreateValidCheckingAccount();
            CheckingAccount account2 = CheckingAccountTestClass.CreateValidCheckingAccount();
            CheckingAccount account3 = CheckingAccountTestClass.CreateValidCheckingAccount();
            Transaction transaction = new Transaction(1, DateTime.Now, account1, account2);

            account3.PerformTransaction(transaction);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Transaction ammount exceeds the balance of transmitting account.")]
        public void AccountPerformTransactionWhenTransactionAmmountExceedsBalanceOfTransmittingAccount()
        {
            CheckingAccount account1 = CheckingAccountTestClass.CreateValidCheckingAccount();
            CheckingAccount account2 = CheckingAccountTestClass.CreateValidCheckingAccount();
            Transaction transaction = new Transaction(500, DateTime.Now, account1, account2);

            account1.PerformTransaction(transaction);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Balance cannot be less than zero.")]
        public void AccountBalanceIsSetToLessThanZeroTest()
        {
            CheckingAccount account = new CheckingAccount(-2, "137");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Account Number cannot be null or contain only whitespace.")]
        public void AccountNumberIsSetToNullOrWhitespaceTest()
        {
            CheckingAccount account = new CheckingAccount(0, " ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Account Number can only contain numbers.")]
        public void AccountNumberContainsDigitsThatAreNotNumbersTest()
        {
            CheckingAccount account = new CheckingAccount(0, "a137");
        }

        [TestMethod]
        public void AccountCalculateCostOfMonthTest()
        {
            Account account1 = SavingAccountTestClass.CreateValidSavingAccount();
            Account account2 = SavingAccountTestClass.CreateValidSavingAccount();
            Transaction transaction1 = new Transaction(2, DateTime.Now, account1, account2);
            Transaction transaction2 = new Transaction(2, DateTime.Now, account1, account2);

            account1.PerformTransaction(transaction1);
            account2.PerformTransaction(transaction2);
            decimal costOfMonth = account1.CalculateCostOfMonth(Month.August, 1.95m, 15);

            Assert.AreEqual(18.90, costOfMonth);
        }
    }

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
            CheckingAccount account1 = CreateValidCheckingAccount();
            CheckingAccount account2 = CreateValidCheckingAccount();
            Transaction transaction = new Transaction(20, DateTime.Now, account1, account2);

            account1.PerformTransaction(transaction);

            Assert.AreEqual(440.69m, account2.Balance);
        }

        [TestMethod]
        public void CheckingAccountCalculateCostOfMonthTest()
        {
            CheckingAccount account1 = CreateValidCheckingAccount();
            CheckingAccount account2 = CreateValidCheckingAccount();
            Transaction transaction1 = new Transaction(20, DateTime.Now, account1, account2);
            Transaction transaction2 = new Transaction(10, DateTime.Now, account2, account1);

            account1.PerformTransaction(transaction1);
            account1.PerformTransaction(transaction2);
            decimal costOfMonth = account1.CalculateCostOfMonth(Month.August, 1.95m, 15);

            Assert.AreEqual(15, costOfMonth);
        }
    }

    [TestClass]
    public class SavingAccountTestClass
    {
        public static SavingAccount CreateValidSavingAccount()
        {
            return new SavingAccount(1.12m, 420.69m, "137");
        }
    }

    [TestClass]
    public class TransactionTestClass
    {
        [TestMethod]
        public void ValidInputConstructorTest()
        {
            CheckingAccount account1 = CheckingAccountTestClass.CreateValidCheckingAccount();
            CheckingAccount account2 = CheckingAccountTestClass.CreateValidCheckingAccount();

            Transaction transaction = new Transaction(1, DateTime.Now, account1, account2);

            Assert.AreEqual("137", transaction.Transmitter.AccountNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The transaction ammount must be greater than zero.")]
        public void TransactionAmmountEqualToOrLessThanZeroTest()
        {
            CheckingAccount account1 = CheckingAccountTestClass.CreateValidCheckingAccount();
            CheckingAccount account2 = CheckingAccountTestClass.CreateValidCheckingAccount();

            Transaction transaction = new Transaction(0, DateTime.Now, account1, account2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The date of the transaction cannot be in the future.")]
        public void TransactionDateTimeIsInTheFutureTest()
        {
            CheckingAccount account1 = CheckingAccountTestClass.CreateValidCheckingAccount();
            CheckingAccount account2 = CheckingAccountTestClass.CreateValidCheckingAccount();

            Transaction transaction = new Transaction(1, new DateTime(2019, 1, 1), account1, account2);
        }
    }
}
