using NUnit.Framework;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{
    [TestFixture]
    public class PremiumAccountTests
    {
        [TestCase("44145", "Premium Account", 100, AccountType.Free, 250, 100, false)]//fail, wrong account type
        [TestCase("44145", "Premium Account", 100, AccountType.Premium, -100, 100, false)]//fail, negative number deposited
        [TestCase("44145", "Premium Account", 100, AccountType.Premium, 1000, 1100, true)]//success    
        [Test]
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IDeposit deposit = new NoLimitDepositRule();
            Account account = new Account(accountNumber, name, balance, accountType);
            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
            if (response.Success == true)
            {
                Assert.AreEqual(newBalance, account.Balance);
            }

        }

        [TestCase("44145", "Premium Account", 100, AccountType.Free, -100, 100, false)]//fail, not a premium account type
        [TestCase("44145", "Premium Account", 100, AccountType.Premium, 100, 100, false)]//fail, positive number withdrawn
        [TestCase("44145", "Premium Account", 150, AccountType.Premium, -450, -300, true)]//success
        [TestCase("44145", "Premium Account", 100, AccountType.Premium, -600, -490, true)]//success, overdraft fee
        [Test]
        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdraw = new PremiumAccountWithdrawRule();
            Account account = new Account(accountNumber, name, balance, accountType);
            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
            if (response.Success == true)
            {
                Assert.AreEqual(newBalance, account.Balance);
            }

        }
      
    }
}
