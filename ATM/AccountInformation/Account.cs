using System.IO;
using ATM.ErrorsAndNotifications;
using ATM.FileProcessing;

namespace ATM.AccountInformation
{
    public class Account
    {
        
        private string _password;
        private float _balance;
        public string Name { get; set; }
        public string Id { get; set; }
        public long Phone { get; set; }
        public Account()
        {
            
        }
        public Account(string name,string id,long phone,float balance)
        {
            Name = name;
            Id = id;
            Phone = phone;
            Balance = balance;
        }
        public float Balance
        {
            get => _balance;
            set
            {
                if (value >= 0)
                    _balance = value;
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if (value.Length > 4)
                    _password = value;
            }
        }
        public static Account Parse(string info)
        {
            var infoList = info.Split("#");
            var id = infoList[0].Substring(2);
            var name = infoList[1].Substring(4);
            var phone = infoList[2].Substring(5);
            var balance = infoList[3].Substring(7);
            return new Account(name, id, long.Parse(phone), float.Parse(balance));
        }

        public static void CreateAcc(string name, string id, long phone, float balance = 0)
        {
            FileProcessor.SaveInfo(new Account(name, id, phone, balance));
            new ShowNotification().Show("Account Was Created Successfully.");
        }

        public static void DeleteAcc(string id)
        {
            var accList = FileProcessor.ListingAccounts();
            File.Delete(@"Account_Info.txt");
           foreach (var acc in accList)
           {
               if (acc.Id != id)
               {
                   FileProcessor.SaveInfo(acc);
               }
           }
           new ShowNotification().Show("Account Was Deleted Successfully.");
        }
    }
}