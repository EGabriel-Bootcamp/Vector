using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankProject.Display;
using BankProject.Domain.Enitities;
using BankProject.Domain.Enums;
using ConsoleTables;
using Newtonsoft.Json;

namespace BankProject.AppServices
{
    public class UserServices : IUserLogin, IUserAccountActions, ITransaction
    {
       // private  List<UserAccount> userAccountList;
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;
        private List<Transaction> _ListOfTransactions;

        private const decimal minimumKeptAmount = 500;

        private readonly AppScreen screen;

        public UserServices()
        {
            screen = new AppScreen();   
            userAccountList = new List<UserAccount>();
        }



        public void OnBoard()
        {
            Console.WriteLine("Do you have an account, if NO enter 1 to REGISTER if YES enter 2 to LOGIN...");

            switch (Validator.Convert<int>("an option:"))
            {
                case (int)OnBoarding.Register:
                    Regiser();
                    break;
                case (int)OnBoarding.Login:
                    Run();
                    break;
                default:
                    Utility.PrintMessage("Invalid Options..", false);
                    break;
            }
            

        }

        public void Run()
        {
            AppScreen.Welcome();

            CheckUserCardNumAndPassword();

            AppScreen.WelcomeCustomer(selectedAccount.FullName);
            while (true)
            {
                AppScreen.DisplaayAppMenu();

                ProcessMenuoption();
            }
          
        }


        public void Regiser()
        {
            AddNewMember();
            InitializeData();
            Run();
        }


        public void AddNewMember()
        {
            string filePath = @"C:\Users\USER\Desktop\Project\BankProject\BankProject\Test.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();
            List<UserAccount> UserAccountList = new List<UserAccount>();

            int id;
            string fullName;
            long accountNumber;
            long cardNumber;
            int cardPin;
            decimal accountBalance;
            int totalLogin;
            bool isLocked;

            Console.WriteLine("Kindly fill in the following details");
            Console.WriteLine("");
            Console.WriteLine("Kindly fill in your Id: ");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Kindly fill in your fullname: ");
            fullName = Console.ReadLine();
            Console.WriteLine("Kindly fill in your accountNumber: ");
            accountNumber = long.Parse(Console.ReadLine());
            Console.WriteLine("Kindly fill in your Card Number: ");
            cardNumber = long.Parse(Console.ReadLine());
            Console.WriteLine("fill in yur card pin:");
            cardPin = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("fill you your first deposite: ");
            accountBalance = long.Parse(Console.ReadLine());
            Console.WriteLine("fill in you total login as 0");
            totalLogin = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("locked access count: ");
            isLocked = bool.Parse(Console.ReadLine());

           UserAccountList.Add(new UserAccount
            {
                Id = id,
                FullName = fullName,
                AccountNumber = accountNumber,
                CardNumber = cardNumber,
                CardPin = cardPin,
                AccountBalance = accountBalance,
                TotalLogin = totalLogin,
                IsLocked = isLocked
            });

            List<string> output = new List<string>();

            foreach (var userAccount in UserAccountList)
            {
                output.Add($"{userAccount.Id},{userAccount.FullName},{userAccount.AccountNumber}," +
                    $"{userAccount.CardNumber}, {userAccount.CardPin}," +
                    $"{userAccount.AccountBalance}, {userAccount.TotalLogin}," +
                    $"{userAccount.IsLocked}");
            }
            Console.WriteLine("Writting to text file");

            File.AppendAllLines(filePath, output);

           // File.WriteAllLines(filePath, output);

            Console.WriteLine("All entries");

            Console.WriteLine("done.....");

        }


        public void InitializeData()
        {
            string filePath = @"C:\Users\DELL\source\repos\Vector\BankProject\Test.txt";
            List<UserAccount> UserAccountList = new List<UserAccount>();
            List<string> lines = File.ReadAllLines(filePath).ToList();
            foreach (string line in lines)
            {
                string[] entries = line.Split(',');
                UserAccount userAccount = new UserAccount();

                userAccount.Id = int.Parse(entries[0]);
                userAccount.FullName = entries[1];
                userAccount.AccountNumber = long.Parse(entries[2]);
                userAccount.CardNumber = long.Parse(entries[3]);
                userAccount.CardPin = Int32.Parse(entries[4]);
                userAccount.AccountBalance = Convert.ToDecimal(entries[5]);
                userAccount.TotalLogin = int.Parse(entries[6]);
                userAccount.IsLocked = bool.Parse(entries[7]);

                userAccountList.Add(userAccount);
            }

            _ListOfTransactions = new List<Transaction>();

        }

