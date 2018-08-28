using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallBanking.Entities;

namespace SmallBanking.Tests
{
    [TestClass]
    public class SavingAccountTestClass
    {
        public static SavingAccount CreateValidSavingAccount()
        {
            return new SavingAccount(2.5m, 420.69m, "137");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SavingAccountSetInterestToLessThanZero()
        {
            SavingAccount savingAccount = new SavingAccount(-1, 0, "137");
        }

        [TestMethod]
        public void SavingAccountAddInterestTest()
        {
            SavingAccount savingAccount = CreateValidSavingAccount();
            decimal interestPercentage = savingAccount.Interest;
            decimal accountBalance = savingAccount.Balance;

            savingAccount.AddInterest();

            Assert.AreEqual(accountBalance + (accountBalance * (interestPercentage / 100)), savingAccount.Balance);
        }
    }
}
