using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallBanking.Entities
{
    public class Customer
    {
        private decimal transactionCost = 1.95m;
        private decimal monthlyAccountFee = 15;
        private string cpr;
        private string name;
        private List<Account> accounts = new List<Account>();

        public Customer(string cpr, string name, List<Account> accounts)
        {
            CPR = cpr;
            Name = name;
            Accounts = accounts;
        }

        public List<Account> Accounts
        {
            get { return accounts; }
            set { accounts = value; }
        }

        public string Name
        {
            get { return name; }
            set {
                if (!string.IsNullOrWhiteSpace(value))
                    name = value;
                else
                    throw new ArgumentNullException("Name cannot be null or only whitespace.");
            }
        }

        public string CPR
        {
            get { return cpr; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("CPR cannot be null.");
                else if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("CPR cannot be only white space.");
                else if (value.Length != 10)
                    throw new ArgumentException("CPR must be 10 digits.");
                else if (!int.TryParse(value, out int number))
                    throw new ArgumentException("CPR can only contain numbers.");
                else
                    cpr = value;
            }
        }

        public decimal MonthlyAccountFee
        {
            get { return monthlyAccountFee; }
            protected set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Monthly account fee cannot be less than zero.");
                else
                    monthlyAccountFee = value;
            }
        }

        public decimal TransactionCost
        {
            get { return transactionCost; }
            protected set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Transaction cost cannot be less than zero.");
                else
                    transactionCost = value;
            }
        }

        public decimal CalculateCostOfMonth(Month month)
        {
            decimal result = 0;

            foreach (Account a in Accounts)
            {
                result += a.CalculateCostOfMonth(month, TransactionCost, MonthlyAccountFee);
            }

            return result;
        }
    }
}
