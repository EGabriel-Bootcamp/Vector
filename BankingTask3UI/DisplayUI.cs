using System;
using BankingTask3Core;
using BankingTask3General;
using BankingTask3Model;

namespace BankingTask3UI
{
    public class DisplayUI
    {
        readonly Validation validation = new Validation();
        CustomerCore customerCore = new CustomerCore();
        BankAccountCore bankAccountCore = new BankAccountCore();
        //TransactionCore transactionCore = new TransactionCore();
        //make changes
        PrintTable printTable = new PrintTable();
        string currentAcctType = AccountType.accountType.Current.ToString();
        string savingsAcctType = AccountType.accountType.Savings.ToString();
        string newEmail;
        string newPassword;
        string newPhoneNumber;
        const string options = @"Press:
                      1   To Login If you already have an account
                      2   To Create an Account if you do not have an account
                      3   To Quit";
        const string DashboardOptions = @"Press:
                      1   To Deposit
                      2   To Withdraw
                      3   To Transfer
                      4   To Create new account
                      5   To Print account details
                      6   To Print statement of account
                      7   To Get your account balance
                      8   To Logout";
        const string confirmOptions = @"Press:
                      1   To Retry
                      2   To Create Account";
        public void StartUI()
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t********Welcome to Banking Console App*********");
            Console.WriteLine("\t\t------------------------------------------------------------");
            Console.ResetColor();
            string inputLine;
            do
            {
                Console.WriteLine(options);
                Console.Write(">>>>>  ");
                inputLine = Console.ReadLine().Trim();
                if (inputLine.Equals("1"))
                {
                    Console.Clear();
                    LoginInput();
                }
                else if (inputLine.Equals("2"))
                {
                    Console.Clear();
                    CreateAccount();
                }
                else if (inputLine.Equals("3"))
                {
                    Environment.Exit(0);
                }
                else
                {
                    // If the user type something that is not recognised by the program
                    Console.WriteLine("Command not recognised, please enter a valid input as stated above");
                }
            } while (!inputLine.Equals("") && !inputLine.Equals("exit"));

        }

        private void LoginInput()
        {
            string email;
            string password;

            while (true)
            {
                Console.WriteLine("Enter your Email: ");
                email = Console.ReadLine();
                if (validation.CheckEmailInput(email))
                {
                    break;
                }
                Console.WriteLine("Please input valid email, like azeezOla56@gmail.com");
            }

            while (true)
            {
                Console.WriteLine("Enter your Password: ");
                password = Console.ReadLine();
                if (validation.CheckPasswordInput(password))
                {
                    break;
                }
                Console.WriteLine("Please input right format, like ola@12");
            }

            var check = customerCore.Login(email, password);
            var name = customerCore.GetName(email, password);
            var accountNumber = bankAccountCore.GetAccountNumberByName(name);
            if (check)
            {
                Console.WriteLine("You are welcome {0}", name);
                Dashboard(accountNumber, name);
            }
            else
            {
                Console.WriteLine("{0}", check);
                UserLoginConfirmation();
            }
        }
        private void UserLoginConfirmation()
        {
            Console.WriteLine(confirmOptions);
            Console.Write(">>>>>  ");
            string inputLine;
            inputLine = Console.ReadLine().Trim();
            if (inputLine.Equals("1"))
            {
                Console.Clear();
                LoginInput();
            }
            else if (inputLine.Equals("2"))
            {
                Console.Clear();
                CreateAccount();
            }
            else
            {
                // If the user type something that is not recognised by the program
                Console.WriteLine("Command not recognised, please enter a valid input as stated above");
            }
        }
        private void CreateAccount()
        {
            string firstName;
            string lastName;
            string email;
            string password;
            string accountType;
            string phoneNumber;
            decimal depositAmount;
            bool check;

            while (true)
            {
                Console.WriteLine("Enter your First Name: ");
                firstName = Console.ReadLine();
                if (validation.CheckNameInput(firstName))
                {
                    break;
                }
                Console.WriteLine("Please input right format like Azeez");
            }

            while (true)
            {
                Console.WriteLine("Enter your Last Name: ");
                lastName = Console.ReadLine();
                if (validation.CheckNameInput(lastName))
                {
                    break;
                }
                Console.WriteLine("Please input right format like Ola");
            }

            while (true)
            {
                Console.WriteLine("Enter your Email: ");
                email = Console.ReadLine();
                if (validation.CheckEmailInput(email))
                {
                    break;
                }
                Console.WriteLine("Please input right format like azeezOla56@gmail.com");
            }

            while (true)
            {
                Console.WriteLine("Enter your Password: ");
                password = Console.ReadLine();
                if (validation.CheckPasswordInput(password))
                {
                    break;
                }
                Console.WriteLine("Please input right format like ola@12");
            }

            while (true)
            {
                Console.WriteLine("Enter your phone number: ");
                phoneNumber = Console.ReadLine();
                if (validation.CheckPhoneNumber(phoneNumber))
                {
                    break;
                }
                Console.WriteLine("Please input right format like 081*****");
            }

            while (true)
            {
                Console.WriteLine("Please specify the Account type(Enter 1 for savings or 2 for current): ");
                accountType = Console.ReadLine().Trim().ToLower();
                if (accountType == "1") 
                {
                    accountType = savingsAcctType;
                    break;
                }
                else if (accountType == "2")
                {
                    accountType = currentAcctType;
                    break;
                }
                Console.WriteLine("Please input correct format \"1\" or \"2\"");
            }

            while (true)
            {
                Console.WriteLine("Fund your account: ");
                string inputAmount = Console.ReadLine();
                (check,depositAmount)= validation.CheckAmount(inputAmount);
                if (check && accountType != currentAcctType && depositAmount < 1000)
                {
                    Console.WriteLine("You can only deposit 1000 and above");
                }
                else
                {
                    break;
                }

            }

            newEmail = email;
            newPassword = password;
            newPhoneNumber = phoneNumber;
            Console.WriteLine(depositAmount);
            customerCore.CustomerRegistration(firstName + " " + lastName, email, password,
                accountType, phoneNumber);
            long accountNumber = customerCore.accountNumber - 1;
            bankAccountCore.CreateBankAccount(firstName + " " + lastName, depositAmount, accountType, accountNumber);
            Console.WriteLine("Your AccountNumber is {0}", accountNumber);
            Console.WriteLine("You have successfully opened an account");
        }

