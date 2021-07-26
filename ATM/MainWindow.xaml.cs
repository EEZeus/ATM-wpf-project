using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
    }
}
