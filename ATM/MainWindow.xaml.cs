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
       /*     Account.CreateAcc("Ehsan", "2950250955", +989222129155, 1000);
            Account.CreateAcc("Ehsan", "295025955", +989222129155, 1000);
            Account.CreateAcc("Ehsan", "29500955", +989222129155, 1000);*/

          Account.DeleteAcc("2950250955");
        }
    }
}
