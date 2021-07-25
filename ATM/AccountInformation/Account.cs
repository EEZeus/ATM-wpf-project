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
            var id = infoList[0].Substring(3);
            var name = infoList[1].Substring(5);
            var phone = infoList[2].Substring(6);
            var balance = infoList[3].Substring(8);
            return new Account(name, id, long.Parse(phone), float.Parse(balance));
        }

        public static void CreateAcc(string name, string id, long phone, float balance = 0)
        {
            var temp = new Account(name, id, phone, balance);
            if(!Security.Security.AccountIsAlreadyExist(temp)){
            FileProcessor.SaveInfo(temp);
            new ShowNotification().Show("Account Was Created Successfully.");
            }
            else
            {
                new ShowError().Show("Account With Same Id Already Exists.");
            }
        }

        public static void DeleteAcc(Account user)
        {
            var accList = FileProcessor.ListingAccounts();
            File.Delete(@"Account_Info.txt");
           foreach (var acc in accList)
           {
               if (acc.Id != user.Id)
               {
                   FileProcessor.SaveInfo(acc);
               }
           }
           new ShowNotification().Show("Account Was Deleted Successfully.");
        }
    }
}