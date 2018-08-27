using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallBanking.Entities
{
    public class Transaction
    {
        private Account receiver;
        private Account transmitter;
        private DateTime dateTimeOfTransaction;
        private decimal ammount;

        public Transaction(decimal ammount, DateTime dateTimeOfTransaction, Account transmitter, Account receiver)
        {
            Ammount = ammount;
            DateTimeOfTransaction = dateTimeOfTransaction;
            Transmitter = transmitter;
            Receiver = receiver;
        }

        public decimal Ammount
        {
            get { return ammount; }
            set {
                if (value > 0)
                    ammount = value;
                else
                    throw new ArgumentOutOfRangeException("The transaction ammount must be greater than zero.");
            }
        }

        public DateTime DateTimeOfTransaction
        {
            get { return dateTimeOfTransaction; }
            set {
                if (value <= DateTime.Now)
                    dateTimeOfTransaction = value;
                else
                    throw new ArgumentOutOfRangeException("The date of the transaction cannot be in the future.");
            }
        }

        public Account Transmitter
        {
            get { return transmitter; }
            set { transmitter = value; }
        }

        public Account Receiver
        {
            get { return receiver; }
            set { receiver = value; }
        }

    }
}
