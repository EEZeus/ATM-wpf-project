//################################################
//## Author : Ehsan Espandar , github : EEZeus  ##
//################################################
using System.Windows;
using Microsoft.VisualBasic;

namespace ATM.ErrorsAndNotifications
{
    public interface IShowable
    {
        public  void Show(string text);
    }
    public class ShowNotification:IShowable
    {
        public  void Show(string notification)
        {
            MessageBox.Show(DateAndTime.Now.ToLongDateString() + "\n" + notification,"Attention",MessageBoxButton.OK,MessageBoxImage.Information);
        }
    }
    public class ShowError : IShowable
    {
        public  void Show(string error)
        {
            MessageBox.Show(DateAndTime.Now.ToLongDateString()+"\n"+error, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}