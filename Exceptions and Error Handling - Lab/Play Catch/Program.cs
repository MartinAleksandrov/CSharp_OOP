namespace Play_Catch;

using System;
using System.Linq;
using System.Reflection;

public class StartUp
{
    static void Main(string[] args)
    {
        List<int> input = Console.ReadLine().Split().Select(int.Parse).ToList();

        int exceptionSNum = 0;

        while (exceptionSNum<3)
        {
            string[] commArgs = Console.ReadLine().Split();

            try
            {
                string command = commArgs[0];
                if (command == "Replace")
                {
                    string startindex = commArgs[1];
                    Exceptions(startindex, input);

                    int FIndex = int.Parse(commArgs[1]);
                    int num = int.Parse(commArgs[2]);

                    input.RemoveAt(FIndex);
                    input.Insert(FIndex, num);
                }
                else if (command=="Print")
                {
                    string startIndex = commArgs[1];
                    string endIndex = commArgs[2];

                    Exceptions(startIndex, input);
                    Exceptions(endIndex, input);

                    int FIndex = int.Parse(commArgs[1]);
                    int LIndex = int.Parse(commArgs[2]);

                    Console.WriteLine(string.Join(", ",input.GetRange(FIndex,LIndex-FIndex+1)));
                }
                else if (command=="Show")
                {
                    string var = commArgs[1];
                    if (!int.TryParse(var, out int result))
                    {
                        throw new FormatException();
                    }
                    Exceptions(var, input);
                    int num = int.Parse(commArgs[1]);
                    Console.WriteLine($"{input[num]}");
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("The index does not exist!");
                exceptionSNum++;
            }
            catch (FormatException)
            {
                Console.WriteLine("The variable is not in the correct format!");
                exceptionSNum++;
            }
        }
        Console.WriteLine(string.Join(", ",input));
    }

    public static void Exceptions(string index, List<int> list)
    {
        if (!int.TryParse(index, out int result))
        {
            throw new FormatException();
        }
        int castIndex = int.Parse(index); 
        if (castIndex < 0 || castIndex >= list.Count)
        {
            throw new IndexOutOfRangeException();
        }
    }
}