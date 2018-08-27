using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallBanking.Entities
{
    public class PremiumCustomer : Customer
    {
        private decimal transactionCost = 1.20m;
        private decimal monthlyAccountFee = 12;

        public PremiumCustomer(string cpr, string name, List<Account> accounts) : base(cpr, name, accounts) { }

        public new decimal MonthlyAccountFee
        {
            get { return monthlyAccountFee; }
        }

        public new decimal TransactionCost
        {
            get { return transactionCost; }
        }
    }
}
