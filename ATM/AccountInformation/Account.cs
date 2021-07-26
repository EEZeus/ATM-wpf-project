using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using ATM.ErrorsAndNotifications;
using ATM.FileProcessing;

namespace ATM.AccountInformation
{
    public class Account
    {
        public static Account CurrentAcc { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public long Phone { get; set; }
        public string Password { get; set; }
        public float Balance { get; set; }
        public Account()
        {
            
        }
        public Account(string name,string id,string password,long phone,float balance)
        {
            Name = name;
            Id = id;
            Phone = phone;
            Balance = balance;
            Password = password;
        }
        public static Account Parse(string info)
        {
            var infoList = info.Split("#");
            var id = infoList[0].Substring(3);
            var password = infoList[1].Substring(9);
            var name = infoList[2].Substring(5);
            var phone = infoList[3].Substring(6);
            var balance = infoList[4].Substring(8);
            return new Account(name, id,password, long.Parse(phone), float.Parse(balance));
        }

        public static void CreateAcc(string name, string id,string password, long phone, float balance = 0)
        { 
            var temp = new Account(name, id,password, phone, balance);
            if(!Security.Security.AccountIsAlreadyExist(temp))
            {
                if (password.Length < 4)
                {
                    new ShowError().Show("Password Is Less Than 4 Characters !");
                }else{
                    FileProcessor.SaveInfo(temp);
                    new ShowNotification().Show("Account Was Created Successfully.");
                }
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