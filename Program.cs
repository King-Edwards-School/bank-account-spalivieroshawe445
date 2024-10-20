using System.Diagnostics;

namespace Console_Blank_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount testAccount = new BankAccount();
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

        public BankAccount()
        {
            SetName();
            SetNewBalance();
            SetSortCode();
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
        void OutputBalance()
        {
            Console.WriteLine(balance);
        }
        void OutputDetails()
        {
            Console.WriteLine("Name: {0} {1} ", foreName, surName);
            Console.WriteLine("Sort Code: {0} ", sortCode);
            Console.WriteLine("Address: {0}, {1}, {2} ", address[0], address[1], address[2]);
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
        public bool SetAccountNum()
        {
            Console.WriteLine("Enter new account number:");
            try
            {
                accountNum = Convert.ToInt32(Console.ReadLine());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}