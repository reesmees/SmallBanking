using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallBanking.Entities
{
    public class SavingAccount : Account
    {
        private decimal interest;

        public SavingAccount(decimal interest, decimal balance, string accountNumber) : base(accountNumber, balance)
        {
            Interest = interest;
        }

        public decimal Interest
        {
            get { return interest; }
            set {
                if (value > 0)
                    interest = value;
                else
                    throw new ArgumentOutOfRangeException("Interest must be greater than zero.");
            }
        }

        public void AddInterest()
        {
            Balance *= 1 + (Interest / 100);
        }
    }
}
