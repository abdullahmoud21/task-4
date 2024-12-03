﻿namespace task_4
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        public DateTime DateTime { get; set; }
        

        public Account(string name = "Unnamed Account", double balance = 0.0 , DateTime dateTime = default)
        {
            Name = name;
            Balance = balance;
            DateTime = dateTime;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
        public override string ToString() { return $"Name: {Name}, Balance: {Balance}"; }
    
    }
    public static class AccountUtil
    {
        // Utility helper functions for Account class

        public static void Display(List<Account> accounts)
        {
            Console.WriteLine("\n=== Accounts ==========================================");
            foreach (var acc in accounts)
            {
                Console.WriteLine(acc);
            }
        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }
    class SavingsAccount : Account
    {
        public double InterestRate {  get; set; }

        public SavingsAccount(string Name = "Unnamed Account" , double Balance = 0.0 , double interestRate = 0.0) : base(Name , Balance)
        {
            InterestRate = interestRate;
        }
        public override string ToString() { return $"{base.ToString()}, Interest Rate: {InterestRate}%"; }
        public override bool Deposit(double amount)
        {
            return base.Deposit(amount);
        }
        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount);
        }
        public bool AddInterestRate()
        {
            double interest = Balance * (InterestRate / 100);
            Balance += interest;
            return true;
        }

    }
    class CheckingAccount : Account
    {
        public CheckingAccount(string Name = "Unnamed Account", double Balance = 0.0, double withdrawFee = 1.5) : base(Name , Balance) 
        {
            WithdrawFee = withdrawFee;
        }

        public double WithdrawFee {  get; set; }
        public override string ToString() { return $"{base.ToString()}, Withdrawal Fee: {WithdrawFee:C}"; }
        public override bool Withdraw(double amount) 
        {
            return base.Withdraw(amount + WithdrawFee);
        }
        public override bool Deposit(double amount)
        {
            return base.Deposit(amount);
        }
    }
    class TrustAccount : SavingsAccount
    {
        public TrustAccount(string Name = "Unnamed Account", double Balance = 0.0, double InterestRate = 0.0, int withdrawCount = 3) : base(Name, Balance, InterestRate)
        {
            WithdrawCount = withdrawCount;
        }
        public int WithdrawCount { get; set; }
        
        public override string ToString() { return $"{base.ToString()}, Withdrawals This Year: {WithdrawCount}, Withdrawal Limit: {WithdrawCount}"; }
        public override bool Deposit(double amount)
        {
            double DepositBonus = 50;
            if (amount <= 5000.00)
            {
                return base.Deposit(amount + DepositBonus);
            }
            return true;
        }
        public override bool Withdraw(double amount)
        {
            if (amount < 0.2 * Balance && WithdrawCount >= WithdrawCount )
            {
                return base.Withdraw(amount);
            }
            return true;
        }
    }
    

    internal class Program
    {
        static void Main(string[] args)
        {
            // Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Savings
            var savAccounts = new List<Account>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.Display(savAccounts);
            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);

            // Checking
            var checAccounts = new List<Account>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.Display(checAccounts);
            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            AccountUtil.Withdraw(checAccounts, 2000);

            // Trust
            var trustAccounts = new List<Account>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.Display(trustAccounts);
            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 6000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);

            Console.WriteLine();
        }
    }
}
