using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Models
{
    public class Account
    {
        
        public Account()
        {

        }

        public Account(string accountNumber, string name, decimal balance, AccountType accountType)
        {
            AccountNumber = accountNumber;
            Name = name;
            Balance = balance;
            Type = accountType;
        }

        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountType Type { get; set; }

        
    }
}
