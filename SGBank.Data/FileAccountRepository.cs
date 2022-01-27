using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        private Dictionary<string, Account> accounts = new Dictionary<string, Account>();
        private static Account _account = new Account();
        private string fileName = ConfigurationManager.AppSettings["FileName"].ToString();

        public Account LoadAccount(string AccountNumber)
        {
            accounts = new Dictionary<string, Account>();
            List<Account> accountList = new List<Account>();
            string[] rows = File.ReadAllLines(fileName);

            for (int i = 1; i < rows.Length; i++)
            { 
                _account = UnmarshallAccount(rows[i]);
                accounts.Add(_account.AccountNumber, _account);

                if (AccountNumber.Equals(_account.AccountNumber))
                {
                    return _account;
                }
            }
            accountList.Where(a => a.AccountNumber == AccountNumber);
            return null;
        }

        public void SaveAccount(Account account)
        {
            string[] rows = File.ReadAllLines(fileName);

            for (int i = 1; i < rows.Length; i++)
            {

                if (rows[i].StartsWith(_account.AccountNumber))
                {
                    rows[i] = MarshallAccount(_account);
                }
                File.WriteAllLines(fileName, rows);
            }
        }

        private string MarshallAccount(Account account)
        {
            string acct = "";

            if (account.Type == AccountType.Free)
            {
                acct = _account.AccountNumber + "," + _account.Name + "," + Convert.ToString(_account.Balance) + ",F";
            }
            if (account.Type == AccountType.Basic)
            {
                acct = _account.AccountNumber + "," + _account.Name + "," + Convert.ToString(_account.Balance) + ",B";
            }
            if (account.Type == AccountType.Premium)
            {
                acct = _account.AccountNumber + "," + _account.Name + "," + Convert.ToString(_account.Balance) + ",P";
            }
            return acct;
        }

        private Account UnmarshallAccount(string accountString)
        {
            string[] accountElements = accountString.Split(',');

            Account account = new Account();

            account.AccountNumber = accountElements[0];
            account.Name = accountElements[1];
            account.Balance = Convert.ToDecimal(accountElements[2]);

            if (accountElements[3] == "F")
            {
                account.Type = AccountType.Free;
            }
            if (accountElements[3] == "B")
            {
                account.Type = AccountType.Basic;
            }
            if (accountElements[3] == "P")
            {
                account.Type = AccountType.Premium;
            }
            return account;
        }
    }
}
