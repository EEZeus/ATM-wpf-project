using System.Windows;

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
            MessageBox.Show(notification,"Attention",MessageBoxButton.OK,MessageBoxImage.Information);
        }
    }
    public class ShowError : IShowable
    {
        public  void Show(string error)
        {
            MessageBox.Show(error, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}