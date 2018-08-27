using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallBanking.Entities
{
    public abstract class Account
    {
        private decimal balance;
        private string accountNumber;
        private List<Transaction> transactions = new List<Transaction>();

        protected Account(string accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public List<Transaction> Transactions
        {
            get { return transactions; }
        }

        public string AccountNumber
        {
            get { return accountNumber; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Account Number cannot be null or contain only whitespace.");
                else if (!int.TryParse(value, out int number))
                    throw new ArgumentException("Account Number can only contain numbers.");
                else
                    accountNumber = value;
            }
        }

        public decimal Balance
        {
            get { return balance; }
            set {
                if (value >= 0)
                    balance = value;
                else
                    throw new ArgumentOutOfRangeException("Balance cannot be less than zero.");
            }
        }

        public bool PerformTransaction(Transaction t)
        {
            if (t.Transmitter == this || t.Receiver == this)
            {
                if (t.Transmitter.Balance >= t.Ammount)
                {
                    t.Transmitter.Balance -= t.Ammount;
                    t.Receiver.Balance += t.Ammount;
                    Transactions.Add(t);
                    return true;
                }
                else
                {
                    throw new ArgumentException("Transaction ammount exceeds the balance of transmitting account.");
                }
            }
            else
            {
                throw new ArgumentException("You can only perform transactions that include your account as either the transmitter or receiver.");
            }
        }

        public virtual decimal CalculateCostOfMonth(Month month, decimal transactionCost, decimal monthlyAccountFee)
        {
            decimal result = 0;
            result += monthlyAccountFee;

            List<Transaction> monthlyTransactions = Transactions.FindAll(x => x.DateTimeOfTransaction.Month.Equals((int)month));
            result += monthlyTransactions.Count * transactionCost;

            return result;
        }
    }
}
