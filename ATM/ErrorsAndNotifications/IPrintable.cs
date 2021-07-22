using System.IO;
using System.Threading.Tasks;

namespace ATM.ErrorsAndNotifications
{
    public interface IPrintable
    {
        public Task Print(string text);
    }
    public class PrintNotification : IPrintable
    {
        public async Task Print(string notification)
        {
            using (var out_file = File.AppendText(@"Operation_Notifications"))
            {
               await out_file.WriteLineAsync(string.Format("Notification :\n{0}\n---------------------------------\n",notification));
            }
        }
    }
    public class PrintError : IPrintable
    {
        public async Task Print(string error)
        {
            using (var out_file = File.AppendText(@"Operation_Errors"))
            {
                await out_file.WriteLineAsync(string.Format("Error :\n{0}\n---------------------------------\n", error));
            }
        }
    }
}
