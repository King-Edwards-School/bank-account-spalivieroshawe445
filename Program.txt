//This version uses no text prompts or menus, and is to be used with the GUI (hopefully)
using System;
using System.Security.Cryptography;

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
			int intAnswer2;
			double toWithdraw_Deposit;
			//start of actual code
			//creates new bank (BankAccounts[]) of length input
			bank = new Bank(Convert.ToInt32(Console.ReadLine()));
			bank.AddAllAccount();
			while(true)
			{
				//main menu
				//add account
				if (answer == "a")
				{
					bank.AddAccounts();
				}
				//edit existing account
				else if (answer == "e")
				{
					account = bank.FindAccount(intAnswer);
					if (account != null)
					{
						if (answer == "for" || answer == "f")
						{
							account.SetForename(answer);
						}
						else if (answer == "sur" || answer == "s")
						{
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
					account = bank.FindAccount(intAnswer);
                    if (account != null && account.GetSortCode() == intAnswer2)
					{
						bank.DeleteAccount(intAnswer);
					}
                }
				else if (answer == "r")
				{
					//return the details of an account
					try
					{
						account = bank.FindAccount(intAnswer);
                        if (account.GetSortCode() == intAnswer)
                        {
                            if (answer == account.GetSurname())
                            {
								account.OutputDetails();
								account.OutputBalance();
                            }
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
					try
					{
						account = bank.FindAccount(intAnswer);
						if (account != null)
						{
							if (account.GetSortCode() == intAnswer)
							{
								if (answer == account.GetSurname())
								{
									account.Withdraw(toWithdraw_Deposit);
								}
								bank.ReturnBalance(intAnswer);
							}
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
					try
					{
						account = bank.FindAccount(intAnswer);
						if (account != null)
						{
                            if (account.GetSortCode() == intAnswer)
                            {
                                if (answer == account.GetSurname())
                                {
                                    account.Deposit(toWithdraw_Deposit);
                                }
                                bank.ReturnBalance(intAnswer);
                            }
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
				bank[i].SetForename(answer);
				bank[i].SetSurname(answer);
				try
				{
					if (answer >= 0)
					{
						bank[i].SetBalance(answer);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
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
            }
            return true;
        }
        public bool AddAccounts()
        {
			//add a new account
			
			Array.Resize(ref bank, bank.Length + 1);
			bank[bank.Length - 1] = new BankAccount();
			accountPos = bank.Length - 1;
			bank[accountPos].SetForename(answer);
			bank[accountPos].SetSurname(answer);
			try
			{
				if (answer >= 0)
				{
					bank[accountPos].SetBalance(answer);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
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
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Withdraw(double amount)
        {
			//opposite of the above (except when you attempt to withdraw more than is available, then rejects)
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
			//writes the balance
			Console.WriteLine(balance);
        }
        public void OutputDetails()
        {
			//writes all details to the screen
			Console.WriteLine(foreName, surName);
			Console.WriteLine(address[0], address[1], address[2], address[3], address[4]);
            Console.WriteLine(SortCode);
            Console.WriteLine(accountNum + 1);
		}
		public bool SetName()
        {
			//sets the name of the person using this account
            foreName = Console.ReadLine();
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
					if (balance > 0)
					{
						return true;
					}
				}
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
			return false;
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
}
