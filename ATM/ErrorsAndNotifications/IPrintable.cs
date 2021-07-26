//################################################
//## Author : Ehsan Espandar , github : EEZeus  ##
//################################################
using System;
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
            using (var out_file = File.AppendText(@"Operation_Notifications.txt"))
            {
               await out_file.WriteLineAsync(string.Format("Notification !\tTime : {0}\n{1}\n---------------------------------\n",DateTime.Now.ToLongDateString(),notification));
            }
        }
    }
    public class PrintError : IPrintable
    {
        public async Task Print(string error)
        {
            using (var out_file = File.AppendText(@"Operation_Errors.txt"))
            {
                await out_file.WriteLineAsync(string.Format("Error !\tTime : {0}\n{1}\n---------------------------------\n", DateTime.Now.ToLongDateString(), error));
            }
        }
    }
}
