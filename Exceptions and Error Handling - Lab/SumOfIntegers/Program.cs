namespace Cards;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;

public class StartUp
{
    static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split();

        int sum = 0;

        foreach (var el in input)
        {
            try
            {
                if (long.TryParse(el, out long num))
                {
                    if (num > int.MaxValue)
                    {
                        throw new OverflowException();
                    }
                    sum += (int)num;
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine($"The element '{el}' is out of range!");
            }
            catch (FormatException)
            {
                Console.WriteLine($"The element '{el}' is in wrong format!");
            }

            Console.WriteLine($"Element '{el}' processed - current sum: {sum}");
        }
        Console.WriteLine($"The total sum of all integers is: {sum}");
    }
}