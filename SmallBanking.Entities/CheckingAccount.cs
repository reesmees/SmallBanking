using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallBanking.Entities
{
    public class CheckingAccount : Account
    {
        public const int NoMonthlyFreeTransactions = 20;

        public CheckingAccount(decimal balance, string accountNumber) : base(accountNumber, balance) { }

        public override decimal CalculateCostOfMonth(Month month, decimal transactionCost, decimal monthlyAccountFee)
        {
            decimal result = 0;
            result += monthlyAccountFee;

            List<Transaction> monthlyTransactions = Transactions.FindAll(x => x.DateTimeOfTransaction.Month.Equals((int)month));

            if (monthlyTransactions.Count > NoMonthlyFreeTransactions)
            {
                result += transactionCost * (monthlyTransactions.Count - NoMonthlyFreeTransactions);
            }

            return result;
        }
    }
}
