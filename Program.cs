using System;

namespace Console_Blank_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank;
			BankAccount account;
			string answer;
			int intAnswer;
			double doubleAnswer;
            Console.WriteLine("Enter the number of accounts to be added:");
			bank = new Bank(Convert.ToInt32(Console.ReadLine()));
			bank.AddAllAccount();
			Console.WriteLine("Bank has been created.");
			while(true)
			{
				Console.WriteLine("Functions are as follows:\nA to add account\nE to edit an account\nD to delete an account\nR to return details");
				answer = Console.ReadLine().ToLower();
				if (answer == "a")
				{
					bank.AddAccounts();
				}
				else if (answer == "e")
				{
					try
					{
						intAnswer = Convert.ToInt32(Console.ReadLine().ToLower());
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
						intAnswer = - 1;
					}
					account = bank.FindAccount(intAnswer);
					if (account != null)
					{
						Console.WriteLine("What do you want to edit? (for, sur, bal, adr)");
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
						else if (answer == "bal" || answer == "b")
						{
							Console.WriteLine("Set a new balance:");
							try
							{
								doubleAnswer = Convert.ToDouble(Console.ReadLine());
								account.SetBalance(intAnswer);
							}
							catch
							{
								account.SetBalance(-1);
							}
						}
						else if (answer == "adr" || answer == "a")
						{
							account.SetAddress();
						}
					}
					else if (answer == "d")
					{
						try
						{
							intAnswer = Convert.ToInt32(Console.ReadLine().ToLower());
						}
						catch (Exception e)
						{
							Console.WriteLine(e.Message);
							intAnswer = - 1;
						}
						account = bank.FindAccount(intAnswer);
						if (account != null)
						{
							account = null;
						}
					}
				}
			}
        }
    }

    public class Bank
    {
        private BankAccount[] bank;
        private string sortCode;
		private string answer;
		private int accountCount;
        public Bank(int maxAccounts)
        {
            bank = new BankAccount[maxAccounts];
			accountCount = maxAccounts;
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
				Console.WriteLine("Account number is: {0}", i + 1);
            }
            return true;
        }
        public bool AddAccounts()
        {
			Array.Resize(ref bank, bank.Length + 1);
			bank[bank.Length - 1] = new BankAccount();
			Console.WriteLine("Enter a new forename: ");
			answer = Console.ReadLine();
			bank[bank.Length - 1].SetForename(answer);
			Console.WriteLine("Enter a new surname: ");
			answer = Console.ReadLine();
			bank[bank.Length - 1].SetSurname(answer);
			Console.WriteLine("Set a new balance");
			try
			{
				double answer = Convert.ToDouble(Console.ReadLine());
				if (answer >= 0)
				{
					bank[accountCount].SetBalance(answer);
				}
				else
				{
					Console.WriteLine("Balance of account {0} set to 0", bank.Length);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine("Balance of account {0} set to 0", bank.Length + 1);
			}
			Console.WriteLine("Enter the new sort code");
			sortCode = Console.ReadLine();
			Console.WriteLine("New sort code is: {0}", sortCode);
            return true;
        }
		public BankAccount FindAccount(int accountNo)
		{
			for (int i = 0; i < bank.Length; i++)
			{
				if (i == accountNo)
				{
					return bank[i];
				}
			}
			return null;
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
