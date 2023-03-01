using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UserAccount
    {
        public int Id { get; set; }
        public long CardNumber { get; set; }
        public int CardPin { get; set; }
        public long AccountNumber { get; set; }
        public string FullName { get; set; }
        public decimal AccountBalance { get; set; }
        public int TotalLogin { get; set; }
        public bool IsLocked { get; set; }

        //public UserAccount(
        //    int id, long cardNumber, int cardPin, 
        //    long accountNumber, string fullName,
        //    decimal accountBalance, int totalLogin,
        //    bool isLocked)
        //{
        //    Id = id;
        //    CardNumber = cardNumber;
        //    CardPin = cardPin;
        //    AccountNumber = accountNumber;
        //    FullName = fullName;
        //    AccountBalance = accountBalance;
        //    TotalLogin = totalLogin;
        //    IsLocked = isLocked;

        //}

        public override string ToString()
        {
            return "Id," + "CardNumber," + "CardPin," + "AccountNumber," +
                "FullName," + "AccountBalance," + "TotalLogin," + "IsLocked,";
        }


    }
}
