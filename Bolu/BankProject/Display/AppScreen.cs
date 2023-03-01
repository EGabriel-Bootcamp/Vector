using BankProject.Domain.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject.Display
{
    public class AppScreen
    {
        internal const string cur = "N ";

        public static void Welcome()
        {
            //clears the console Screen
            Console.Clear();
            //Sets the title of the console window
            Console.Title = "My Bank APP";

            //set the Text color to white
            Console.ForegroundColor = ConsoleColor.White;

            //Welcome message
            Console.WriteLine("\n\n -------------Welcome to My Bank APP-------------\n\n");

            //Prompt the user to insert ATM
            Console.WriteLine("Please Enter your Bank Card Number");

            Console.WriteLine("Note: We don't accept physical card");

            Utility.PressEnterToContinue();
            
        }

        internal static UserAccount UserLoginForm()
        {
            UserAccount tempUserAccount = new UserAccount();

            tempUserAccount.CardNumber = Validator.Convert<long>("Your Bank Account card number...");

            //Hidden input to mask pin.
            tempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecretInput("Enter your Card PIN"));

            return tempUserAccount;
        }

        internal static void LoginProgress()
        {
            Console.WriteLine("\nChecking card number and Pin.....");

            Utility.PrintDotAnimation();
        
        }

        internal static void PrintLockScreen()
        {
            Console.Clear();
            Utility.PrintMessage("Your account has been locked", true);

            Utility.PressEnterToContinue();
            Environment.Exit(1);
        }

        internal static void WelcomeCustomer(string fullName)
        {
            Console.WriteLine($"Welcome back,{fullName}.");
        }

        internal static void DisplaayAppMenu()
        {
            Console.Clear();
            Console.WriteLine("---------My Bank APP Menu----------");
            Console.WriteLine(":                                  ");
            Console.WriteLine("1.   Account Balance              :");
            Console.WriteLine("2.   Cash Deposit                 :");
            Console.WriteLine("3.   Withdrawal                   :");
            Console.WriteLine("4.   Transfer                     :");
            Console.WriteLine("5.   Transactions                 :");
            Console.WriteLine("6.   Logout                       :");
        }

        internal static void LogOutProgress()
        {
            Console.WriteLine("Thank you for using My Bank APP...");
            Utility.PrintDotAnimation();
            Console.Clear();
        }

        internal static int SelectAmonunt()
        {
            Console.WriteLine("");
            Console.WriteLine(":1.{0}500    5.{0}10,000", cur);
            Console.WriteLine(":2.{0}1000    6.{0}15,000", cur);
            Console.WriteLine(":3.{0}2000    7.{0}20,000", cur);
            Console.WriteLine(":4.{0}5000    8.{0}40,000", cur);
            Console.WriteLine(":0.Other");
            Console.WriteLine("");

            int selectedAmount = Validator.Convert<int>("option:");
            switch (selectedAmount)
            {
                case 1:
                    return 500;
                    break;
                case 2:
                    return 1000;
                    break;
                case 3:
                    return 2000;
                    break;
                case 4:
                    return 5000;
                    break;
                case 5:
                    return 10000;
                    break;
                case 6:
                    return 15000;
                    break;
                case 7:
                    return 20000;
                    break;
                case 8:
                    return 40000;
                    break;
                case 0:
                    return 0;
                    break;
                default:
                    Utility.PrintMessage("Invalid input. try again", false);
                    return -1;
                    break;
            }
        }


        internal InternalTransfer InternalTransferForm()
        {
            var internalTransfer = new InternalTransfer();
            internalTransfer.ReciepeintBankAccountNumber = Validator.Convert<long>("recipient's account number:");
            internalTransfer.TransferAmount = Validator.Convert<decimal>($"amount {cur}");
            internalTransfer.RecipientBankAccountName = Utility.GetUserInput("recipient's name:");
            return internalTransfer;
        }

    }
}
