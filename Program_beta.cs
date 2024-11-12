//This version uses a text menu; this is not to be used for the final GUI
using System;
using System.Security.Cryptography;

namespace Console_Blank_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank;
            //start of actual code
            Console.WriteLine("Enter the number of accounts to be added:");
            //creates new bank (BankAccounts[]) of length input
            bank = new Bank(Convert.ToInt32(Console.ReadLine()));
            bank.AddAllAccount();
            Console.WriteLine("Bank has been created.");
            bank.Menu();
        }
    }

    //object Bank
    public class Bank
    {
        private BankAccount[] bank;
        private string answer;
        private int accountCount;
        private int accountPos;
        public Bank(int maxAccounts)
        {
            //basic Bank constructor
            bank = new BankAccount[maxAccounts];
            accountCount = maxAccounts;
        }
        public bool AddAllAccount()
        {
            //initial adding of accounts
            for (int i = 0; i < bank.Length; i++)
            {
                bank[i] = new BankAccount();
                Console.WriteLine("Enter a new forename: ");
                answer = Console.ReadLine();
                bank[i].SetForename(answer);
                Console.WriteLine("Enter a new surname: ");
                answer = Console.ReadLine();
                bank[i].SetSurname(answer);
                Console.WriteLine("Set a new balance");
                try
                {
                    double answer = Convert.ToDouble(Console.ReadLine());
                    if (answer >= 0)
                    {
                        bank[i].SetBalance(answer);
                    }
                    else
                    {
                        Console.WriteLine("Input out of range; Balance of account {0} set to 0", i);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Input out of range; Balance of account {0} set to 0", i);
                }
                bank[i].SetAddress();
                bank[i].SetSortCode();
                if (i > 1)
                {
                    for (int j = 1; j < i; j++)
                    {
                        if (bank[i].GetSortCode() == bank[j].GetSortCode())
                        {
                            bank[i].SetSortCode();
                            j = 0;
                        }
                    }
                }
                Console.WriteLine("New sort code is: {0}", bank[i].GetSortCode());
                Console.WriteLine("Account number is: {0}", i + 1);
            }
            return true;
        }
        public bool AddAccounts()
        {
            //add a new account

            Array.Resize(ref bank, bank.Length + 1);
            bank[bank.Length - 1] = new BankAccount();
            accountPos = bank.Length - 1;
            Console.WriteLine("Enter a new forename: ");
            answer = Console.ReadLine();
            bank[accountPos].SetForename(answer);
            Console.WriteLine("Enter a new surname: ");
            answer = Console.ReadLine();
            bank[accountPos].SetSurname(answer);
            Console.WriteLine("Set a new balance");
            try
            {
                double answer = Convert.ToDouble(Console.ReadLine());
                if (answer >= 0)
                {
                    bank[accountPos].SetBalance(answer);
                }
                else
                {
                    Console.WriteLine("Input out of range; Balance of account {0} set to 0", bank.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Balance of account {0} set to 0", bank.Length);
            }
            bank[accountPos].SetAddress();
            bank[accountPos].SetSortCode();
            if (bank.Length > 1)
            {
                for (int j = 1; j < bank.Length; j++)
                {
                    if (bank[accountPos].GetSortCode() == bank[j].GetSortCode() && accountPos != j)
                    {
                        bank[accountPos].SetSortCode();
                        j = 0;
                    }
                }
            }
            Console.WriteLine("New sort code is: {0}", bank[accountPos].GetSortCode());
            Console.WriteLine("Account number is: {0}", bank.Length);
            return true;
        }
        public void Menu()
        {
            while (true)
            {
                string answer;
                BankAccount account;
                int intAnswer;
                int intAnswer2;
                double toWithdraw_Deposit;
                //main menu
                Console.WriteLine("\nFunctions are as follows:\nA to add account\nE to edit an account\nD to delete an account\nR to return details\nW to withdraw\nP to deposit money");
                answer = Console.ReadLine().ToLower();
                //add account
                if (answer == "a")
                {
                    AddAccounts();
                }
                //edit existing account
                else if (answer == "e")
                {
                    try
                    {
                        Console.WriteLine("\nEnter the account number you wish to edit:");
                        intAnswer = Convert.ToInt32(Console.ReadLine().ToLower()) - 1;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        intAnswer = -1;
                    }
                    account = FindAccount(intAnswer);
                    if (account != null)
                    {
                        Console.WriteLine("\nWhat do you want to edit? (for, sur, adr)");
                        answer = Console.ReadLine().ToLower();
                        if (answer == "for" || answer == "f")
                        {
                            Console.WriteLine("Set new forename:");
                            answer = Console.ReadLine();
                            account.SetForename(answer);
                        }
                        else if (answer == "sur" || answer == "s")
                        {
                            Console.WriteLine("Set new surname:");
                            answer = Console.ReadLine();
                            account.SetSurname(answer);
                        }
                        else if (answer == "adr" || answer == "a")
                        {
                            account.SetAddress();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Account not found: It may have been moved or deleted");
                    }
                }
                //delete existing account
                else if (answer == "d")
                {
                    try
                    {
                        Console.WriteLine("\nEnter the account number you wish to delete:");
                        intAnswer = Convert.ToInt32(Console.ReadLine().ToLower());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        intAnswer = -1;
                    }
                    account = FindAccount(intAnswer);
                    Console.WriteLine("Confirm your identity by entering your Sort Code: ");
                    intAnswer2 = Convert.ToInt32(Console.ReadLine());
                    if (account != null && account.GetSortCode() == intAnswer2)
                    {
                        DeleteAccount(intAnswer);
                    }
                    else
                    {
                        Console.WriteLine("Account not found: It may have been moved or deleted, or you may have typed an incorrect Sort Code");
                    }
                }
                else if (answer == "r")
                {
                    //return the details of an account
                    Console.WriteLine("\nEnter the account number to return details: ");
                    try
                    {
                        intAnswer = Convert.ToInt32(Console.ReadLine());
                        account = FindAccount(intAnswer);
                        Console.WriteLine("Confirm your identity by entering your Sort Code: ");
                        intAnswer = Convert.ToInt32(Console.ReadLine());
                        if (account.GetSortCode() == intAnswer)
                        {
                            Console.WriteLine("Confirm your identity by entering your surname:");
                            answer = Console.ReadLine();
                            if (answer == account.GetSurname())
                            {
                                account.OutputDetails();
                                account.OutputBalance();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Account not found: It may have been moved or deleted");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (answer == "w")
                {
                    //withdraw money from the account
                    Console.WriteLine("Enter the account number to withdraw from:");
                    try
                    {
                        intAnswer = Convert.ToInt32(Console.ReadLine());
                        account = FindAccount(intAnswer);
                        if (account != null)
                        {
                            Console.WriteLine("\n||ACCOUNT FOUND||\n");
                            Console.WriteLine("Confirm your identity by entering your Sort Code: ");
                            intAnswer = Convert.ToInt32(Console.ReadLine());
                            if (account.GetSortCode() == intAnswer)
                            {
                                Console.WriteLine("Confirm your identity by entering your surname:");
                                answer = Console.ReadLine();
                                if (answer == account.GetSurname())
                                {
                                    Console.WriteLine("Enter the amount to be withdrawn:");
                                    toWithdraw_Deposit = Convert.ToDouble(Console.ReadLine());
                                    account.Withdraw(toWithdraw_Deposit);
                                }
                                ReturnBalance(intAnswer);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Account not found: It may have been moved or deleted");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (answer == "p")
                {
                    //Deposit money to the account
                    Console.WriteLine("Enter the account number to deposit to:");
                    try
                    {
                        intAnswer2 = Convert.ToInt32(Console.ReadLine());
                        account = FindAccount(intAnswer2);
                        if (account != null)
                        {
                            Console.WriteLine("\n||ACCOUNT FOUND||\n");
                            Console.WriteLine("Confirm your identity by entering your Sort Code: ");
                            intAnswer = Convert.ToInt32(Console.ReadLine());
                            if (account.GetSortCode() == intAnswer)
                            {
                                Console.WriteLine("Confirm your identity by entering your surname:");
                                answer = Console.ReadLine();
                                if (answer == account.GetSurname())
                                {
                                    Console.WriteLine("Enter the amount to be deposited:");
                                    toWithdraw_Deposit = Convert.ToDouble(Console.ReadLine());
                                    account.Deposit(toWithdraw_Deposit);
                                }
                                ReturnBalance(intAnswer2);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Account not found: It may have been moved or deleted");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
        public BankAccount FindAccount(int accountNo)
        {
            //finds an account (for editing or deleting)
            for (int i = 0; i < bank.Length; i++)
            {
                if (i == accountNo - 1)
                {
                    return bank[i];
                }
            }
            return null;
        }
        public bool ReturnBalance(int accountNo)
        {
            //returns just the balance
            accountNo--;
            bank[accountNo].OutputBalance();
            return true;
        }
        public bool DeleteAccount(int accountNumber)
        {
            bank[accountNumber] = null;
            return true;
        }
    }

    public class BankAccount
    {
        private string foreName;
        private string surName;
        private double balance = -1;
        private string accountSort;
        private string[] address = { "ab", "bc", "cd", "DE" };
        private int accountNum;
        private int SortCode;
        private Random random = new Random();

        public BankAccount()
        {
            //basic constructor
            foreName = "John";
            surName = "Smith";
            balance = 0;
        }
        public BankAccount(int accountNumber)
        {
            //constructor used when adding accounts
            SetName();
            SetNewBalance();
            SetAccountNum(accountNumber);
            SetAddress();
            SetSortCode();
        }
        public bool Deposit(double amount)
        {
            //when money is added to an account
            try
            {
                balance = balance + amount;
                Console.WriteLine("\n||DEPOSIT REQUEST APPROVED||\n");
                return true;
            }
            catch
            {
                Console.WriteLine("\n||DEPOSIT REQUEST DENIED||\n");
                return false;
            }
        }
        public bool Withdraw(double amount)
        {
            //opposite of the above (except when you attempt to withdraw more than is available, then rejects)
            if (amount < balance)
            {
                balance = balance - amount;
                Console.WriteLine("\n||WITHDRAWAL REQUEST APPROVED||\n");
                return true;
            }
            else
            {
                Console.WriteLine("\n||WITHDRAWAL REQUEST DENIED||\n");
                return false;
            }
        }
        public void OutputBalance()
        {
            //writes the balance
            Console.WriteLine("Balance: {0} ", balance);
        }
        public void OutputDetails()
        {
            //writes all details to the screen
            Console.WriteLine("\nName: {0} {1} ", foreName, surName);
            Console.WriteLine("Address: {0}, {1}, {2}, {3}, {4} ", address[0], address[1], address[2], address[3], address[4]);
            Console.WriteLine("Sort Code: {0} ", SortCode);
            Console.WriteLine("Account number: {0} ", accountNum + 1);
        }
        public bool SetName()
        {
            //sets the name of the person using this account
            Console.WriteLine("Enter forename:");
            foreName = Console.ReadLine();
            Console.WriteLine("Enter surname:");
            surName = Console.ReadLine();
            return true;
        }
        public bool SetForename(string newForename)
        {
            foreName = newForename;
            return true;
        }
        public bool SetSurname(string newSurname)
        {
            surName = newSurname;
            return true;
        }
        public string GetSurname()
        {
            return surName;
        }
        public bool SetNewBalance()
        {
            //sets the initial value of the balance of the account
            while (balance <= 0)
            {
                Console.WriteLine("Enter new balance:");
                try
                {
                    balance = Convert.ToDouble(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                if (balance > 0)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Balance cannot be less than or equal to 0");
                }
            }
            Console.WriteLine("New balance could not be set");
            return false;
        }
        public bool SetBalance(double newBalance)
        {
            //sets the balance when editing an existing account
            balance = newBalance;
            return true;
        }
        public bool SetAddress()
        {
            try
            {
                do
                {
                    Console.WriteLine("Enter new address in the following format, incl. commas: House/House number, Street, Town, City (repeat Town where appropriate), Postcode");
                    address = Console.ReadLine().Split(',');
                } while (address.Length != 5);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public string[] GetAddress()
        {
            return address;
        }
        public bool SetAccountNum(int accountNumber)
        {
            accountNum = accountNumber;
            return true;
        }
        public bool SetSortCode()
        {
            SortCode = random.Next(0, 999999999);
            return true;
        }
        public int GetSortCode()
        {
            return SortCode;
        }
    }
    public class CurrentAccount : BankAccount
    {
        private double overdraft;
        private string foreName;
        private string surName;
        private double balance = -1;
        private string accountSort;
        private string[] address = { "ab", "bc", "cd", "DE" };
        private int accountNum;
        private int SortCode;
        private Random random = new Random();

        public bool DepositToBalance(double amount)
        {
            //when money is added to an account
            try
            {
                balance = balance + amount;
                Console.WriteLine("\n||DEPOSIT REQUEST APPROVED||\n");
                return true;
            }
            catch
            {
                Console.WriteLine("\n||DEPOSIT REQUEST DENIED||\n");
                return false;
            }
        }
        public bool Withdraw(double amount)
        {
            //opposite of the above (except when you attempt to withdraw more than is available, then rejects)
            if (amount < (balance + overdraft))
            {
                if (balance - amount <= 0)
                {
                    overdraft = overdraft + (balance - amount);
                    balance = 0;
                    Console.WriteLine("\n||WITHDRAWAL REQUEST APPROVED||\n");
                    return true;
                }
                else
                {
                    balance = balance - amount;
                    Console.WriteLine("\n||WITHDRAWAL REQUEST APPROVED||\n");
                    return true;
                }
            }
            else
            {
                Console.WriteLine("\n||WITHDRAWAL REQUEST DENIED||\n");
                return false;
            }
        }
    }
}