        private void Dashboard(long initialAccountNumber, string name)
        {
            string input;
            do
            {
                Console.WriteLine("Your Account Number is {0}", initialAccountNumber);
                Console.WriteLine("Your Balance is {0}", bankAccountCore.GetBalance(initialAccountNumber));
                Console.WriteLine(DashboardOptions);
                Console.Write(">>>>>  ");
                input = Console.ReadLine().Trim();
                if (input.Equals("1"))
                {
                    DepositMethod();
                }
                else if (input.Equals("2"))
                {
                    WithdrawMethod(initialAccountNumber, name);
                }
                else if (input.Equals("3"))
                {
                    TransferMethod(initialAccountNumber, name);
                }
                else if (input.Equals("4"))
                {
                    CreateNewAccount(initialAccountNumber, name);
                }
                else if (input.Equals("5"))
                {
                    PrintAccountDetails(name);
                }
                else if (input.Equals("6"))
                {
                    PrintAccountStatement();
                }
                else if (input.Equals("7"))
                {
                    GetBalanceOfAccount();
                }
                else if (input.Equals("8"))
                {
                    StartUI();
                }
                else
                {
                    // If the user type something that is not recognised by the program
                    Console.WriteLine("Command not recognised, please enter a valid input as stated above");
                }
            } while (!input.Equals("") && !input.Equals("exit"));
        }

        private void GetBalanceOfAccount()
        {
            long accountNumber;
            bool confirm;
            while (true)
            {
                Console.WriteLine("Enter valid Account Number  :");
                string checkAccountNumber = Console.ReadLine();
                (confirm,accountNumber) = validation.CheckAccountNumber(checkAccountNumber);
                if (confirm)
                {
                    break;
                }
                Console.WriteLine("Account number should be in 10 digit numbers");
            }

            bankAccountCore.GetBalance(accountNumber);
        }

