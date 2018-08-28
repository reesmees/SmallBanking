using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallBanking.Entities;

namespace SmallBanking.Tests
{
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
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TransactionAmountEqualToOrLessThanZeroTest()
        {
            CheckingAccount account1 = CheckingAccountTestClass.CreateValidCheckingAccount();
            CheckingAccount account2 = CheckingAccountTestClass.CreateValidCheckingAccount();

            Transaction transaction = new Transaction(0, DateTime.Now, account1, account2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TransactionDateTimeIsInTheFutureTest()
        {
            CheckingAccount account1 = CheckingAccountTestClass.CreateValidCheckingAccount();
            CheckingAccount account2 = CheckingAccountTestClass.CreateValidCheckingAccount();

            Transaction transaction = new Transaction(1, new DateTime(2019, 1, 1), account1, account2);
        }
    }
}
