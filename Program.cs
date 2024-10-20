using System.Diagnostics;

namespace Console_Blank_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank;
            Console.WriteLine("Enter the number of accounts to be added:");
            try
            {
                bank = new Bank(Convert.ToInt32(Console.ReadLine()));
                bank.AddAllAccount();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public class Bank
    {
        private BankAccount[] bank;
        private string sortCode;
        public Bank(int maxAccounts)
        {
            bank = new BankAccount[maxAccounts];
        }
        public bool AddAllAccount()
        {
            for (int i = 0; i < bank.Length; i++)
            {
                bank[i] = new BankAccount(i);
            }
            return true;
        }
        public bool AddAccounts()
        {
            return false;
        }
        public bool ReturnAccountDetails(int accountNo)
        {
            accountNo--;
            bank[accountNo].OutputDetails();
            bank[accountNo].OutputBalance();
            return true;
        }
    }

    public class BankAccount
    {
        private string foreName;
        private string surName;
        private double balance = -1;
        private string sortCode;
        private string[] address;
        private int accountNum;

        public BankAccount(int accountNumber)
        {
            SetName();
            SetNewBalance();
            SetSortCode();
            SetAccountNum(accountNumber);
            SetAddress();
        }
        public bool Deposit(double amount)
        {
            try
            {
                balance = balance + amount;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Withdraw(double amount)
        {
            if (amount < balance)
            {
                balance = balance - amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void OutputBalance()
        {
            Console.WriteLine(balance);
        }
        public void OutputDetails()
        {
            Console.WriteLine("Name: {0} {1} ", foreName, surName);
            Console.WriteLine("Sort Code: {0} ", sortCode);
            Console.WriteLine("Address: {0}, {1}, {2}, {3} ", address[0], address[1], address[2], address[3]);
            Console.WriteLine("Account number: {0} ", accountNum);
        }
        public bool SetName()
        {
            Console.WriteLine("Enter forename:");
            foreName = Console.ReadLine();
            Console.WriteLine("Enter surname:");
            surName = Console.ReadLine();
            return true;
        }
        public bool SetNewBalance()
        {
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
        public bool SetSortCode()
        {
            Console.WriteLine("Enter new sort code:");
            try
            {
                sortCode = (Console.ReadLine());
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetAddress()
        {
            Console.WriteLine("Enter new address in the following format, incl. commas: House Street, Town, City (repeat Town where appropriate), Postcode");
            try
            {
                address = Console.ReadLine().Split(", ");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool SetAccountNum(int accountNumber)
        {
            accountNum = accountNumber;
            return true;
        }
    }
}