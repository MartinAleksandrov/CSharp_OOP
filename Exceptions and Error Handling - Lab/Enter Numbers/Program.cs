namespace EnterNumbers
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>(10);
            int start = 1;
            int end = 100;
            while (numbers.Count < 10)
            {
                try
                {
                    numbers.Add(ReadNumber(ref start, end));
                }
                catch (FormatException fex)
                {
                    Console.WriteLine(fex.Message);
                }
                catch (ArgumentException argex)
                {
                    Console.WriteLine(argex.Message);
                }
            }

            Console.WriteLine(string.Join(", ", numbers));
        }

        private static int ReadNumber(ref int start, int end)
        {
            string numberAsString = Console.ReadLine()!;
            int numericValue;
            if (!int.TryParse(numberAsString, out numericValue))
                throw new FormatException("Invalid Number!");

            if (start >= numericValue || end <= numericValue)
                throw new ArgumentException($"Your number is not in range {start} - 100!");

            start = numericValue;
            return numericValue;
        }
    }
}