namespace MoneyTransactions;

using System;

public class StartUp
{
    static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split(",");
        Dictionary<double, double> bank = new Dictionary<double, double>();

        foreach (var el in input)
        {
            string[] splittedArray = el.Split("-");
            double accountNum = double.Parse(splittedArray[0]);
            double balance = double.Parse(splittedArray[1]);

            bank.Add(accountNum, balance);

        }

        string commands = string.Empty;
        while ((commands=Console.ReadLine())!="End")
        {
            string[] commArgs = commands.Split();

            string command = commArgs[0];
            double accNum = double.Parse(commArgs[1]);
            double accBal = double.Parse(commArgs[2]);

            try
            {
                if (!bank.ContainsKey(accNum))
                {
                    throw new DirectoryNotFoundException();
                }


                if (command == "Deposit")
                {
                    bank[accNum] += accBal;
                }
                else if (command == "Withdraw")
                {
                    if (bank[accNum] < accBal)
                    {
                        throw new OverflowException();
                    }
                    bank[accNum] -= accBal;
                }
                else
                {
                    throw new FormatException();
                }
                Console.WriteLine($"Account {accNum} has new balance: {bank[accNum]:f2}");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Invalid account!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Insufficient balance!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid command!");
            }
           
            Console.WriteLine("Enter another command");
        }
    }
}