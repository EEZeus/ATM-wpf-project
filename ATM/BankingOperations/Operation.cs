using System;
using System.Windows;
using ATM.AccountInformation;
using ATM.ErrorsAndNotifications;
using ATM.FileProcessing;

namespace ATM.BankingOperations
{
    public class Operation
    {
        public static string GetBalance(Account acc,string operationType = "Get Balance")
        {
            new ShowNotification().Show(string.Format("Operation Type : {0} \n Your Account Balance Is : {1}", operationType, acc.Balance));
            new PrintNotification().Print(string.Format("Operation Type : {0} \n Your Account Balance Is : {1}",operationType,acc.Balance));
            return acc.Balance.ToString("C");
        }
        public static void MoneyTransfer(Account acc,string destinationId,float amount,string operationType = "Money Transfer")
        {
            try
            {
               
                if (acc.Balance>=amount)
                {
                    FileProcessor.GetInfo(destinationId).Balance += amount;
                    acc.Balance -= amount;
                    new ShowNotification().Show(string.Format(
                        "Operation Type : {0} \n {1} Transfered From {2} Account To {3} Account.", operationType,
                        amount, acc.Id, FileProcessor.GetInfo(destinationId).Id));
                    new PrintNotification().Print(string.Format(
                        "Operation Type : {0} \n {1} Transfered From {2} Account To {3} Account.", operationType,
                        amount, acc.Id, FileProcessor.GetInfo(destinationId).Id));
                }
                else
                {
                    new ShowError().Show(string.Format("Operation Type : {0}\nNot Enough Balance To Transfer !",operationType));
                    new PrintError().Print(string.Format("Operation Type : {0}\nNot Enough Balance To Transfer !", operationType));

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Somthing Went Wrong About Destination Account Id !", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

        }
        public static void Withdraw(Account acc, float amount, string operationType = "Withdraw")
        {
            if (acc.Balance >= amount)
            {
                acc.Balance -= amount;
                new ShowNotification().Show(string.Format(
                    "Operation Type : {0} \n Withdraw {1}  From {2} Account;", operationType,
                    amount, acc.Id));
                new PrintNotification().Print(string.Format(
                    "Operation Type : {0} \n Withdraw {1}  From {2} Account;", operationType,
                    amount, acc.Id));
            }
            else
            {
                new ShowError().Show(string.Format("Operation Type : {0}\nNot Enough Balance To Withdraw !", operationType));
                new PrintError().Print(string.Format("Operation Type : {0}\nNot Enough Balance To Withdraw !", operationType));

            }

        }
        public static void Deposit(Account acc, float amount, string operationType = "Deposit")
        {
            if ( 0 < amount)
            {
                acc.Balance += amount;
                new ShowNotification().Show(string.Format(
                    "Operation Type : {0} \n Deposited {1}  To {2} Account;", operationType,
                    amount, acc.Id));
                new PrintNotification().Print(string.Format(
                    "Operation Type : {0} \n Deposited {1}  To {2} Account;", operationType,
                    amount, acc.Id));
            }
            else
            {
                new ShowError().Show(string.Format("Operation Type : {0}\nIllegal Amount To Deposit !", operationType));
                new PrintError().Print(string.Format("Operation Type : {0}\nIllegal Amount To Deposit ! !", operationType));

            }
        }
    }
}