        public void CheckUserCardNumAndPassword()
        {
            int count = 0;
            bool isCorrectLogin = false;
            while (isCorrectLogin == false)
            {
                UserAccount inputAccount = AppScreen.UserLoginForm();
                AppScreen.LoginProgress();


                ///
                //if
        
                    ////
                    foreach (UserAccount account in userAccountList)
                    {
                    count++;
                    selectedAccount = account;
                    if (inputAccount.CardNumber.Equals(selectedAccount.CardNumber))
                    {
                        selectedAccount.TotalLogin++;

                        if (inputAccount.CardPin.Equals(selectedAccount.CardPin))
                        {
                            selectedAccount = account;

                            if (selectedAccount.IsLocked || selectedAccount.TotalLogin > 3)
                            {
                                AppScreen.PrintLockScreen();
                            }
                            else
                            {
                                selectedAccount.TotalLogin = 0;
                                isCorrectLogin = true;
                                break;
                            }
                        }
                    }
                    if (count < userAccountList.Count)
                    {
                        continue;
                    }
                    if (count >= userAccountList.Count)
                    {
                        if (isCorrectLogin == false)
                        {
                            Utility.PrintMessage("\nInvalid card number or PIN.", false);
                            selectedAccount.IsLocked = selectedAccount.TotalLogin == 3;
                            if (selectedAccount.IsLocked)
                            {
                                AppScreen.PrintLockScreen();
                            }
                        }
                    }
                    ///if not end of the list do not go futher untill you check the whole list
                    //if (isCorrectLogin == false)
                    //{
                    //    Utility.PrintMessage("\nInvalid card number or PIN.", false);
                    //    selectedAccount.IsLocked = selectedAccount.TotalLogin == 3;
                    //    if (selectedAccount.IsLocked)
                    //    {
                    //        AppScreen.PrintLockScreen();
                    //    }
                    //}
                    Console.Clear();
                }
            }

        }

        private void ProcessMenuoption()
        {
            switch(Validator.Convert<int>("an option:"))
            {
                case (int)BankAppMenu.CheckBalance:
                    CheckBalance();
                    break;
                case (int)BankAppMenu.PlaceDeposit:
                    PlaceDeposit();
                    break;
                case (int)BankAppMenu.MakeWithdrawal:
                    MakeWithDrawal();
                    break;
                case (int)BankAppMenu.InternalTransfer:
                    var internalTransfer = screen.InternalTransferForm();
                    ProcessInternalTransfer(internalTransfer);
                    break;
                case (int)BankAppMenu.ViewTransaction:
                    ViewTransaction();
                    break;
                case (int)BankAppMenu.Logout:
                    AppScreen.LogOutProgress();
                    Utility.PrintMessage("You have successfully logged out.", true);
                    Run();
                    break;
                default:
                    Utility.PrintMessage("Invalid Options..", false);
                    break;
            }
        }

        //check balance
        public void CheckBalance()
        {
            Utility.PrintMessage($"Your account balance is: {Utility.FormatAmount(selectedAccount.AccountBalance)}", true);
        }

        public void PlaceDeposit()
        {
            Console.WriteLine("\nOnly multiple of 500 and 1000 naira allowed.\n");

            var transaction_amt = Validator.Convert<int>($"amount {AppScreen.cur}");

            //simulate counting
            Console.WriteLine("\nChecking and Counting bank notes.");
            Utility.PrintDotAnimation();

            Console.WriteLine("");

            if(transaction_amt <= 0)
            {
                Utility.PrintMessage("Amount needs to be greater than 0", false);
                return;
            }
            if (transaction_amt % 500 != 0)
            {
                Utility.PrintMessage($"Enter deposit amount of mutiple of 500 or 1000. Tray again.", false);
                return;
            }

            if (PreviewBankNotesCount(transaction_amt) == false)
            {
                Utility.PrintMessage($"You have cancalled your action.", false);
                return;
            }

            //bind transaction detatails to the transaction object
            InsertTransaction(selectedAccount.Id, TransactionType.Deposit, transaction_amt, "");

            //Update Account balance
            selectedAccount.AccountBalance += transaction_amt;

            //print success message to the screen
            Utility.PrintMessage($"Your deposit of {Utility.FormatAmount(transaction_amt)} was successful", true);


        }


        //Make withdrawal method
        public void MakeWithDrawal()
        {
            var transaction_amt = 0;
            int selectedAmount = AppScreen.SelectAmonunt();

            if (selectedAmount == -1)
            {
                //reccuive calling of the same method.
                MakeWithDrawal();
                return;
            }
            else if (selectedAmount != 0)
            {
                transaction_amt = selectedAmount;
            }
            else
            {
                //if user select 0
                transaction_amt = Validator.Convert<int>($"amount{AppScreen.cur}");
            }

            //input validation
            if(transaction_amt <= 0)
            {
                Utility.PrintMessage("Amount must be greater than zero.", false);
                    return;
            }
            if (transaction_amt % 500 != 0)
            {
                Utility.PrintMessage("You can only Withdraw amount in multiples of 5000 or 1000", false);
                return;
            }

            //Logic validations
            if(transaction_amt > selectedAccount.AccountBalance)
            {
                Utility.PrintMessage($"Withdralal failed. Your balance is too low {Utility.FormatAmount(transaction_amt)}", false);
                return;
            }

            if ((selectedAccount.AccountBalance - transaction_amt) < minimumKeptAmount)
            {
                Utility.PrintMessage($"Withdrawal failed. Your account needs to have " +
                    $"minimum {Utility.FormatAmount(minimumKeptAmount)}", false);
                return;
            }

            //Bind withdrawal details to transaction object
            InsertTransaction(selectedAccount.Id, TransactionType.Withdrawal, -transaction_amt, "");
            //update account balance
            selectedAccount.AccountBalance -= transaction_amt;
            //success message
            Utility.PrintMessage($"You have successfully withdrawn " +
                $"{Utility.FormatAmount(transaction_amt)}.", true);

        }


