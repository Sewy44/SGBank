using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL.WithdrawRules
{
    public class PremiumAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            if (account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Message = "Error: a non-premium account hit the Basic withdrawal rule. Contact IT";
                return response;
            }

            if (amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdrawal amounts must be negative!";
                return response;
            }

            if (amount + account.Balance < -500)
            {
                response.Success = false;
                response.Message = "Premium accounts cannot overdraft more than $500!";
                return response;
            }

            response.Success = true;
            response.Account = account;
            response.Amount = amount;
            response.OldBalance = account.Balance;
            account.Balance = account.Balance + amount;

            return response;


        }
    }
}
