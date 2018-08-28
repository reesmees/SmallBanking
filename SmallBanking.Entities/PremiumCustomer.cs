using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallBanking.Entities
{
    public class PremiumCustomer : Customer
    {
        public PremiumCustomer(string cpr, string name, List<Account> accounts) : base(cpr, name, accounts)
        {
            MonthlyAccountFee = 12;
            TransactionCost = 1.2m;
        }

        
    }
}
