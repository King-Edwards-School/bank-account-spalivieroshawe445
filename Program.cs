using System;

namespace Console_Blank_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank;
			string answer;
            Console.WriteLine("Enter the number of accounts to be added:");
            try
            {
                bank = new Bank(Convert.ToInt32(Console.ReadLine()));
                bank.AddAllAccount();
				Console.WriteLine("Bank has been created. Functions are as follows:/nA to add account/nE to edit an account/nD to delete an account");
				while(true)
				{
					if (answer == "A")
					{
						bank.AddAccounts();
					}
				}
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
		private string answer;
        public Bank(int maxAccounts)
        {
            bank = new BankAccount[maxAccounts];
        }
        public bool AddAllAccount()
        {
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
						Console.WriteLine("Balance of account {0} set to 0", i);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					Console.WriteLine("Balance of account {0} set to 0", i + 1);
				}
				Console.WriteLine("Enter the new sort code");
				sortCode = Console.ReadLine();
				Console.WriteLine("New sort code is: {0}", sortCode);
            }
            return true;
        }
        public bool AddAccounts()
        {
            return false;
        }
		public bool FindAccount(int acountNo)
		{
			for (int i = 0; i < bank.Length; i++)
			{
				if (i = accountNo)
				{
					
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
		private string accountSort;
        private string[] address = new string[4];
        private int accountNum;
		public BankAccount()
		{
			foreName = "John";
			surName = "Smith";
			balance = 0;
			
			address = ["ab", "bc", "cd", "DE"];
		}
        public BankAccount(int accountNumber)
        {
            SetName();
            SetNewBalance();
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
            Console.WriteLine("Sort Code: {0} ", accountSort);
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
		public bool SetBalance(double newBalance)
		{
			balance = newBalance;
			return true;
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
