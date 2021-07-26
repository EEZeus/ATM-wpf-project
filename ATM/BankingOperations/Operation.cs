//################################################
//## Author : Ehsan Espandar , github : EEZeus  ##
//################################################
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
            new ShowNotification().Show(string.Format("Operation Type : {0} \n Your Account Balance Is : {1}", operationType, acc.Balance.ToString("C")));
            new PrintNotification().Print(string.Format("Operation Type : {0} \n Your Account Balance Is : {1}",operationType,acc.Balance.ToString("C")));
            return acc.Balance.ToString("C");
        }
        public static void MoneyTransfer(Account acc,string destinationId,float amount,string operationType = "Money Transfer")
        {
            try
            {
               
                if (acc.Balance>=amount && amount < 2000)
                {
                    var destinationAcc = FileProcessor.GetInfo(destinationId);
                    if (destinationAcc != null && destinationAcc.Id != acc.Id)
                    {
                        destinationAcc.Balance += amount;
                        acc.Balance -= amount;
                        new ShowNotification().Show(string.Format(
                            "Operation Type : {0} \n {1} Transfered From {2} Account To {3} Account.", operationType,
                            amount.ToString("C"), acc.Name, destinationAcc.Name));
                        new PrintNotification().Print(string.Format(
                            "Operation Type : {0} \n {1} Transfered From {2} Account To {3} Account.", operationType,
                            amount.ToString("C"), acc.Name, destinationAcc.Name));
                        Account.DeleteAcc(destinationAcc);
                        Account.CreateAcc(destinationAcc.Name, destinationAcc.Id, destinationAcc.Password,
                            destinationAcc.Phone, destinationAcc.Balance);
                        Account.DeleteAcc(acc);
                        Account.CreateAcc(acc.Name, acc.Id, acc.Password, acc.Phone, acc.Balance);
                    }
                    else
                    {
                        new ShowError().Show(string.Format("Operation Type : {0}\nDestination Id Doesn't Exist Or It's The Same Id Of Source Account !", operationType));
                    }
                }
                else
                {
                    new ShowError().Show(string.Format("Operation Type : {0}\nNot Enough Balance To Transfer Or Amount Is More Than 2000 $ !", operationType));
                    new PrintError().Print(string.Format("Operation Type : {0}\nNot Enough Balance To Transfer Or Amount Is More Than 2000 $!", operationType));

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
            if (acc.Balance >= amount && amount <500)
            {
                acc.Balance -= amount;
                new ShowNotification().Show(string.Format(
                    "Operation Type : {0} \n Withdraw {1}  From {2} Account;", operationType,
                    amount.ToString("C"), acc.Name));
                new PrintNotification().Print(string.Format(
                    "Operation Type : {0} \n Withdraw {1}  From {2} Account;", operationType,
                    amount.ToString("C"), acc.Name));
                Account.DeleteAcc(acc);
                Account.CreateAcc(acc.Name, acc.Id, acc.Password, acc.Phone, acc.Balance);
            }
            else
            {
                new ShowError().Show(string.Format("Operation Type : {0}\nNot Enough Balance To Withdraw Or Amount Is More Than 500 $ !", operationType));
                new PrintError().Print(string.Format("Operation Type : {0}\nNot Enough Balance To Withdraw Or Amount Is More Than 500 $ !", operationType));

            }

        }
        public static void Deposit(Account acc, float amount, string operationType = "Deposit")
        {
            if ( 0 < amount)
            {
                acc.Balance += amount;
                new ShowNotification().Show(string.Format(
                    "Operation Type : {0} \n Deposited {1}  To {2} Account;", operationType,
                    amount.ToString("C"), acc.Name));
                new PrintNotification().Print(string.Format(
                    "Operation Type : {0} \n Deposited {1}  To {2} Account;", operationType,
                    amount.ToString("C"), acc.Name));
                Account.DeleteAcc(acc);
                Account.CreateAcc(acc.Name, acc.Id, acc.Password, acc.Phone, acc.Balance);
            }
            else
            {
                new ShowError().Show(string.Format("Operation Type : {0}\nIllegal Amount To Deposit !", operationType));
                new PrintError().Print(string.Format("Operation Type : {0}\nIllegal Amount To Deposit ! !", operationType));

            }
        }
    }
}