using BirthdayCelebrations.Models;
using BirthdayCelebrations.Models.Interfaces;

namespace BirthdayCelebrations;
public class StartUp
{
    public static void Main()
    {
        //List<IBirthDate> birthDate = new List<IBirthDate>();
        List<Citizens> citizens = new List<Citizens>();
        List<Rebel> rebels = new List<Rebel>();


        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            string[] inputN = Console.ReadLine().Split();

            if (inputN.Length==4)
            {
                string citizenName = inputN[0];
                int age = int.Parse(inputN[1]);
                string citizenId = inputN[2];
                string birthDay = inputN[3];

                citizens.Add(new Citizens(citizenName, age, citizenId, birthDay));
            }
            else
            {
                string rebelName = inputN[0];
                int age = int.Parse(inputN[1]);
                string group = inputN[2];
                
                rebels.Add(new Rebel(rebelName, age, group));
            }

        }
        
        string input = string.Empty;
        while ((input = Console.ReadLine()) != "End")
        {

            foreach (var item in citizens)
            {
                if (item.Name == input)
                {
                    item.BuyFood();
                }
            }
            foreach (var item in rebels)
            {
                if (item.Name == input)
                {
                    item.BuyFood();
                }
            }

            //string[] commArgs = input.Split();


            //if (commArgs[0]== "Citizen")
            //{
            //    string citizenName = commArgs[1];
            //    int age = int.Parse(commArgs[2]);
            //    string citizenId = commArgs[3];
            //    string birthDay = commArgs[4];

            //    birthDate.Add(new Citizens(citizenName, age, citizenId, birthDay));
            //}
            //else if (commArgs[0]== "Pet")
            //{
            //    string petName = commArgs[1];
            //    string birthDay = commArgs[2];

            //    birthDate.Add(new Pet(petName,birthDay));
            //}  
        }
        Console.WriteLine(rebels.Sum(b => b.TotalFood) + citizens.Sum(b => b.TotalFood));
        //var lastDigits = Console.ReadLine();

        //birthDate.Where(c => c.Birthday.EndsWith(lastDigits))
        //    .Select(c => c.Birthday)
        //    .ToList()
        //    .ForEach(Console.WriteLine);
    }
}