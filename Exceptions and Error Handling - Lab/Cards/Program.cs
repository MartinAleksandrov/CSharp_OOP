namespace Cards;

using System;
using System.Collections;
using System.Collections.Generic;

public class StartUp
{
    static void Main(string[] args)
    {

        List<string> validFaces = new List<string>
        {
            "2","3","4","5","6","7","8","9","10","J","Q","K","A"
        };
        Dictionary<string, string> validSuits = new Dictionary<string, string>();
        validSuits.Add("S", "\u2660");
        validSuits.Add("H", "\u2665");
        validSuits.Add("D", "\u2666");
        validSuits.Add("C", "\u2663");

        List<string> results = new List<string>();

        string[] input = Console.ReadLine().Split(", ");

        foreach (var item in input)
        {
            try
            {
                string[] splittedInput = item.Split();
                string face = splittedInput[0];
                string suit = splittedInput[1];
                if (validFaces.Contains(face) && validSuits.ContainsKey(suit))
                {
                    results.Add($"[{face}{validSuits[suit]}]");
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid card!");
            }
        }
        Console.WriteLine(string.Join(" ",results));
    }
}