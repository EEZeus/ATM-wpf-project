﻿using System.Collections.Generic;
using ATM.AccountInformation;
using ATM.ErrorsAndNotifications;

namespace ATM.Security
{
    public class Security
    {
        private static List<Account> BannedAccounts = new List<Account>();

        public static bool UserConfirmation(Account acc, string password)
        {
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
    }
}