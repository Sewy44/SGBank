using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class PremiumAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account()
        {
            Name = "Premum Account",
            Balance = 100.00M,
            AccountNumber = "44145",
            Type = AccountType.Premium
        };

        public Account LoadAccount(string AccountNumber)
        {
            if (AccountNumber.Equals(_account.AccountNumber))
            {
                return _account;
            }
            else
            {
                return null;
            }
        }

        public void SaveAccount(Account account)
        {
            _account = account;
        }

    }
}
