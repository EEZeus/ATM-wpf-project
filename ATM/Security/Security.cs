//################################################
//## Author : Ehsan Espandar , github : EEZeus  ##
//################################################
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Automation.Peers;
using ATM.AccountInformation;
using ATM.ErrorsAndNotifications;
using ATM.FileProcessing;

namespace ATM.Security
{
    public class Security
    {
        private static List<Account> BannedAccounts = new List<Account>(); 
        public static bool UserConfirmation(Account acc, string password)
        {
            if (new string(password.ToCharArray().Reverse().ToArray()) == acc.Password)
            {
                BanAccont(acc);
                SecurityAlert("Your Account Has Banned Until Next Application Run Because You Entered Reverse Password !");
            }

            if (acc.Password == password)
                return true;
            return false;
        }

        public static void ChangePassword(ref Account acc, string oldPass, string newPass)
        {
            if (UserConfirmation(acc, oldPass))
            {
                acc.Password = newPass;
            }
        }

        public static void BanAccont(Account acc)
        {
            BannedAccounts.Add(acc);
        }

        public static List<Account> GetBannedAccounts()
        {
            return BannedAccounts;
        }

        public static void SecurityAlert(string Alert)
        {
            new ShowNotification().Show("Security Alert : "+Alert);
        }

        public static bool AccountIsAlreadyExist(Account acc)
        {
            if (FileProcessor.GetInfo(acc.Id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool isBanned(Account acc)
        {
            foreach (var account in BannedAccounts)
            {
                if (account.Id == acc.Id)
                    return true;
            }
            return false;
        }
    }
}