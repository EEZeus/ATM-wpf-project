//################################################
//## Author : Ehsan Espandar , github : EEzeus  ##
//################################################
using System;
using System.Windows;
using ATM.AccountInformation;
using ATM.ErrorsAndNotifications;

namespace ATM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            gridCreateAccount.Visibility = Visibility.Hidden;
            GridAlreadyHaveAccount.Visibility = Visibility.Hidden;
            GridOperations.Visibility = Visibility.Hidden;
            GridTransferMoney.Visibility = Visibility.Hidden;
            GridWithDraw.Visibility = Visibility.Hidden;
            GridDeposit.Visibility = Visibility.Hidden;

        }

        private void buttonCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            gridCreateAccount.Visibility = Visibility.Visible;
        }

        private void buttonAlreadyHaveAccount_Click(object sender, RoutedEventArgs e)
        {
            GridAlreadyHaveAccount.Visibility = Visibility.Visible;

        }

        private void buttonExitMain_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void buttonGridCreateAccountBack_Click(object sender, RoutedEventArgs e)
        {
            gridCreateAccount.Visibility = Visibility.Hidden;
        }
        private void buttonGridCreateAccountSubmitCreation_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text == string.Empty)
            {
                new ShowError().Show("Name Box Is Empty !");
            }
            else if (textBoxId.Text == string.Empty)
            {
                new ShowError().Show("Id Box Is Empty !");

            }
            else if (textBoxPhone.Text == string.Empty)
            {
                new ShowError().Show("Phone Box Is Empty !");

            }else if (passwordBoxPassword.Password == string.Empty)
            {
                new ShowError().Show("Password Box Is Empty !");
            }
            else if (passwordBoxPassword.Password.Length < 4)
            {
                new ShowError().Show("Password Is Less Than 4 Characters !");
            }
            else
            {
                Account.CreateAcc(textBoxName.Text,textBoxId.Text,passwordBoxPassword.Password,Convert.ToInt64(textBoxPhone.Text));
                new ShowNotification().Show("Account Was Created Successfully.");
                textBoxName.Clear();
                textBoxId.Clear();
                textBoxPhone.Clear();
                passwordBoxPassword.Clear();
            }
        }

        private void buttonAlreadyHaveAccountGridLogin_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxLoginId.Text == string.Empty)
            {
                new ShowError().Show("Id Box Is Empty !");
            }
            else if (passwordBoxLoginPassword.Password == string.Empty)
            {
                new ShowError().Show("Password Box Is Empty !");
            }
            else
            {
                var temp = FileProcessing.FileProcessor.GetInfo(textBoxLoginId.Text);
                if (temp != null)
                {
                    if (Security.Security.UserConfirmation(temp,passwordBoxLoginPassword.Password))
                    {
                        if (!Security.Security.isBanned(temp))
                        {
                            new ShowNotification().Show("Logged In Successfully .\nWelcome " + temp.Name);
                            Account.CurrentAcc = temp;
                            GridOperations.Visibility = Visibility.Visible;
                            textBoxLoginId.Clear();
                            passwordBoxLoginPassword.Clear();
                        }
                        else
                        {
                            new ShowError().Show("Account is Temporarily Banned !");
                        }
                    }
                    else
                    {
                        new ShowError().Show("Wrong Password !");
                    }
                }
                else
                {
                    new ShowError().Show("Account With Entered Id Doesn't Exist !");

                }
            }
        }

        private void buttonAlreadyHaveAccountGridBack_Click(object sender, RoutedEventArgs e)
        {
            GridAlreadyHaveAccount.Visibility = Visibility.Hidden;
        }

        private void buttonOperationsBack_Click(object sender, RoutedEventArgs e)
        {
            GridOperations.Visibility = Visibility.Hidden;
        }

        private void buttonOperationsShowPrintBalance_Click(object sender, RoutedEventArgs e)
        {
            BankingOperations.Operation.GetBalance(Account.CurrentAcc);
        }

        private void buttonOperationsTransferMoney_Click(object sender, RoutedEventArgs e)
        {
            GridTransferMoney.Visibility = Visibility.Visible;
            textBoxMoneyTransferAmount.Clear();
            textBoxMoneyTransferDestinationId.Clear();
        }

        private void buttonOperationsWithdraw_Click(object sender, RoutedEventArgs e)
        {
            GridWithDraw.Visibility = Visibility.Visible;
            textBoxWithdrawAmount.Clear();
        }

        private void buttonOperationsDeposit_Click(object sender, RoutedEventArgs e)
        {
            GridDeposit.Visibility = Visibility.Visible;

        }

        private void buttonGridMoneyTransferBack_Click(object sender, RoutedEventArgs e)
        {
            GridTransferMoney.Visibility = Visibility.Hidden;

        }

        private void buttonGridMoneyTransferTransfer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BankingOperations.Operation.MoneyTransfer(Account.CurrentAcc, textBoxMoneyTransferDestinationId.Text,
                    Convert.ToSingle(textBoxMoneyTransferAmount.Text));
                GridTransferMoney.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
                new ShowError().Show("Something Went Wrong With TexBoxes !");
            }
        }

        private void buttonGridWithdrawWithdraw_Click(object sender, RoutedEventArgs e)
        {
            BankingOperations.Operation.Withdraw(Account.CurrentAcc, Convert.ToSingle(textBoxWithdrawAmount.Text));
            GridWithDraw.Visibility = Visibility.Hidden;
        }

        private void buttonGridWithdrawBack_Click(object sender, RoutedEventArgs e)
        {
            GridWithDraw.Visibility = Visibility.Hidden;
        }

        private void buttonGridDepositDeposit_Click(object sender, RoutedEventArgs e)
        {
            BankingOperations.Operation.Deposit(Account.CurrentAcc,Convert.ToSingle(textBoxDepositAmount.Text));
            GridDeposit.Visibility = Visibility.Hidden;
        }
        private void buttonGridDepositBack_Click(object sender, RoutedEventArgs e)
        {
            GridDeposit.Visibility = Visibility.Hidden;
        }

        private void buttonOperationsDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show( "Are You Sure You Want To Delete Your Account?", "Delete Account !",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Account.DeleteAcc(Account.CurrentAcc);
                new ShowNotification().Show(Account.CurrentAcc.Name +"Account Was Deleted Successfully.\n Amount "+Account.CurrentAcc.Balance+"Was Given To "+Account.CurrentAcc.Name);
                new PrintNotification().Print(Account.CurrentAcc.Name + "Account Was Deleted Successfully.\n Amount " + Account.CurrentAcc.Balance.ToString("C") + " Was Given To " + Account.CurrentAcc.Name);

            }
        }
    }

}
