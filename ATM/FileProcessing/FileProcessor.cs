//################################################
//## Author : Ehsan Espandar , github : EEZeus  ##
//################################################
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ATM.AccountInformation;

namespace ATM.FileProcessing
{
    public class FileProcessor
    {
        public static async Task SaveInfo(Account account)
        {
            using (var out_file = File.AppendText(@"Account_Info.txt"))
            {
                await out_file.WriteLineAsync("id$"+account.Id+"#password$"+account.Password+"#name$"+account.Name+"#Phone$"+account.Phone+"#balance$"+account.Balance);
            }
        }

        public static Account GetInfo(string id)
        {
            string line;
            using (StreamReader in_file = new StreamReader(@"Account_Info.txt"))
            {
                while ((line = in_file.ReadLine()) != null)
                {
                    var splitedLine = line.Split("#");
                    if (splitedLine[0].Substring(3) == id)
                    {
                        return Account.Parse(line);
                    }
                }
            }
            return null;
        }

        public static List<Account> ListingAccounts()
        {
            var accountsList = new List<Account>();
            string line;
            using (StreamReader in_file = new StreamReader(@"Account_Info.txt"))
            {
                while ((line = in_file.ReadLine()) != null)
                {
                    var infoList = line.Split("#");
                    var id = infoList[0].Substring(3);
                    var password = infoList[1].Substring(9);
                    var name = infoList[2].Substring(5);
                    var phone = infoList[3].Substring(6);
                    var balance = infoList[4].Substring(8);
                    accountsList.Add(new Account(name, id,password, Convert.ToInt64(phone), Convert.ToSingle(balance)));
                }
            }

            return accountsList;
        }
    }

}