        private void DepositMethod()
        {
            decimal depositAmount;
            string description;
            bool confirm;
            bool checker;
            long accountNumber;
            while (true)
            {
                Console.WriteLine("Enter valid Account Number  :");
                string checkAccountNumber = Console.ReadLine();
                (confirm, accountNumber) = validation.CheckAccountNumber(checkAccountNumber);
                if (confirm)
                {
                    break;
                }
                Console.WriteLine("Account number should be in 10 digit numbers");
            }
            if (!bankAccountCore.CheckAccountNumberIfExists(accountNumber))
            {
                Console.WriteLine("This Account Number is Not Found please re-enter your account number");
                DepositMethod();
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("How much would you like to deposit?: ");
                    string inputAmount = Console.ReadLine();
                    (checker, depositAmount) = validation.CheckAmount(inputAmount);
                    if (checker)
                    {
                        break;
                    }
                    Console.WriteLine("Please enter a valid input: ");
                }
                Console.WriteLine("Enter the reason for deposit");
                description = Console.ReadLine();
                bankAccountCore.MakeDeposit("deposit", depositAmount, description, accountNumber);
            }
        }

        private void WithdrawMethod(long initialAccountNumber, string name)
        {
            decimal withdrawAmount;
            string description;
            long accountNumber;
            bool confirm;
            bool checker;
            while (true)
            {
                Console.WriteLine("Enter valid Account Number  :");
                string checkAccountNumber = Console.ReadLine();
                (confirm, accountNumber) = validation.CheckAccountNumber(checkAccountNumber);
                if (confirm)
                {
                    break;
                }
                Console.WriteLine("Account number should be in 10 digit numbers");
            }
            decimal balance = bankAccountCore.GetBalance(accountNumber);
            Console.WriteLine("Available Balance: {0}", balance);
            string accountType = bankAccountCore.GetAccountType(name);
            if (balance == 1000 && accountType == "Savings")
            {
                Console.WriteLine("You can not withdraw, Fund this account Thank you");
                Dashboard(initialAccountNumber, name);
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("How much would you like to withdraw?: ");
                    string inputAmount = Console.ReadLine();
                    (checker, withdrawAmount) = validation.CheckAmount(inputAmount);
                    if (checker)
                    {
                        break;
                    }
                    Console.WriteLine("Please enter a valid input: ");
                }
                Console.WriteLine("Enter the reason for withdraw");
                description = Console.ReadLine();
                bankAccountCore.MakeWithdraw("Withdrawal", withdrawAmount, description, accountNumber);
            }
        }

        private void TransferMethod(long initialAccountNumber, string name)
        {
            decimal transferAmount;
            string description;
            long debitAccountNumber;
            long creditAccountNumber;
            while (true)
            {
                Console.WriteLine("Enter valid Debit Account Number :");
                bool confirm = long.TryParse(Console.ReadLine(), out debitAccountNumber);
                if (confirm)
                {
                    break;
                }
                Console.WriteLine("Account number should be in 10 digit numbers");
            }
            decimal balance = bankAccountCore.GetBalance(debitAccountNumber);
            Console.WriteLine("Available Balance: {0}", balance);
            string accountType = bankAccountCore.GetAccountType(name);
            if (balance == 1000 && accountType == "Savings")
            {
                Console.WriteLine("You can not withdraw, Fund this account Thank you");
                Dashboard(initialAccountNumber, name);
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("Enter valid Credit Account Number :");
                    bool confirm = long.TryParse(Console.ReadLine(), out creditAccountNumber);
                    if (confirm)
                    {
                        break;
                    }
                    Console.WriteLine("Account number should be in 10 digit numbers");
                }
                while (true)
                {
                    Console.WriteLine("How much would you like to withdraw?: ");
                    bool checker = decimal.TryParse(Console.ReadLine(), out transferAmount);
                    if (checker)
                    {
                        break;
                    }
                    Console.WriteLine("Please enter a valid input: ");
                }
                Console.WriteLine("Enter the reason for withdraw");
                description = Console.ReadLine();
                bankAccountCore.MakeTransfer("Transfer", transferAmount, description, debitAccountNumber, creditAccountNumber);
            }
        }

        private void CreateNewAccount(long initialAccountNumber, string name)
        {
            string accountType;
            decimal depositAmount;
            while (true)
            {
                Console.WriteLine("Please specify the Account type(Enter 1 for savings or 2 for current): ");
                accountType = Console.ReadLine().Trim().ToLower();
                if (accountType == "2")
                {
                    accountType = currentAcctType;
                    break;
                }
                else if (accountType == "1")
                {
                    accountType = savingsAcctType;
                    break;
                }
                Console.WriteLine("Please input correct format \"1\" or \"2\"");
            }
            while (true)
            {
                Console.WriteLine("Fund your account: ");
                bool check = decimal.TryParse(Console.ReadLine(), out depositAmount);
                if (check && accountType != currentAcctType && depositAmount < 1000)
                {
                    Console.WriteLine("You can only deposit 1000 and above");
                }
                else
                {
                    break;
                }

            }
            customerCore.CustomerRegistration(name, newEmail, newPassword, accountType, newPhoneNumber);
            long accountNumber = customerCore.accountNumber - 1;
            bankAccountCore.CreateBankAccount(name, depositAmount, accountType, accountNumber);
            Console.WriteLine("Your AccountNumber is {0}", accountNumber);
            Console.WriteLine("You have successfully opened an account");
        }

        private void PrintAccountDetails(string name)
        {
            printTable.PrintTableForAccountDetails(name);
        }

        private void PrintAccountStatement()
        {
            long accountNumber;
            while (true)
            {
                Console.WriteLine("Enter valid Debit Account Number :");
                bool confirm = long.TryParse(Console.ReadLine(), out accountNumber);
                if (confirm)
                {
                    break;
                }
                Console.WriteLine("Account number should be in 10 digit numbers");
            }
            printTable.PrintTableForAccountStatement(accountNumber);
        }

    }
}
