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
			double toWithdraw_Deposit;
			//start of actual code
            Console.WriteLine("Enter the number of accounts to be added:");
			//creates new bank (BankAccounts[]) of length input
			bank = new Bank(Convert.ToInt32(Console.ReadLine()));
			bank.AddAllAccount();
			Console.WriteLine("Bank has been created.");
			while(true)
			{
				//main menu
				Console.WriteLine("\nFunctions are as follows:\nA to add account\nE to edit an account\nD to delete an account\nR to return details\nW to withdraw\nP to deposit money");
				answer = Console.ReadLine().ToLower();
				//add account
				if (answer == "a")
				{
					bank.AddAccounts();
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
					account = bank.FindAccount(intAnswer);
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
					account = bank.FindAccount(intAnswer);
					if (account != null)
					{
						account = null;
					}
				}
				else if (answer == "r")
				{
					Console.WriteLine("\nEnter the account number to return details: ");
					try
					{
						intAnswer = Convert.ToInt32(Console.ReadLine());
						bank.ReturnAccountDetails(intAnswer);
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
				}
				else if (answer == "w")
				{
					Console.WriteLine("Enter the account number to withdraw from:");
					try
					{
						intAnswer = Convert.ToInt32(Console.ReadLine());
						if (bank.FindAccount(intAnswer) != null)
						{
							Console.WriteLine("\n||ACCOUNT FOUND||\n");
							Console.WriteLine("Confirm your identity by entering your surname:");
							answer = Console.ReadLine();
							if (answer == bank.FindAccount(intAnswer).GetSurname())
							{
								Console.WriteLine("Enter the amount to be withdrawn:");
								toWithdraw_Deposit = Convert.ToDouble(Console.ReadLine());
								bank.FindAccount(intAnswer).Withdraw(toWithdraw_Deposit);
							}
							bank.ReturnBalance(intAnswer);
						}
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
				}
				else if (answer == "p")
				{
					Console.WriteLine("Enter the account number to deposit to:");
					try
					{
						intAnswer = Convert.ToInt32(Console.ReadLine());
						if (bank.FindAccount(intAnswer) != null)
						{
							Console.WriteLine("\n||ACCOUNT FOUND||\n");
							Console.WriteLine("Confirm your identity by entering your surname:");
							answer = Console.ReadLine();
							if (answer == bank.FindAccount(intAnswer).GetSurname())
							{
								Console.WriteLine("Enter the amount to deposit:");
								toWithdraw_Deposit = Convert.ToDouble(Console.ReadLine());
								bank.FindAccount(intAnswer).Deposit(toWithdraw_Deposit);
							}
							bank.ReturnBalance(intAnswer);
						}
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
				}		
			}
        }
    }

	//object Bank
    public class Bank
    {
        private BankAccount[] bank;
		private string answer;
		//private string sortCode;
		private int accountCount;
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
				//Console.WriteLine("Enter the new sort code");
				//sortCode = Console.ReadLine();
				//Console.WriteLine("New sort code is: {0}", sortCode);
				Console.WriteLine("Account number is: {0}", i + 1);
            }
            return true;
        }
        public bool AddAccounts()
        {
			//add a new account
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
					Console.WriteLine("Input out of range; Balance of account {0} set to 0", bank.Length);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine("Balance of account {0} set to 0", bank.Length);
			}
            //Console.WriteLine("Enter the new sort code");
            //sortCode = Console.ReadLine();
            //Console.WriteLine("New sort code is: {0}", sortCode);
            Console.WriteLine("Account number is: {0}", bank.Length);
            return true;
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
        public bool ReturnAccountDetails(int accountNo)
        {
			//what it says on the tin
            accountNo--;
            bank[accountNo].OutputDetails();
            bank[accountNo].OutputBalance();
            return true;
        }
		public bool ReturnBalance(int accountNo)
		{
			accountNo--;
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
        private string[] address = { "ab", "bc", "cd", "DE" };
        private int accountNum;
		public BankAccount()
		{
			foreName = "John";
			surName = "Smith";
			balance = 0;
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
            Console.WriteLine("Balance: {0} ", balance);
        }
        public void OutputDetails()
        {
            Console.WriteLine("\nName: {0} {1} ", foreName, surName);
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
		public string GetSurname()
		{
			return surName;
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
		public string[] GetAddress()
		{
			return address;
		}
        public bool SetAccountNum(int accountNumber)
        {
            accountNum = accountNumber;
            return true;
        }
    }
}
