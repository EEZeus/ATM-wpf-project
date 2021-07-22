using System.Collections.Generic;
using System.IO;

namespace ATM
{
    public class Account
    {
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

        public static Account Parse(string info)
        {
            var infoList = info.Split("#");
            var id = infoList[0].Substring(2);
            var name = infoList[1].Substring(4);
            var phone = infoList[2].Substring(5);
            var balance = infoList[3].Substring(7);
            return new Account(name, id, long.Parse(phone), float.Parse(balance));
        }

        public static void CreateAcc(string name, string id, long phone, float balance)
        {
            FileProcessor.SaveInfo(new Account(name, id, phone, balance));
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
        }
    }
}