        private bool PreviewBankNotesCount(int amount)
        {
            int thousandNotesCount = amount / 1000;
            int fiveHundredNotesCount = (amount % 1000) / 500;

            Console.WriteLine("\nSummary");
            Console.WriteLine("-------");
            Console.WriteLine($"{AppScreen.cur}1000 X {thousandNotesCount} = {1000 * thousandNotesCount}");
            Console.WriteLine($"{AppScreen.cur}500 X {fiveHundredNotesCount} = {500 * fiveHundredNotesCount}");
            Console.WriteLine($"Total amount: {Utility.FormatAmount(amount)}\n\n");

            int opt = Validator.Convert<int>("1 to confirm");
            return opt.Equals(1);

        }

        public void InsertTransaction(long _userBankAccountId, TransactionType _tranType, decimal _tranAmount, string _desc)
        {
            //Create a new Transaction object
            var transaction = new Transaction()
            {
                TransactionId = Utility.GetTransactionId(),
                UserBankAccountId = _userBankAccountId,
                TransactionDate = DateTime.Now,
                TransactionType = _tranType,
                TransactionAmount = _tranAmount,
                Descriprion = _desc
            };

            _ListOfTransactions.Add(transaction);
        }

        public void ViewTransaction()
        {
            var filteredTransactionList = _ListOfTransactions.Where(t => t.UserBankAccountId == selectedAccount.Id).ToList();
            //check if there's a transaction
            if (filteredTransactionList.Count <= 0)
            {
                Utility.PrintMessage("You have no transaction yet.", true);
            }
            else
            {
                //Install a console table to draw the table.
                var table = new ConsoleTable("Id", "Transaction Date", "Type", "Descriptions", "Amount " + AppScreen.cur);
                foreach (var tran in filteredTransactionList)
                {
                    table.AddRow(tran.TransactionId, tran.TransactionDate, tran.TransactionType, tran.Descriprion, tran.TransactionAmount);
                }
                table.Options.EnableCount = false;
                table.Write();
                Utility.PrintMessage($"You have {filteredTransactionList.Count} transaction(s)", true);
            }

        }

        private void ProcessInternalTransfer(InternalTransfer internalTransfer)
        {
            if (internalTransfer.TransferAmount <= 0)
            {
                Utility.PrintMessage("Amount needs to be more than 0. Try again", false);
                return;
            }

            //check sender's account balance
            if(internalTransfer.TransferAmount > selectedAccount.AccountBalance)
            {
                Utility.PrintMessage($"Transfered failed, Insufficeint funds{Utility.FormatAmount(internalTransfer.TransferAmount)}", false);
                return;
            }

            //check the minimum kept amount 
            if ((selectedAccount.AccountBalance - internalTransfer.TransferAmount) < minimumKeptAmount)
            {
                Utility.PrintMessage($"Transfer faile. Your account needs to have minimum" +
                    $" {Utility.FormatAmount(minimumKeptAmount)}", false);
                return;
            }

            //check reciever's account number is valid  ***************************************************************
            var selectedBankAccountReciever = (from userAcc in userAccountList
                                               where userAcc.AccountNumber == internalTransfer.ReciepeintBankAccountNumber
                                               select userAcc).FirstOrDefault();
            if (selectedBankAccountReciever == null)
            {
                Utility.PrintMessage("Transfer failed. Recieber bank account number is invalid.", false);
                return;
            }
            //check receiver's name
            if (selectedBankAccountReciever.FullName != internalTransfer.RecipientBankAccountName)
            {
                Utility.PrintMessage("Transfer Failed. Recipient's bank account name does not match.", false);
                return;
            }

            //add transaction to transactions record- sender
            InsertTransaction(selectedAccount.Id, TransactionType.Transfer, -internalTransfer.TransferAmount, "Transfered " +
                $"to {selectedBankAccountReciever.AccountNumber} ({selectedBankAccountReciever.FullName})");
            //update sender's account balance
            selectedAccount.AccountBalance -= internalTransfer.TransferAmount;

            //add transaction record-reciever
            InsertTransaction(selectedBankAccountReciever.Id, TransactionType.Transfer, internalTransfer.TransferAmount, "Transfered from " +
                $"{selectedAccount.AccountNumber}({selectedAccount.FullName})");
            //update reciever's account balance
            selectedBankAccountReciever.AccountBalance += internalTransfer.TransferAmount;
            //print success message
            Utility.PrintMessage($"You have successfully transfered" +
                $" {Utility.FormatAmount(internalTransfer.TransferAmount)} to " +
                $"{internalTransfer.RecipientBankAccountName}", true);

        }
    }

}




